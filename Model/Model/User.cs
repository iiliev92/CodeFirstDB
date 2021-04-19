using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(75)]
        [Index]
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Age must be a positive number")]
        public int? Age { get; set; }

        [Required]
        public virtual ICollection<Product> Products { get; set; }
    }
}
