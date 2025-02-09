
using FoodApp.Core.Helper;
using FoodApp.Core.ViewModle;

namespace FoodApp.Core.interfaces
{
    public interface IFaviortService
    {
        Task<ResponsiveView<bool>> Addfaviort(AddFaviortViewModel viewModel); 
        Task<ResponsiveView<IQueryable<FavUserViewModel>>> GetFaviortsForUser();
        Task<ResponsiveView<Pagination<FavUserViewModel>>>GetFavResipesByPagination(FavParames parameter); 
        Task<ResponsiveView<bool>> DeleteRecipFromFavaiort(int id);
    }
}
