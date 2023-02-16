using FotoWorldBackend.Models;


namespace FotoWorldBackend.Services.Auth
{
    public interface IAuthService
    {
        bool Login(LoginModel login);

        bool RegisterUser(RegisterUserModel register);

        bool RegisterOperator(RegisterOperatorModel register);
    }
}
