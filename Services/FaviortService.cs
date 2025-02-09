using AutoMapper.Configuration.Annotations;


using FoodApp.Core.Helper;
using FoodApp.Core.ViewModle;
using FoodApp.Core.ViewModle.RecpieViewModle;
using FoodApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using FoodApp.Core.interfaces;
using FoodApp.Core.Enums;
using FoodApp.Core.FoodApp.Service;
using FoodApp.Core.interfaces.RecipeService;
using FoodApp.Core;

namespace FoodApp.Services
{
    public class FaviortService : IFaviortService
    {
        private readonly IRepository<Favioret> _repository;

        private readonly IRecpieService _recpieService;

        private UserManager<ApplicationUser> _userManager;
        private readonly IAuthenticationsService _authenticationsService;


        public FaviortService(IRepository<Favioret> repository, UserManager<ApplicationUser> userManager, IRecpieService recpieService, IAuthenticationsService service)
        {
            _repository = repository;
            _userManager = userManager;
            _recpieService = recpieService;
            _authenticationsService = service;
        }


        public async Task<ResponsiveView<bool>> Addfaviort(AddFaviortViewModel viewModel)
        {
            var ExistUser = await _userManager.FindByIdAsync(viewModel.UserID);

            if (ExistUser == null) return new FailerResView<bool>(Errorcode.usersnotExits, "there is no user with this id");

            var recipe = _recpieService.RecpieDetiles(viewModel.RecipeID);
            if (!recipe.IsSuccess) return new FailerResView<bool>(Errorcode.unfoundData, "there is no  recipe with this id");


            var ExistFav = _repository.Any(c => c.UserID == viewModel.UserID && c.RecipeID == viewModel.RecipeID);

            if (ExistFav) return new FailerResView<bool>(Errorcode.existsFavList, "already add to fav with this user");

            var fav = viewModel.MapTo<Favioret>();
            _repository.Add(fav);
            _repository.SaveChanges();

            return new SuccessResView<bool>(true);
        }

        public async Task<ResponsiveView<bool>> DeleteRecipFromFavaiort(int Faviortid)
        {
            var existList = await _repository.Get(e => e.ID == Faviortid).FirstOrDefaultAsync();

            if (existList is null) return new FailerResView<bool>(Errorcode.existsFavList, "there is no list with this id ");

            _repository.Delete(existList);

            return new SuccessResView<bool>(true);
        }

        public async Task<ResponsiveView<IQueryable<FavUserViewModel>>> GetFaviortsForUser()
        {
            var ExistUser = await _authenticationsService.GetUserIdFromToken();

            if (string.IsNullOrEmpty(ExistUser.data)) return new FailerResView<IQueryable<FavUserViewModel>>(ExistUser.ErrorCode, ExistUser.Massage);

            if (ExistUser == null) return new FailerResView<IQueryable<FavUserViewModel>>(Errorcode.usersnotExits, "there is no user with this id");

            var recipes = _repository.GetAll();

            if (!recipes.Any()) return new FailerResView<IQueryable<FavUserViewModel>>(Errorcode.emptyData, "no recipes added to this list");

            var favRecipes = recipes.ProjectTo<FavUserViewModel>();

            return new SuccessResView<IQueryable<FavUserViewModel>>(favRecipes);

        }

        public async Task<ResponsiveView<Pagination<FavUserViewModel>>> GetFavResipesByPagination(FavParames parameter)
        {

            var favRecpie = await GetFaviortsForUser();
            var filterRecipe = favRecpie.data;

            if (!favRecpie.data.Any()) return new FailerResView<Pagination<FavUserViewModel>>(favRecpie.ErrorCode, favRecpie.Massage);

            if (!string.IsNullOrEmpty(parameter.name)) filterRecipe = filterRecipe.Where(r => r.name.Contains(parameter.name));

            if (parameter.price > 0) filterRecipe = filterRecipe.Where(r => r.price == parameter.price);

            if (!string.IsNullOrEmpty(parameter.CategoryName)) filterRecipe = filterRecipe.Where(r => r.CategoryName.Contains(parameter.CategoryName));



            var PaginatResult = await Pagination<FavUserViewModel>.ToPagedList(filterRecipe, parameter.PageSize, parameter.PageNumber);


            return new SuccessResView<Pagination<FavUserViewModel>>(PaginatResult);
        }
    }
}
