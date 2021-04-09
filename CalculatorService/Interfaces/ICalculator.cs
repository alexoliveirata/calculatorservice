using CalculatorService.Server.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CalculatorService.Server.Interfaces
{
    public interface ICalculator
    {
        /// <summary>
        /// Compute the addition of two or more operands
        /// </summary>
        /// <param name="sum">The numbers to sum</param>
        /// <param name="trackingId">TrackingId to identify this operation</param>
        /// <returns></returns>
        Task<SumResponseDto> Addition(SumDto sum, string trackingId);

        /// <summary>
        /// Compute the subtraction of two operands
        /// </summary>
        /// <param name="sub">The minuend and subtrahend numbers</param>
        /// <param name="trackingId">TrackingId to identify this operation</param>
        /// <returns></returns>
        Task<SubResponseDto> Subtraction(SubDto sub, string trackingId);

        /// <summary>
        /// Compute the multiplication of two or more operands
        /// </summary>
        /// <param name="mult">The numbers to multiplicate</param>
        /// <param name="trackingId">TrackingId to identify this operation</param>
        /// <returns></returns>
        Task<MultResponseDto> Multiplication(MultDto mult, string trackingId);

        /// <summary>
        /// Compute the division of two operands
        /// </summary>
        /// <param name="div">The dividend and divisor numbers</param>
        /// <param name="trackingId">TrackingId to identify this operation</param>
        /// <returns></returns>
        Task<DivResponseDto> Division(DivDto div, string trackingId);

        /// <summary>
        /// Compute the squre root of an operand
        /// </summary>
        /// <param name="sqr">Number to calculate the square root</param>
        /// <param name="trackingId">TrackingId to identify this operation</param>
        /// <returns></returns>
        Task<SqrtResponseDto> SquareRoot(SqrtDto sqr, string trackingId);

        /// <summary>
        /// Review it's history of requested operations (issued with a common Tracking­Id)
        /// </summary>
        /// <param name="trackingId">The common trackingId</param>
        /// <returns>A collection with calculations results</returns>
        List<JournalResponseDto> JournalQuery(string trackingId);
    }
}