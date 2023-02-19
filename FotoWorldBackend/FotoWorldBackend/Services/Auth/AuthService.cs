using FotoWorldBackend.Models;
using FotoWorldBackend.Services.Email;
using FotoWorldBackend.Utilities;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace FotoWorldBackend.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly FotoWorldContext _context;
        private PasswordHash _passwordHash;
        private readonly IEmailService _emailService;
        public AuthService()
        {
            _context = new FotoWorldContext();
            _passwordHash = new PasswordHash();
            
        }

        //jezeli gosc jest operatorem to ma zwykle konto wiec go zalogujmy jako zwykle konto a potem mu zrobimy odpwiedni token na bazie tego co zaznaczy

        public bool Login(LoginModel login)
        {


            var user = _context.Users.FirstOrDefault(m => m.Email == login.Username ||m.Username==login.Username);
            if (user != null)
            {
                if(user.IsActice)
                {
                    return _passwordHash.VerifyPassword(login.Password, user);
                }
                
            }
            return false;
        }

        public Operator RegisterOperator(RegisterOperatorModel register)
        {
            var newServices = new OperatorService();
            var newOperator = new Operator();
            var newUser = new User();

            if (_context.Operators.FirstOrDefault(m => m.Email == register.Email) == null && _context.Users.FirstOrDefault(m => m.Email == register.Email) == null)
            {

                if (register.Password == register.RepeatPassword)
                {


                    newServices.DronePhoto = register.DronePhotoService;
                    newServices.DroneFilm = register.DroneFilmService;
                    newServices.Photo = register.PhotoService;
                    newServices.Filming = register.FilmingService;
                    _context.OperatorServices.Add(newServices);
                    _context.SaveChanges();


                    newOperator.Username = register.Username;
                    newOperator.Email = register.Email;
                    newOperator.PhoneNumber = register.PhoneNumber;
                    newOperator.PasswordSalt = _passwordHash.GenerateSalt();
                    newOperator.HashedPassword = _passwordHash.HashPassword(register.Password, newOperator.PasswordSalt);
                    newOperator.Services = newServices.Id;
                    newOperator.IsCompany = register.IsCompany;
                    newOperator.LocationCity = register.LocationCity;
                    newOperator.OperatingRadius = register.OperatingRadius;
                    newOperator.IsActice = false;
                    _context.Operators.Add(newOperator);
                    _context.SaveChanges();


                    newUser.Username = newOperator.Username;
                    newUser.Email = newOperator.Email;
                    newUser.PhoneNumber = newOperator.PhoneNumber;
                    newUser.PasswordSalt = newOperator.PasswordSalt;
                    newUser.HashedPassword = newOperator.HashedPassword;
                    newUser.IsActice = false;
                    _context.Users.Add(newUser);
                    _context.SaveChanges();
                    return newOperator;
                }


            }

            return null ;
        }

        public User RegisterUser(RegisterUserModel register)
        {
            if (_context.Users.FirstOrDefault(m => m.Email == register.Email) == null)
            {
                if (register.Password == register.RepeatPassword)
                {
                    var newUser = new User();
                    newUser.Username = register.Username;
                    newUser.Email = register.Email;
                    newUser.PhoneNumber = register.PhoneNumber;
                    newUser.PasswordSalt = _passwordHash.GenerateSalt();
                    newUser.HashedPassword = _passwordHash.HashPassword(register.Password, newUser.PasswordSalt);
                    newUser.IsActice = false;
                    _context.Users.Add(newUser);
                    _context.SaveChanges();





                    return newUser;
                }


            }
            return null;
        }


        public bool ActivateAccount(int id) { 
            User userToActivate= _context.Users.FirstOrDefault(m=>m.Id== id);
            if (userToActivate != null)
            {
                userToActivate.IsActice= true;
                _context.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
