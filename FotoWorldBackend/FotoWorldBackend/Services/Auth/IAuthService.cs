﻿using FotoWorldBackend.Models;


namespace FotoWorldBackend.Services.Auth
{
    public interface IAuthService
    {
        User LoginUser(LoginModel login);

        User RegisterUser(RegisterUserModel register);

        bool ActivateUser(int id);

        User GetUserByMail(string email);

        bool RestartPassword(int userID, string newPassword);
    }
}
