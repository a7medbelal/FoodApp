using FoodApp.Core.ViewModle;
using FoodApp.Core.ViewModle.authVIewModel; 

namespace FoodApp.Core.interfaces
{
    public interface IAuthenticationsService
    {
        Task<ResponsiveView<AuthModel>> RegisterAsync(RegistrationViewModel registrationView);
        Task<ResponsiveView<AuthModel>> loginAsync(LoginView login);
        Task<ResponsiveView<IEnumerable<UserViewModel>>> GetAllUser();
        Task<ResponsiveView<bool>> ForgetPassword(string Email);
        Task<ResponsiveView<bool>> ResetPassword(ResetPasswordModel model);
        Task<ResponsiveView<string>> GetUserIdFromToken();
        Task<ResponsiveView<bool>> EmailConfirmtion(string token);
        Task<ResponsiveView<AuthModel>> ChangeRole(string id); 

    }
}
