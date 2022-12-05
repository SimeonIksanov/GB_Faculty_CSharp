using System.ComponentModel.DataAnnotations;

namespace MVC.ValidationAttributes
{
    public class MyDateTimeAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is not DateTime)
                return true;
            DateTime dt = (DateTime)value;
            if (dt >= DateTime.Now)
                return true;
            else
                return false;
        }
    }
}
