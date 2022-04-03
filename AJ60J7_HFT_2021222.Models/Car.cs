using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJ60J7_HFT_2021222.Models
{
    [Table("cars")]
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public string Model { get; set; }

        public int? BasePrice { get; set; }

        [NotMapped]
        
        public virtual Brand Brand { get; set; }

        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }

        [NotMapped]
        public virtual Engine Engine { get; set; }
        public int EngineId { get; set; }


    }
}
