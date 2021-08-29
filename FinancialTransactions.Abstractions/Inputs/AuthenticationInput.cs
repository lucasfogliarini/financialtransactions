namespace FinancialTransactions.Inputs.Abstractions
{
    public class AuthenticationInput
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string Provider { get; set; }
    }
}
