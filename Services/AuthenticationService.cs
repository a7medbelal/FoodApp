
using FoodApp.Core.ViewModle;
using FoodApp.Core.ViewModle.authVIewModel;
using FoodApp.Core.Entities;
using FoodApp.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

using System.Data;

using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using FoodApp.Core.interfaces;
using FoodApp.Services.MailService;
using FoodApp.Core;


namespace FoodApp.Services
{
    public class AuthenticationService : IAuthenticationsService
    {
        private UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _Context;

        public AuthenticationService(UserManager<ApplicationUser> user, RoleManager<IdentityRole> role, IOptions<JWT> jWT, IEmailService mailService, IHttpContextAccessor context)
        {
            userManager = user;
            _roleManager = role;
            _jwt = jWT.Value;
            _emailService = mailService;
            _Context = context;
        }

        public async Task<ResponsiveView<AuthModel>> RegisterAsync(RegistrationViewModel registrationView)
        {
            var cheak = await userManager.FindByEmailAsync(registrationView.Email);
            if (cheak != null)
                return new FailerResView<AuthModel>(Errorcode.userexist);

            if (await userManager.FindByNameAsync(registrationView.Username) is not null)
                return new FailerResView<AuthModel>(Errorcode.userexist);

            // default  role is user 
            var rolee = Role.User;


            var user = registrationView.MapTo<ApplicationUser>();
            user.roles = rolee;
            var result = await userManager.CreateAsync(user, registrationView.Password);


            if (!result.Succeeded)
            {
                var error = new StringBuilder();
                foreach (var item in result.Errors)
                {

                    error.Append($"{item.Description}, ");

                }
                return new FailerResView<AuthModel>(Errorcode.registerfaild, error.ToString());
            }

            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

            var sendEmail =  _emailService.SendEmailAsync(user.Email, "the otp for confirmation your mail ", $"{token}"); 

            var userRole = user.roles;
            var jwtSecurityToken = await CreateJwtToken(user, userRole);

            return new SuccessResView<AuthModel>(new AuthModel
            { 
                Username = user.UserName,
                 Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            }, "ples check your mail ");
        }


        public async Task<ResponsiveView<bool>> ForgetPassword(string Email)
        {


            var userExit = await userManager.FindByEmailAsync(Email);

            if (userExit == null) return new FailerResView<bool>(Errorcode.usersnotExits, "this email doesnt in system ples regihster frist");

            var token = await userManager.GeneratePasswordResetTokenAsync(userExit);

            await _emailService.SendEmailAsync(userExit.Email, "Password Reset Code", $"Your password reset code is: {new { userExit.Email, token }}. This code is valid for 1 minute.");

            return new SuccessResView<bool>(true, "check your email ples");
        }



        public async Task<ResponsiveView<bool>> ResetPassword(ResetPasswordModel model)
        {
            var userEXsit = await userManager.FindByEmailAsync(model.email);

            if (userEXsit is null) return new FailerResView<bool>(Errorcode.usersnotExits, "user deosnt exist ples uptodate user ");

            var resetPassword = await userManager.ResetPasswordAsync(userEXsit, model.Token, model.Password);

            if (!resetPassword.Succeeded)
            {
                return new FailerResView<bool>(Errorcode.unfoundData);
            }
            return new SuccessResView<bool>(true, "the password is change sucessfualy ");
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user, Role role)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roleClaims = new List<Claim>();

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 new Claim("roletype" ,((int)role).ToString()) ,
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            }
            .Union(userClaims);


            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public async Task<ResponsiveView<AuthModel>> loginAsync(LoginView model)
        {
            var authModel = new AuthModel();

            var user = await userManager.FindByEmailAsync(model.Email);

            if (user is null || !await userManager.CheckPasswordAsync(user, model.Password))
            {
                return new FailerResView<AuthModel>(Errorcode.loginfaild, "password or eamil in correct");
            }

            if(!user.EmailConfirmed) return new FailerResView<AuthModel>(Errorcode.loginfaild, "ples confirm your email first");

            var jwtSecurityToken = await CreateJwtToken(user, user.roles);


            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            //authModel.Roles = ((int)user.roles).ToString();
            authModel.Username = user.UserName;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            return new SuccessResView<AuthModel>(authModel);
        }

        public async Task<ResponsiveView<IEnumerable<UserViewModel>>> GetAllUser()
        {
            IQueryable<ApplicationUser> users = (IQueryable<ApplicationUser>)await userManager.Users.ToListAsync();

            if (users.Count() == 0) return new FailerResView<IEnumerable<UserViewModel>>
                      (Errorcode.usersnotExits, "no user in the system");

            var usersview = users.ProjectTo<UserViewModel>();

            return new SuccessResView<IEnumerable<UserViewModel>>(usersview, "list of all users in the systenm ");
        }

        public async Task<ResponsiveView<string>> GetUserIdFromToken()
        {
            var user = _Context.HttpContext?.User;

            if (user == null) return new FailerResView<string>(Errorcode.usersnotExits, "there is no fav recipe for this user");

            var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return new SuccessResView<string>(id);
        }


        public async Task<ResponsiveView<bool>> EmailConfirmtion(string token)
        {

             var UserId = await GetUserIdFromToken();

            var UserExist = await userManager.FindByIdAsync(UserId.data);

            if (UserExist == null) return new FailerResView<bool>(Errorcode.usersnotExits, "there is no user with id ");

            var confirmEmail = await userManager.ConfirmEmailAsync(UserExist, token);

            return !confirmEmail.Succeeded ? 
                new FailerResView<bool>(Errorcode.Mailerror, "somthing happen on confirmation ") :
                new SuccessResView<bool>(true, "email confirmation sussfulay ");  
        }

    }
}
