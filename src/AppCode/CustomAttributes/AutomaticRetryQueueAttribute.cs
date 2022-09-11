using Hangfire.Common;
using Hangfire.Logging;
using Hangfire.States;
using Hangfire.Storage;
using System;
using System.Linq;

namespace TheMuscleBar.AppCode.CustomAttributes
{
    public class AutomaticRetryQueueAttribute : JobFilterAttribute, IApplyStateFilter, IElectStateFilter
    {
        private string queue;
        private int attempts;
        private readonly object _lockObject = new object();

        private readonly ILog _logger = LogProvider.For<AutomaticRetryQueueAttribute>();
        public AutomaticRetryQueueAttribute(int Attempts = 10, string Queue = "Default")
        {
            queue = Queue;
            attempts = Attempts;
        }

        public int Attempts
        {
            get { lock (_lockObject) { return attempts; } }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), @"Attempts value must be equal or greater than zero.");
                }

                lock (_lockObject)
                {
                    attempts = value;
                }
            }
        }

        public string Queue
        {
            get { lock (_lockObject) { return queue; } }
            set
            {
                lock (_lockObject)
                {
                    queue = value;
                }
            }
        }
        public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            var newState = context.NewState as EnqueuedState;
            if (!string.IsNullOrWhiteSpace(queue) && newState != null && newState.Queue != Queue)
            {
                newState.Queue = String.Format(Queue, context.BackgroundJob.Job.Args.ToArray());
            }

            if (context.NewState is ScheduledState &&
                context.NewState.Reason != null &&
                context.NewState.Reason.StartsWith("Retry attempt"))
            {
                transaction.AddToSet("retries", context.BackgroundJob.Id);
            }
        }

        public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            if (context.OldStateName == ScheduledState.StateName)
            {
                transaction.RemoveFromSet("retries", context.BackgroundJob.Id);
            }
        }

        public void OnStateElection(ElectStateContext context)
        {
            var failedState = context.CandidateState as FailedState;
            if (failedState == null)
            {
                // This filter accepts only failed job state.
                return;
            }

            if (context.CandidateState is EnqueuedState enqueuedState)
            {
                enqueuedState.Queue = String.Format(Queue, context.BackgroundJob.Job.Args.ToArray());
            }

            var retryAttempt = context.GetJobParameter<int>("RetryCount") + 1;

            if (retryAttempt <= Attempts)
            {
                ScheduleAgainLater(context, retryAttempt, failedState);
            }
            else
            {
                _logger.ErrorException($"Failed to process the job '{context.BackgroundJob.Id}': an exception occurred.", failedState.Exception);
            }
        }

        private void ScheduleAgainLater(ElectStateContext context, int retryAttempt, FailedState failedState)
        {
            context.SetJobParameter("RetryCount", retryAttempt);

            const int maxMessageLength = 50;
            var exceptionMessage = failedState.Exception.Message.Length > maxMessageLength
                ? failedState.Exception.Message.Substring(0, maxMessageLength - 1) + "…"
                : failedState.Exception.Message;

            // If attempt number is less than max attempts, we should
            // schedule the job to run again later.

            var reason = $"Retry attempt {retryAttempt} of {Attempts}: {exceptionMessage}";

            context.CandidateState = (IState)new EnqueuedState { Reason = reason };

            if (context.CandidateState is EnqueuedState enqueuedState)
            {
                enqueuedState.Queue = String.Format(Queue, context.BackgroundJob.Job.Args.ToArray());
            }

            _logger.WarnException($"Failed to process the job '{context.BackgroundJob.Id}': an exception occurred. Retry attempt {retryAttempt} of {Attempts} will be performed.", failedState.Exception);
        }
    }
}
