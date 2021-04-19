using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller.Validation
{
    public static class ValidationManager
    {
        public static bool ValidateString(string value)
        {
            return !(string.IsNullOrEmpty(value));
        }

        public static bool ValidatePositiveNumber(int number)
        {
            return number > 0 ? true : false;
        }

        public static bool ValidateCollection<T>(ICollection<T> collection)
        {
            return collection.Count != 0 ? true : false;
        }

    }
}
