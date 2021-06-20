namespace Payment.Solution.RuntimeModels
{
    public class Invoice
    {
        public int UserId { get; set; }
        public bool PaymentStatus { get; set; } /* If false need to pay, else no */
        public decimal Amount { get; set; }
        public string IdentityNumber { get; set; }
        public int Id { get; set; }
    }
}
