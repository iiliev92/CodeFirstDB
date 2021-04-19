using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller.Events
{
    public delegate void BrandReadHandler(BrandEventsArgs brandEventsArgs);
    public delegate void BrandReadAllHandler(List<BrandEventsArgs> brandEventsArgs);

    public class BrandEventsContext
    {
        public event BrandReadHandler OnBrandReadCompleted;
        public event BrandReadAllHandler OnBrandReadAllCompleted;

        public Delegate GetDelegate()
        {
            Delegate function = OnBrandReadCompleted.GetInvocationList()[0];

            if (function != null)
            {
                return function;
            }

            throw new MissingMethodException("There is no function attached to the event yet!");
        }

    }
}
