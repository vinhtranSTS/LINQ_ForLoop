using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LINQ_FORLoop
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Tuple<Ledger, Provider>> ledgerAndProviders = new List<Tuple<Ledger, Provider>>();

            List<Transaction> transactions = new List<Transaction>();

            List<LedgerItem> ledgerItems = new List<LedgerItem>();

            List<string> holdingIds = new List<string>();

            Random rnd = new Random();

            Console.WriteLine($"Reloading data...");

            for (int i = 0; i < 2000; i++)
            {
                string holdingId = $"ho-{i}";

                holdingIds.Add(holdingId);

                ledgerAndProviders.Add(new Tuple<Ledger, Provider>(new Ledger(holdingId), new Provider(holdingId)));
            }

            for (int j = 0; j < 70000; j++)
            {
                int r = rnd.Next(holdingIds.Count);

                string transactionId = $"tr-{j}";

                transactions.Add(new Transaction(holdingIds[r], transactionId));
            }

            for (int k = 0; k < 1000; k++)
            {
                int r = rnd.Next(holdingIds.Count);

                string ledgerItemId = $"lg-{k}";
                ledgerItems.Add(new LedgerItem(holdingIds[r], ledgerItemId));
            }

            Console.WriteLine($"Running test...");

            var approach = new ApproachLINQAndForLoop();

            var watch = Stopwatch.StartNew();
            ledgerAndProviders = approach.Approach(ledgerAndProviders, transactions, ledgerItems);
            watch.Stop();
            Console.WriteLine($"Approach {watch.ElapsedMilliseconds} ms");

            ledgerAndProviders.ForEach(x => { x.Item2.Transactions = null; x.Item2.LedgerItems = null; });

            watch = Stopwatch.StartNew();
            ledgerAndProviders = approach.ApproachOptimize(ledgerAndProviders, transactions, ledgerItems);
            watch.Stop();
            Console.WriteLine($"ApproachOptimize {watch.ElapsedMilliseconds} ms");

            ledgerAndProviders.ForEach(x => { x.Item2.Transactions = null; x.Item2.LedgerItems = null; });

            watch = Stopwatch.StartNew();
            ledgerAndProviders = approach.ApproachLINQ(ledgerAndProviders, transactions, ledgerItems);
            watch.Stop();
            Console.WriteLine($"ApproachLINQ {watch.ElapsedMilliseconds} ms");

            ledgerAndProviders.ForEach(x => { x.Item2.Transactions = null; x.Item2.LedgerItems = null; });

            watch = Stopwatch.StartNew();
            ledgerAndProviders = approach.ApproachLambdaLINQ(ledgerAndProviders, transactions, ledgerItems);
            watch.Stop();
            Console.WriteLine($"ApproachLambdaLINQ {watch.ElapsedMilliseconds} ms");

            Console.ReadKey();
        }
    }
}
