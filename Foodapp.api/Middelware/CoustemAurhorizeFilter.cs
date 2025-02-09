
using FoodApp.Core.FoodApp.Service.RoleService;
using FoodApp.Core.Enums;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using System.Security.Cryptography.Xml;

namespace FoodApp.Core.Helper
{
    public class CoustemAurhorizeFilter : ActionFilterAttribute
    {
        IRoleService _roleService;

        features _features; 

         public CoustemAurhorizeFilter( IRoleService roleService, features features)
         {
            _roleService = roleService;
            _features = features;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var calims = context.HttpContext.User;

            var RoleID = calims.FindFirst("roletype");

            if (RoleID == null || string.IsNullOrEmpty(RoleID.Value)) {

                throw new UnauthorizedAccessException(); 
            }
        
           var role  = (Role) int.Parse(RoleID.Value);

            var check = _roleService.HasAccess(role , _features );

            if (check == null) throw new UnauthorizedAccessException();


            base.OnActionExecuted(context);
        }

     

    }
}
