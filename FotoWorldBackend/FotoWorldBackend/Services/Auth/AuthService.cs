﻿using FotoWorldBackend.Models;
using FotoWorldBackend.Services.Email;
using FotoWorldBackend.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace FotoWorldBackend.Services.Auth
{
    /// <summary>
    /// Provides operations on database for auth controller
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly FotoWorldContext _context;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;
        public AuthService(FotoWorldContext context, IConfiguration config )
        {
            _context = context;
            _config = config;
        }


        public User LoginUser(LoginModel login)
        {
            var user = _context.Users.FirstOrDefault(m => m.Email == login.Username || m.Username == login.Username);
            if (user != null)
            {
                if (user.IsActive)
                {
                    if (PasswordHash.VerifyPassword(login.Password, user))
                    {
                        return user;
                    }
                }

            }
            return null;
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
                    newUser.PasswordSalt = PasswordHash.GenerateSalt();
                    newUser.HashedPassword = PasswordHash.HashPassword(register.Password, newUser.PasswordSalt);
                    newUser.IsOperator = register.isOperator;
                    newUser.IsActive = false;
                    _context.Users.Add(newUser);
                    _context.SaveChanges();

                    if (newUser.IsOperator)
                    {
                        var newOperator = new Operator();
                        newOperator.AccountId = newUser.Id;
                        newOperator.IsCompany = register.IsCompany;
                        newOperator.Availability = register.Availability;
                        newOperator.LocationCity = register.LocationCity;
                        newOperator.OperatingRadius = register.OperatingRadius;
                        newOperator.Photo = register.PhotoService;
                        newOperator.DronePhoto = register.DronePhotoService;
                        newOperator.Filming = register.FilmingService;
                        newOperator.DroneFilming = register.DroneFilmService;

                        _context.Operators.Add(newOperator);
                        _context.SaveChanges();

                    }

                    return newUser;
                }


            }
            return null;
        }


        public bool ActivateUser(int id)
        {
            User userToActivate = _context.Users.FirstOrDefault(m => m.Id == id);
            if (userToActivate != null)
            {
                userToActivate.IsActive = true;
                _context.SaveChanges();

                return true;
            }

            return false;
        }




       public bool RestartPassword(int userID, string newPassword)
       {
            User user= _context.Users.FirstOrDefault(m=> m.Id== userID);
            if(user != null)
            {
                user.PasswordSalt = PasswordHash.GenerateSalt();
                user.HashedPassword = PasswordHash.HashPassword(newPassword, user.PasswordSalt);
                _context.SaveChanges();
                return true;
            }
            return false;
       }



        public User GetUserByMail(string email) {
            var user=_context.Users.FirstOrDefault(m => m.Email == email);
            if (user != null)
            {
                return user;
            }
            return null;
        }
    }
}
