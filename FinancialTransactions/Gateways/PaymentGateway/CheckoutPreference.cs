using System.Collections.Generic;

namespace FinancialTransactions.Gateways.PaymentGateway
{
    public class CheckoutPreference
    {
        public string Id { get; set; }
        public string ExternalReference { get; set; }
        public string StatementDescriptor { get; set; } = "DIVAGANDO";
        public bool BinaryMode { get; } = false;
        public bool Expires { get; } = false;
        public string NotificationUrl { get; } = "https://divagando-api.azurewebsites.net/paymentGateway/notifications";
        public Payer Payer { get; set; }
        public List<CheckoutPreferenceItem> Items { get; set; }
    }

    public class CheckoutPreferenceItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public string CategoryId { get; set; } = "ticket";
        public int Quantity { get; set; } = 1;
        public string CurrencyId { get; set; } = "BRL";
    }

    public class Payer
    {
        public string Email { get; set; }
    }
}
