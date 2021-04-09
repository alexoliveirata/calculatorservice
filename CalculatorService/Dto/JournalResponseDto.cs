using System;

namespace CalculatorService.Server.Dto
{
    public class JournalResponseDto
    {
        public string Operation { get; set; }
        public string Calculation { get; set; }

        public DateTime Date { get; set; }
    }
}
