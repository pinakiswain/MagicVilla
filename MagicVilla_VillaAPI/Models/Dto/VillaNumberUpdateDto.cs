using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.Dto
{
    public class VillaNumberUpdateDto
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public string SpecialDetails { get; set; }
        [Required]
        public int VillaId { get; set; }
    }
}
