using FinancialTransactions.Databases.Abstractions;
using FinancialTransactions.Services.Abstractions;
using System;

namespace FinancialTransactions.Services
{
    internal class SeedService : ISeedService
    {
        readonly IAccountService _accountService;

        public SeedService(IFinancialTransactionsDatabase database,
                            IAccountService accountService)
        {
            _accountService = accountService;
        }

        public int Seed()
        {
            SeedAccounts();
            return 1;
        }
        public void SeedAccounts()
        {
            try
            {
                _accountService.CreateAsync("lucasfogliarini@gmail.com", "Lucas Fogliarini");
                _accountService.CreateAsync("luanabuenoflores@gmail.com", "Luana Flores");
            }
            catch (Exception)
            {
            }
        }
    }
}
