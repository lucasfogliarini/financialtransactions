using System;

namespace FinancialTransactions.Entities.Abstractions
{
    public interface IIdentity
    {
        public string Identity { get; set; }
        public static string Convert(string text = null)
        {
            if (text == null)
            {
                text = Guid.NewGuid().ToString().Replace("-","");
            }
            text = text.ToLower().Replace(" ", "");
            return text.Length <= 30 ? text : text.Substring(0, 30);
        }
    }
}