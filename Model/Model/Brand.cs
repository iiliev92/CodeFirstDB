using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class Brand
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [MaxLength(30)]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
