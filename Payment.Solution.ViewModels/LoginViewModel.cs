using Payment.Solution.RuntimeModels;

namespace Payment.Solution.ViewModels
{
    public class LoginViewModel
    {
        public string IdentityNumber { get; set; }
        public string Password { get; set; }

        public static implicit operator LoginModel(LoginViewModel author)
        {
            return new LoginModel
            {
                IdentityNumber = author.IdentityNumber,
                Password = author.Password
            };
        }


    }
}
