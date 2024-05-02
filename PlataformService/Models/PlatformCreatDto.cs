using System.ComponentModel.DataAnnotations;

namespace PlataformService.Models
{
    public class PlatformCreatDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        public string Cost { get; set; }
    }
}
