using System;
using System.Collections.Generic;
using System.Text;

namespace LINQ_FORLoop
{
    public class Ledger
    {
        public Ledger(string holdingId)
        {
            HoldingId = holdingId;
        }

        public string HoldingId { get; set; }
    }
}
