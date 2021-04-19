using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller.Events
{
    public class BrandEventsArgs : EventArgs
    {
        public int ID { get; private set; }

        public string Name { get; private set; }

        public ICollection<Product> Products { get; private set; }

        public BrandEventsArgs(int id, string name, ICollection<Product> products)
        {
            ID = id;
            Name = name;
            Products = products;
        }

    }
}
