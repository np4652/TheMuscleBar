﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheMuscleBar.Models
{
    public class PostICICIStatementRequest
    {
        public string AccountNo { get; set; }
        public List<ICICTransactionDetail> data { get; set; }
    }
    public class ICICTransactionDetail
    {
        public string SlNo { get; set; }
        public string TransactionId { get; set; }
        public string ValueDate { get; set; }
        public string TransactionDate { get; set; }
        public string TransactionPostedDate { get; set; }
        public string ChequeRefNo { get; set; }
        public string TransactionRemarks { get; set; }
        public string TransactionAmount { get; set; }
        public string TransactionType { get; set; }
        public string AvailableBalance { get; set; }
    }
}
