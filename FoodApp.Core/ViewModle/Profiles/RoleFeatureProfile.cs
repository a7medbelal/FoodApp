
using AutoMapper;
using FoodApp.Core.Entities;
using FoodApp.Core.ViewModle.authVIewModel;

namespace FoodApp.Core.ViewModle.Profiles
{
    public class RoleFeatureProfile : Profile 
    {
         public RoleFeatureProfile() { 
         
          CreateMap<RoleFeatureViewModel , RoleFeature> (); 


        
        }

    }
}
