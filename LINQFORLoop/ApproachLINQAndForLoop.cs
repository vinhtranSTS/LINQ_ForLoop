using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQ_FORLoop
{
   public class ApproachLINQAndForLoop
    {
        public List<Tuple<Ledger, Provider>> Approach(List<Tuple<Ledger, Provider>> ledgerAndProviders, List<Transaction> transactions, List<LedgerItem> ledgerItems)
        {
            foreach (var ledgerAndProvider in ledgerAndProviders)
            {
                var provider = ledgerAndProvider.Item2;
                var transactionsByHolding = transactions.Where(z => z.HoldingId == provider.HoldingId);
                var ledgerItemsByHolding = ledgerItems.Where(z => z.HoldingId == provider.HoldingId);

                if (transactionsByHolding.Any())
                {
                    provider.Transactions = transactionsByHolding.ToList();
                }
                if (ledgerItemsByHolding.Any())
                {
                    provider.LedgerItems = ledgerItemsByHolding.ToList();
                }
            }

            return ledgerAndProviders;
        }


        public List<Tuple<Ledger, Provider>> ApproachOptimize(List<Tuple<Ledger, Provider>> ledgerAndProviders, List<Transaction> transactions, List<LedgerItem> ledgerItems)
        {
            var ledgerAndProvidersIds = ledgerAndProviders.Select(x => x.Item2).Select(x => x.HoldingId).ToHashSet();

            var transactionsByHolding = transactions.Where(x => ledgerAndProvidersIds.Contains(x.HoldingId)).GroupBy(x => x.HoldingId).ToDictionary(x => x.Key, x => x.ToList());

            var ledgerItemsByHolding = ledgerItems.Where(x => ledgerAndProvidersIds.Contains(x.HoldingId)).GroupBy(x => x.HoldingId).ToDictionary(x => x.Key, x => x.ToList());

            for (int i = 0; i < ledgerAndProviders.Count; i++)
            {
                var provider = ledgerAndProviders[i].Item2;

                if (transactionsByHolding.TryGetValue(provider.HoldingId, out List<Transaction> transactionsList))
                {
                    provider.Transactions = transactionsList;
                }

                if (ledgerItemsByHolding.TryGetValue(provider.HoldingId, out List<LedgerItem> ledgerItemsList))
                {
                    provider.LedgerItems = ledgerItemsList;
                }
            }

            return ledgerAndProviders;
        }

        /// <summary>
        /// Can merge 2 query into one...
        /// </summary>
        /// <param name="ledgerAndProviders"></param>
        /// <param name="transactions"></param>
        /// <param name="ledgerItems"></param>
        /// <returns></returns>
        public List<Tuple<Ledger, Provider>> ApproachLINQ(List<Tuple<Ledger, Provider>> ledgerAndProviders, List<Transaction> transactions, List<LedgerItem> ledgerItems)
        {
            var globalVariables1 = from ledgerAndProvider in ledgerAndProviders
                                   join tran in transactions on ledgerAndProvider.Item2.HoldingId equals tran.HoldingId
                                   into leftJoinList
                                   from transaction in leftJoinList.DefaultIfEmpty()
                                   select new { ledgerAndProvider, transaction } into data
                                   group data by data.ledgerAndProvider.Item2.HoldingId into grouped
                                   select new Tuple<Ledger, Provider>
                                   (
                                    (grouped.FirstOrDefault().ledgerAndProvider.Item1),
                                    (new Provider(grouped.FirstOrDefault().ledgerAndProvider.Item2, grouped.Where(x => x.transaction != null).Select(x => x.transaction).ToList()))
                                   );

            ledgerAndProviders = globalVariables1.ToList();


            var globalVariables2 = from ledgerAndProvider in ledgerAndProviders
                                   join ledgerItem in ledgerItems on ledgerAndProvider.Item2.HoldingId equals ledgerItem.HoldingId
                                   into leftJoinList
                                   from ledgerItem in leftJoinList.DefaultIfEmpty()
                                   select new { ledgerAndProvider, ledgerItem } into data
                                   group data by data.ledgerAndProvider.Item2.HoldingId into grouped
                                   select new Tuple<Ledger, Provider>
                                   (
                                    (grouped.FirstOrDefault().ledgerAndProvider.Item1),
                                    (new Provider(grouped.FirstOrDefault().ledgerAndProvider.Item2, null, grouped.Where(x => x.ledgerItem != null).Select(x => x.ledgerItem).ToList()))
                                   );

            ledgerAndProviders = globalVariables2.ToList();

            return ledgerAndProviders;
        }

        /// <summary>
        /// Can merge 2 query into one...
        /// </summary>
        /// <param name="ledgerAndProviders"></param>
        /// <param name="transactions"></param>
        /// <param name="ledgerItems"></param>
        /// <returns></returns>
        public List<Tuple<Ledger, Provider>> ApproachLambdaLINQ(List<Tuple<Ledger, Provider>> ledgerAndProviders, List<Transaction> transactions, List<LedgerItem> ledgerItems)
        {
            var globalVariables1 = ledgerAndProviders.GroupJoin(
                        transactions.GroupBy(x => x.HoldingId).ToDictionary(x => x.Key, x => x.ToList()),
                        ledgerAndProvider => ledgerAndProvider.Item2.HoldingId,
                        transaction => transaction.Key,
                        (ledgerAndProviders, transactions) => new { ledgerAndProviders, transactions })
                .SelectMany(
                              data => data.transactions.DefaultIfEmpty(),
                              (ledgerAndProviders, transactions) => new
                                     Tuple<Ledger, Provider>(
                                            ledgerAndProviders.ledgerAndProviders.Item1,
                                             new Provider(ledgerAndProviders.ledgerAndProviders.Item2, transactions.Value)
                 ));

            ledgerAndProviders = globalVariables1.ToList();


            var globalVariables2 = ledgerAndProviders.GroupJoin(
                    ledgerItems.GroupBy(x => x.HoldingId).ToDictionary(x => x.Key, x => x.ToList()),
                    ledgerAndProvider => ledgerAndProvider.Item2.HoldingId,
                    transaction => transaction.Key,
                    (ledgerAndProviders, ledgerItems) => new { ledgerAndProviders, ledgerItems })
            .SelectMany(
                          data => data.ledgerItems.DefaultIfEmpty(),
                          (ledgerAndProviders, ledgerItems) => new
                                 Tuple<Ledger, Provider>(
                                        ledgerAndProviders.ledgerAndProviders.Item1,
                                         new Provider(ledgerAndProviders.ledgerAndProviders.Item2, null, ledgerItems.Value)
             ));

            ledgerAndProviders = globalVariables2.ToList();

            return ledgerAndProviders;
        }

    }
}
