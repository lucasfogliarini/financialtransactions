namespace FinancialTransactions.Gateways.PaymentGateway
{
    public interface IPaymentGateway
    {
        long? Refund(long paymentId);
        long? Cancel(long paymentId);
        CheckoutPreference CreatePreference(CheckoutPreference checkoutPreference);
        Payment GetPayment(long paymentId);
    }
}
