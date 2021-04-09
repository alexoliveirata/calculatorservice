using System.ComponentModel.DataAnnotations;

namespace CalculatorService.Server.Dto
{
    public class SubDto
    {
        [Required]
        public int Minuend { get; set; }
        public int Subtrahend { get; set; }
    }
}
