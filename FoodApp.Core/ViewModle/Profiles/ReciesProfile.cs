using AutoMapper;
using FoodApp.Core.ViewModle.RecpieViewModle;
using FoodApp.Core.Entities;

namespace FoodApp.Core.ViewModle.Profiles
{
    public class RecipesProfile : Profile
    {

        public RecipesProfile() {

            CreateMap<RecpieCreateViewModel, Recipe>().
                ForPath((dst=> dst.Item.EnName),(opt=>opt.MapFrom(so=> so.Name)))
                .ForMember(d=>d.Tags, opt=> opt.
                MapFrom(s => s.TagId.
                Select(c => new RecipeTag {TagId=c})));

            CreateMap<Recipe, RecpieDetailsViewModel>()
                .ForMember(dst => dst.Title, opt => opt.MapFrom(src => src.Item.EnName))
                .ForMember(dst => dst.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dst => dst.tagname, opt => opt.MapFrom(src =>
                 src.Tags.Select(tag => tag.Tags.Name).ToList()));


             CreateMap<UpdateRecipeViewMode, Recipe>().
                ForPath(dst => dst.Item.EnName, opt => opt.MapFrom(sou => sou.Name));


            CreateMap<Recipe, RecipeViewModel>()
               .ForMember(dst => dst.categoryname, opt => opt.MapFrom(s => s.Category.Name));
              
        }

    }
}
