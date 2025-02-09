using FoodApp.Core.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FoodApp.Core.ViewModle
{
    public class ResponsiveView<T>
    {
        public T data { get; set; }

        public bool IsSuccess { get; set; }

        public Errorcode ErrorCode { get; set; }

        public string Massage { get; set; }

    }

    public class SuccessResView<T> : ResponsiveView<T>
    {

        public SuccessResView(T Data, string message = " ")
        {


            data = Data;
            IsSuccess = true;
            Massage = message;
            ErrorCode = Errorcode.None;
        }
    }


    public class FailerResView<T> : ResponsiveView<T>
    {

        public FailerResView(Errorcode errorCode, string message = " ")
        {
            data = default;
            IsSuccess = false;
            Massage = message;
            ErrorCode = errorCode;
        }
    }
}

