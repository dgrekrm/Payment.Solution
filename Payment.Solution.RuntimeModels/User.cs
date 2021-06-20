using System;

namespace Payment.Solution.RuntimeModels
{
    public class User
    {
        public string IdentityNumber { get; set; }
        public string Password { get; set; }
        public byte UserType { get; set; }
    }
}
