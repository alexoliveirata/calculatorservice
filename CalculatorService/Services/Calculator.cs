using CalculatorService.Server.Dto;
using CalculatorService.Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorService.Server.Services
{
    public class Calculator : ICalculator
    {
        #region Global

        private readonly IJournal _journal;

        #endregion Global

        #region Constructor

        public Calculator(IJournal journal)
        {
            _journal = journal;
        }

        #endregion Constructor

        #region Public methods

        public async Task<SumResponseDto> Addition(SumDto sum, string trackingId)
        {
            var result = sum.Addends.Sum();
            var response = new SumResponseDto { Sum = result };

            await _journal.AddJournalOperation(EnumOperationsType.Sum, sum, response, trackingId);

            return response;
        }

        public async Task<SubResponseDto> Subtraction(SubDto sub, string trackingId)
        {
            var result = (Math.Abs(sub.Minuend) - Math.Abs(sub.Subtrahend));
            var response = new SubResponseDto { Difference = result };

            await _journal.AddJournalOperation(EnumOperationsType.Sub, sub, response, trackingId);

            return response;
        }

        public async Task<MultResponseDto> Multiplication(MultDto mult, string trackingId)
        {
            var result = 1;

            foreach (var item in mult.Factors)
            {
                result *= item;
            }

            var response = new MultResponseDto { Product = result };

            await _journal.AddJournalOperation(EnumOperationsType.Mul, mult, response, trackingId);

            return response;
        }

        public async Task<DivResponseDto> Division(DivDto div, string trackingId)
        {
            int remainder;
            var result = Math.DivRem(div.Dividend, div.Divisor, out remainder);
            var response = new DivResponseDto { Quotient = result, Remainder = remainder };

            await _journal.AddJournalOperation(EnumOperationsType.Div, div, response, trackingId);

            return response;
        }

        public async Task<SqrtResponseDto> SquareRoot(SqrtDto sqr, string trackingId)
        {
            var result = Math.Sqrt(sqr.Number);
            var response = new SqrtResponseDto { Square = result };

            await _journal.AddJournalOperation(EnumOperationsType.Sqr, sqr, response, trackingId);

            return response;
        }

        public List<JournalResponseDto> JournalQuery(string trackingId)
        {
            return _journal.JournalQuery(trackingId);
        }

        #endregion Public methods
    }
}