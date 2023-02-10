﻿using FotoWorldBackend.Models;
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

        //jezeli gosc jest operatorem to ma zwykle konto wiec go zalogujmy jako zwykle konto a potem mu zrobimy odpwiedni token na bazie tego co zaznaczy

        public bool Login(LoginModel login)
        {
 
            
            var user = _context.Users.FirstOrDefault(m => m.Email == login.Username);
            if (user != null)
            {
                return _passwordHash.VerifyPassword(login.Password, user);
            }
            return false;
        }

        public bool RegisterOperator(RegisterOperatorModel register)
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
                    newServices.Filming= register.FilmingService;
                    _context.OperatorServices.Add(newServices);
                    _context.SaveChanges();

                    
                    newOperator.Username = register.Username;
                    newOperator.Email = register.Email;
                    newOperator.PhoneNumber = register.PhoneNumber;
                    newOperator.PasswordSalt = _passwordHash.GenerateSalt();
                    newOperator.HashedPassword = _passwordHash.HashPassword(register.Password, newOperator.PasswordSalt);
                    newOperator.Services = newServices.Id;
                    newOperator.IsCompany= register.IsCompany;
                    newOperator.LocationCity= register.LocationCity;
                    newOperator.OperatingRadius = register.OperatingRadius;

                    _context.Operators.Add(newOperator);
                    _context.SaveChanges();

                    
                    newUser.Username = newOperator.Username;
                    newUser.Email = newOperator.Email;
                    newUser.PhoneNumber = newOperator.PhoneNumber;
                    newUser.PasswordSalt = newOperator.PasswordSalt;
                    newUser.HashedPassword = newOperator.HashedPassword;
                    _context.Users.Add(newUser);
                    _context.SaveChanges();
                    return true;
                }


            }
            
            return false;
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