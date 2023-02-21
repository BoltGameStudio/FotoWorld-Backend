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
        private readonly IEmailService _emailService;

        public AuthService(FotoWorldContext context)
        {
            _context = context;
            
            
        }

     

        public User LoginUser(LoginModel login)
        {


            var user = _context.Users.FirstOrDefault(m => m.Email == login.Username ||m.Username==login.Username);
            if (user != null)
            {
                if(user.IsActice)
                {
                    if(PasswordHash.VerifyPassword(login.Password, user))
                    {
                        return user;
                    }
                }
                
            }
            return null;
        }




        public Operator RegisterOperator(RegisterOperatorModel register)
        {
            var newServices = new OperatorService();
            var newOperator = new Operator();


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
                    newOperator.PasswordSalt = PasswordHash.GenerateSalt();
                    newOperator.HashedPassword = PasswordHash.HashPassword(register.Password, newOperator.PasswordSalt);
                    newOperator.Services = newServices.Id;
                    newOperator.IsCompany = register.IsCompany;
                    newOperator.LocationCity = register.LocationCity;
                    newOperator.OperatingRadius = register.OperatingRadius;
                    newOperator.IsActice = false;
                    _context.Operators.Add(newOperator);
                    _context.SaveChanges();

                    return newOperator;
                }


            }

            return null ;
        }

        public User RegisterUser(RegisterUserModel register)
        {
            if (_context.Operators.FirstOrDefault(m => m.Email == register.Email) == null && _context.Users.FirstOrDefault(m => m.Email == register.Email) == null)
            {
                if (register.Password == register.RepeatPassword)
                {
                    var newUser = new User();
                    newUser.Username = register.Username;
                    newUser.Email = register.Email;
                    newUser.PhoneNumber = register.PhoneNumber;
                    newUser.PasswordSalt = PasswordHash.GenerateSalt();
                    newUser.HashedPassword = PasswordHash.HashPassword(register.Password, newUser.PasswordSalt);
                    newUser.IsActice = false;
                    _context.Users.Add(newUser);
                    _context.SaveChanges();

                    return newUser;
                }


            }
            return null;
        }


        public bool ActivateUser(int id) { 
            User userToActivate= _context.Users.FirstOrDefault(m=>m.Id== id);
            if (userToActivate != null)
            {
                userToActivate.IsActice= true;
                _context.SaveChanges();

                return true;
            }

            return false;
        }


        public bool ActivateOperator(int id)
        {
            Operator operatorToActivate = _context.Operators.FirstOrDefault(m => m.Id == id);
            if (operatorToActivate != null)
            {
                operatorToActivate.IsActice = true;
                _context.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
