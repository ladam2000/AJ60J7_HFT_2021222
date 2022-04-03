using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AJ60J7_HFT_2021222.Models
{
    [Table("brands")]
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        [NotMapped]
        public virtual ICollection<Car> Cars { get; set; }

        public Brand()
        {
            Cars = new HashSet<Car>();
        }
    }
}
