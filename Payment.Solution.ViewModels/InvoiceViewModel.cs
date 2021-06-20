using Payment.Solution.RuntimeModels;

namespace Payment.Solution.ViewModels
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }
        public bool PaymentStatus { get; set; }
        public string PaymentStatusText { get; set; } /* If false need to pay, else no */
        public string Amount { get; set; }
        public string IdentityNumber { get; set; }

        public static implicit operator InvoiceViewModel(Invoice invoice)
        {
            return new InvoiceViewModel
            {
                Id = invoice.Id,
                PaymentStatus = invoice.PaymentStatus,
                Amount = invoice.Amount.ToString(),
                PaymentStatusText = invoice.PaymentStatus ? "Ödendi" : "Ödenmedi",
                IdentityNumber = invoice.IdentityNumber
            };
        }

    }
}
