using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.Dto
{
    public class VillaDtoUpdate
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        public string Details { get; set; }
        [Required]
        public double Rate { get; set; }
        [Required]
        public int SQft { get; set; }
        [Required]
        public int Occupancy { get; set; }
        public string Amenity { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}
