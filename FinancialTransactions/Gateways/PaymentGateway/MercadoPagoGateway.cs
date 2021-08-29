using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;

namespace FinancialTransactions.Gateways.PaymentGateway
{
    internal class MercadoPagoGateway : IPaymentGateway
    {
        private readonly HttpClient _httpClient;

        public MercadoPagoGateway(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public long? Refund(long paymentId)
        {
            throw new System.NotImplementedException();
        }
        public long? Cancel(long paymentId)
        {
            throw new System.NotImplementedException();
        }
        public CheckoutPreference CreatePreference(CheckoutPreference checkoutPreference)
        {
            string checkoutPreferenceJson = JsonConvert.SerializeObject(checkoutPreference, GetJsonSerializerSettings());
            var httpContent = new StringContent(checkoutPreferenceJson);

            var result = _httpClient.PostAsync("checkout/preferences", httpContent).Result;
            string resultContent = result.Content.ReadAsStringAsync().Result;
            checkoutPreference = JsonConvert.DeserializeObject<CheckoutPreference>(resultContent, GetJsonSerializerSettings());
            return checkoutPreference;
        }
        public Payment GetPayment(long paymentId)
        {
            var paymentResponse = _httpClient.GetAsync($"v1/payments/{paymentId}").Result;
            string paymentJson = paymentResponse.Content.ReadAsStringAsync().Result;
            var payment = JsonConvert.DeserializeObject<Payment>(paymentJson, GetJsonSerializerSettings());
            return payment;
        }

        private JsonSerializerSettings GetJsonSerializerSettings()
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            };
            return jsonSerializerSettings;
        }
    }
}
