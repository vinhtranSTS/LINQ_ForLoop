using System;
using System.Collections.Generic;
using System.Text;

namespace LINQ_FORLoop
{
    public class Provider
    {
        public Provider(string holdingId)
        {
            HoldingId = holdingId;
        }

        public Provider(Provider provider, List<Transaction> transactions = null, List<LedgerItem> ledgerItems = null)
        {
            HoldingId = provider.HoldingId;
            provider.Transactions = transactions ?? provider.Transactions;
            provider.LedgerItems = ledgerItems ?? provider.LedgerItems;
        }

        public string HoldingId { get; set; }
        public List<Transaction> Transactions { get; set; }
        public List<LedgerItem> LedgerItems { get; set; }
    }
}
