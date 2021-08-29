using System;

namespace FinancialTransactions.Gateways.PaymentGateway
{
    public class Payment
    {
        public long Id { get; set; }
        public string ExternalReference { get; set; }
        public string Status { get; set; }
        public string StatusDetail { get; set; }
        public PaymentStatus PaymentStatus
        {
            get 
            {
                var status = Status.Replace("_", "");
                return Enum.Parse<PaymentStatus>(status, true);
            }
        }
    }

    public enum PaymentStatus
    { 
        Pending,
        Approved,
        InProcess,
        Rejected,
        Cancelled
    }
}
