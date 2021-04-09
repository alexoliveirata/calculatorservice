using CalculatorService.Server.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CalculatorService.Server.Interfaces
{
    public interface IJournal
    {
        /// <summary>
        /// Add calculation to Journal
        /// </summary>
        /// <param name="operationsType">Type of the operation</param>
        /// <param name="objCalculation">The object used to calculation</param>
        /// <param name="objResult">The object with the result of the operation</param>
        /// <param name="trackingId">Id of the tracking</param>
        Task AddJournalOperation(EnumOperationsType operationsType, object objCalculation, object objResult, string trackingId);

        /// <summary>
        /// Return a list of all the calculations operations saved
        /// </summary>
        /// <param name="trackingId">Id of the register</param>
        /// <returns></returns>
        List<JournalResponseDto> JournalQuery(string trackingId);
    }
}
