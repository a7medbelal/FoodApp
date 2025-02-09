
using FoodApp.Core.Enums;
using FoodApp.Core.Helper;
using FoodApp.Core.interfaces;
using FoodApp.Core.ViewModle.authVIewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;

namespace FoodApp.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class authenController : ControllerBase
    {
        private readonly IAuthenticationsService authenticationService; 

        public authenController(IAuthenticationsService authenticationService)
        {
             this.authenticationService = authenticationService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await authenticationService.RegisterAsync(model);

            if (!result.IsSuccess)
                return BadRequest(result.Massage);
            return Ok(new { massage = result.Massage, token = result.data.Token , expireson = result.data.ExpiresOn });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginView model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await authenticationService.loginAsync(model);

            if (!result.IsSuccess)
                return BadRequest(result.Massage);

            return Ok(new  {token = result.data.Token , role = result.data.Roles ,  expiresdate = result.data.ExpiresOn});
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> FrogetPassord(string email)
        {
            if (string.IsNullOrEmpty(email)) return BadRequest("ples enter your email ");

            var response = await  authenticationService.ForgetPassword(email); 

            return Ok(response);

        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassord(ResetPasswordModel model)
        {
            if (string.IsNullOrEmpty(model.Token)) return BadRequest("ples check the token correctness");

            var response = await authenticationService.ResetPassword(model); 


            return Ok(response);

        }


        [HttpPut("Confirm Email")]
        [Authorize]
        [TypeFilter(typeof(CoustemAurhorizeFilter), Arguments = new object[] { features.ConfirmEmail})]

        public async Task<IActionResult>EmailConfirmation(string OTP)
        {
            if (string.IsNullOrEmpty(OTP)) return BadRequest("ples put the OTP"); 


            var response =await  authenticationService.EmailConfirmtion(OTP);
            
            return Ok(response);
        }

    }
}
