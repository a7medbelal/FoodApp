using AutoMapper;

namespace FoodApp.Core 
{ 
    public static  class AutomapperService
    {
        public  static IMapper mapper;
        // this function make my code more readablity and clean 
        // it help to make easy to change from mapping libaray to anthoer tecnology 
        public static IQueryable<T> ProjectTo<T>(this IQueryable<object> source) {

            return mapper.ProjectTo<T>(source); 
        
        }
        public static T MapToFristOrDeafult<T>(this IQueryable<object> source)
        { 
            return mapper.ProjectTo<T>(source).FirstOrDefault(); 
        }

        public static T MapTo<T>(this object source)
        {
            
            
            

            return mapper.Map<T>(source); 
        }
        

    }
}
