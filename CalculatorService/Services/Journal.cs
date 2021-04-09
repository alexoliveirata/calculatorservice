using AutoMapper;
using CalculatorService.Server.Dto;
using CalculatorService.Server.Interfaces;
using Data.Repository.Interfaces;
using Data.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CalculatorService.Server.Services
{
    public class Journal : IJournal
    {
        #region Global

        private readonly ICalculatorRepository _calculatorRepository;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        #endregion Global

        #region Constructor

        public Journal(ICalculatorRepository calculatorRepository, ILoggerManager loggerManager, IMapper mapper)
        {
            _calculatorRepository = calculatorRepository;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        #endregion Constructor

        #region Public methods

        public async Task AddJournalOperation(EnumOperationsType operationType, object objCalculation, object objResult, string trackingId)
        {
            try
            {
                if (!string.IsNullOrEmpty(trackingId))
                {
                    var calculation = PrepareCalculationToJournal(operationType, objCalculation, objResult);

                    var operations = new Operations
                    {
                        TrackingId = trackingId,
                        Operation = operationType.ToString(),
                        Calculation = calculation
                    };

                    await _calculatorRepository.Insert(operations);
                }
            }
            catch (System.Exception ex)
            {
                _loggerManager.LogError("InternalErrorException on calls 'Add' post method on CalculatorController", ex);
            }
        }

        public List<JournalResponseDto> JournalQuery(string trackingId)
        {
            var response = new List<JournalResponseDto>();

            try
            {
                List<Operations> result = _calculatorRepository.Read(trackingId);
                response = _mapper.Map<List<JournalResponseDto>>(result);

                return response;
            }
            catch (System.Exception ex)
            {
                _loggerManager.LogError("InternalErrorException on calls 'JournalQuery' method on Journal service", ex);
            }

            return response;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Prepare the information of the operation to be save into the journal
        /// </summary>
        /// <param name="operationType">Type of the operation</param>
        /// <param name="objCalculation">The object used to calculate</param>
        /// <param name="objResult">The object with the result of the calculation</param>
        /// <returns></returns>
        private string PrepareCalculationToJournal(EnumOperationsType operationType, object objCalculation, object objResult)
        {
            string operation = string.Empty;

            switch (operationType)
            {
                case EnumOperationsType.Sum:
                    var sum = (SumDto)objCalculation;
                    var sumResult = (SumResponseDto)objResult;
                    operation = string.Concat(string.Join(" + ", sum.Addends), " = ", sumResult.Sum);
                    break;

                case EnumOperationsType.Sub:
                    var sub = (SubDto)objCalculation;
                    var subResult = (SubResponseDto)objResult;
                    operation = string.Concat(sub.Minuend, " - ", sub.Subtrahend, " = ", subResult.Difference);
                    break;

                case EnumOperationsType.Div:
                    var div = (DivDto)objCalculation;
                    var divResult = (DivResponseDto)objResult;
                    operation = string.Concat(div.Dividend, " / ", div.Divisor, " = ", divResult.Quotient, " remainder ", divResult.Remainder);
                    break;

                case EnumOperationsType.Mul:
                    var mul = (MultDto)objCalculation;
                    var mulResult = (MultResponseDto)objResult;
                    operation = string.Concat(string.Join(" + ", mul.Factors), " = ", mulResult.Product);
                    break;

                case EnumOperationsType.Sqr:
                    var sqr = (SqrtDto)objCalculation;
                    var sqrResult = (SqrtResponseDto)objResult;
                    operation = string.Concat("Square root of ", sqr.Number, " is ", sqrResult.Square);
                    break;

                default:
                    break;
            }

            return operation;
        }

        #endregion Private methods
    }
}
