using FotoWorldBackend.Models;


namespace FotoWorldBackend.Services.Auth
{
    public interface IAuthService
    {
        User LoginUser(LoginModel login);

        User RegisterUser(RegisterUserModel register);

        Operator RegisterOperator(RegisterOperatorModel register);

        bool ActivateUser(int id);

        bool ActivateOperator(int id);
    }
}
