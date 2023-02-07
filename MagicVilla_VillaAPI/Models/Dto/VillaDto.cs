﻿using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.Dto
{
    public class VillaDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        public string Details { get; set; }
        [Required]
        public double Rate { get; set; }
        public int SQft { get; set; }
        public int Occupancy { get; set; }
        public string Amenity { get; set; }
        public string ImageUrl { get; set; }
    }
}
