using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.Dto
{
    public class VillaNumberCreateDto
    {
        [Required]
        //[MinLength(5)]
        public int VillaNo { get; set; }
       // [MinLength(10,ErrorMessage ="Length must be greater than 10")]
        [StringLength(30, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = 6)]
        public string SpecialDetails { get; set; }
        [Required]
        public int VillaId { get; set; }
    }
}
