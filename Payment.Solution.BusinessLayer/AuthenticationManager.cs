using Payment.Solution.DataAccessLayer.Interfaces;
using Payment.Solution.RuntimeModels;
using System.Linq;

namespace Payment.Solution.BusinessLayer
{
    public class AuthenticationManager
    {
        private readonly IRepository<User> _userRepository;
        public AuthenticationManager(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public User Login(LoginModel loginModel)
        {
            var user = _userRepository.Get($"{nameof(User.IdentityNumber)} = '{loginModel.IdentityNumber}' AND " +
                $"{nameof(User.Password)} = '{loginModel.Password}'").FirstOrDefault();

            return user;
        }
    }
}
