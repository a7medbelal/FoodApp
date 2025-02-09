namespace FoodApp.Core.Enums
{
    public enum features
    {
        //for recipe
        GetallRecipe=0,
        GetAllForAdmin,
        updateRecipe,
        detils,
        deleteRecipe,
        CreateRecipe,

        //forCategory
        GetAllCategory=10, 
        GetCategoryByID,
        CreateCategory,
        updateCategory,
        DeleteCategory, 


       //forFaviortRecipe
       AddFavrot = 20 , 
       getFavRecipe ,
       GetFavRecieWithPagination,


       //for Users
       ConfirmEmail=200,
        ChangeRole = 201,
    }
}
