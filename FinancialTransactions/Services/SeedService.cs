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
                _accountService.CreateAsync("finance@totvs.com", "TOTVS");
                _accountService.CreateAsync("finance@supplier.com", "Supplier");

                //CreateParticipant("lucasfogliarini", "Lucas Fogliarini Pedroso", "51992364249", "lucasfogliarini@gmail.com");
                //CreateParticipant("luanabueno", "Luana Bueno", "5193840006", "luanabuenoflores@gmail.com");
                //CreateParticipant("guistaub", "Guilherme Staub", "5194171008", "gui_staub@hotmail.com");
                //CreateParticipant("micheleborba", "Michele Borba", "5198644493", "");
                //CreateParticipant("felipealmeida", "Felipe Almeida", "51980451264", "felipealmeida1395@gmail.com");
                //CreateParticipant("grazieleribeiro", "Graziele Ribeiro", "5199779985", "");
                //CreateParticipant("schieck", "Ricardo Schieck", "5596258202", "ricardoschieck@gmail.com");
                //CreateParticipant("horlle", "Gabriel Horlle", "5191388345", "grhorlle@gmail.com");
                //CreateParticipant("jonmoon", "Jonathan Monteiro", "5191388345", "jonathan.m.m@live.com");
            }
            catch (Exception)
            {
            }
        }
    }
}
