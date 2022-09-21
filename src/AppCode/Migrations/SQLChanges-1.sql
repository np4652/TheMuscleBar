CREATE PROC Proc_DashboardData
AS
BEGIN
DECLARE @TotalUser int,@TotalSubscription int,@ActiveSubscription int,@ExpiredSubscription int,@AboutToExpired int
Select @TotalUser = Count(1) from Users(nolock) Where Id <> 1
Select @TotalSubscription = Count(1) from UserSubscription(nolock)
Select @ActiveSubscription = Count(1) from UserSubscription(nolock) Where DateTo >= GETDATE()
Select @ExpiredSubscription = Count(1) from UserSubscription(nolock) Where DateTo < GETDATE()
Select @AboutToExpired = Count(1) from UserSubscription(nolock) Where DateTo <= (GETDATE() - 7) and DateTo > GETDATE()

Select @TotalUser TotalUser,@TotalSubscription TotalSubscription,@ActiveSubscription ActiveSubscription,@ExpiredSubscription ExpiredSubscription,@AboutToExpired AboutToExpired
END