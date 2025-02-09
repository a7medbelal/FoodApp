using AutoMapper;
using FoodApp.Core.Entities;


namespace FoodApp.Core.ViewModle.Profiles
{
    public class FaviortProfile : Profile
    {
        public FaviortProfile() {
            CreateMap<AddFaviortViewModel, Favioret>();
            CreateMap<Favioret, FavUserViewModel>().
              ForMember(dst=> dst.name , opt => opt.MapFrom(s=> s.Recipe.Item.EnName)).
              ForMember(dst=> dst .descrption , opt=> opt.MapFrom(c=> c.Recipe.Description))
              .ForMember(dst => dst.price, opt => opt.MapFrom(c => c.Recipe.price))
              .ForMember(dst => dst.CategoryName, opt => opt.MapFrom(c => c.Recipe.Category.Name));    
        
        
        
        }

    }
}
