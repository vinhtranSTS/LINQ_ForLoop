using System;
using System.Collections.Generic;
using System.Text;

namespace LINQ_FORLoop
{
    public class Transaction
    {
        public Transaction()
        {

        }

        public Transaction(string holdingId,string entityId)
        {
            HoldingId = holdingId;
            EntityId = entityId;
        }

        public string HoldingId { get; set; }
        public string EntityId { get; set; }
    }
}
