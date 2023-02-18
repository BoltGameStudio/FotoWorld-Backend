using FotoWorldBackend.Models;


namespace FotoWorldBackend.Services.Auth
{
    public interface IAuthService
    {
        bool Login(LoginModel login);

        User RegisterUser(RegisterUserModel register);

        Operator RegisterOperator(RegisterOperatorModel register);
    }
}
