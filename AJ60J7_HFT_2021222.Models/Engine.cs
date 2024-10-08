﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AJ60J7_HFT_2021222.Models
{
    [Table("engines")]
    public class Engine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Type { get; set; } //Diesel, Petrol, Hybrid, Electric..
        public int Horsepower { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Car Car { get; set; }

        [ForeignKey(nameof(Car))]
        public int CarId { get; set; }

    }
}
