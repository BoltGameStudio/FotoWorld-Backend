using FotoWorldBackend.Models;
using FotoWorldBackend.Utilities;
using Microsoft.AspNetCore.Identity;

namespace FotoWorldBackend.Services
{
    public class AuthService : IAuthService
    {
        private readonly FotoWorldContext _context;
        private PasswordHash _passwordHash;

        public AuthService(FotoWorldContext context)
        {
            _context = context;
            _passwordHash = new PasswordHash() ;
        }

        public bool Login(LoginModel login)
        {
            throw new NotImplementedException();
        }

        public bool RegisterOperator(RegisterOperatorModel register)
        {
            throw new NotImplementedException();
        }

        public bool RegisterUser(RegisterUserModel register)
        {
            if (_context.Users.FirstOrDefault(m => m.Email == register.Email) == null)
            {
                if(register.Password == register.RepeatPassword)
                {
                    var newUser = new User();
                    newUser.Username = register.Username;
                    newUser.Email = register.Email;
                    newUser.PhoneNumber = register.PhoneNumber;
                    newUser.PasswordSalt= _passwordHash.GenerateSalt();
                    newUser.HashedPassword = _passwordHash.HashPassword(register.Password, newUser.PasswordSalt);
                    _context.Users.Add(newUser);
                    _context.SaveChanges();
                    return true;
                }


            }
            return false;
        }
    }
}
