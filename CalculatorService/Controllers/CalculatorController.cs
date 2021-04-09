using CalculatorService.Server.Dto;
using CalculatorService.Server.ExceptionHandler;
using CalculatorService.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CalculatorService.Controllers
{
    [Route("calculator")]
    [ApiController]
    public class CalculatorController : Controller
    {
        private readonly ICalculator _calculator;
        private readonly ILoggerManager _loggerManager;

        public CalculatorController(ICalculator calculator, ILoggerManager loggerManager)
        {
            _calculator = calculator;
            _loggerManager = loggerManager;
        }

        [HttpPost]
        [Route("add")]
        public async Task<SumResponseDto> Add([FromHeader(Name = "X-Evi-TrackingId")] string trackingId, [FromBody] SumDto add)
        {
            _loggerManager.LogInfo("Call 'Add' post method on CalculatorController");

            if (add.Addends == null)
            {
                _loggerManager.LogError("BadRequestException on calls 'Add' post method on CalculatorController");
                throw new BadRequestException("Unable to process request: Add");
            }

            try
            {
                var result = await _calculator.Addition(add, trackingId);

                return result;
            }
            catch (System.Exception ex)
            {
                _loggerManager.LogError("InternalErrorException on calls 'Add' post method on CalculatorController", ex);
                throw new InternalErrorException("An unexpected error condition was triggered which made impossible to fulfill the request. Please try again later.");
            }
        }

        [HttpPost]
        [Route("sub")]
        public async Task<SubResponseDto> Sub([FromHeader(Name = "X-Evi-TrackingId")] string trackingId, [FromBody] SubDto sub)
        {
            _loggerManager.LogInfo("Call 'Sub' post method on CalculatorController");

            if (sub == null)
            {
                _loggerManager.LogError("BadRequestException on calls 'Sub' post method on CalculatorController");
                throw new BadRequestException("Unable to process request: Sub");
            }

            try
            {
                var result = await _calculator.Subtraction(sub, trackingId);
                return result;
            }
            catch (System.Exception ex)
            {
                _loggerManager.LogError("InternalErrorException on calls 'Sub' post method on CalculatorController", ex);
                throw new InternalErrorException("An unexpected error condition was triggered which made impossible to fulfill the request. Please try again later.");
            }
        }

        [HttpPost]
        [Route("mult")]
        public async Task<MultResponseDto> Mult([FromHeader(Name = "X-Evi-TrackingId")] string trackingId, [FromBody] MultDto mul)
        {
            _loggerManager.LogInfo("Call 'Mult' post method on CalculatorController");

            if (mul == null)
            {
                _loggerManager.LogError("BadRequestException on calls 'Mult' post method on CalculatorController");
                throw new BadRequestException("Unable to process request: Mult");
            }

            try
            {
                var result = await _calculator.Multiplication(mul, trackingId);
                return result;
            }
            catch (System.Exception ex)
            {
                _loggerManager.LogError("InternalErrorException on calls 'Mult' post method on CalculatorController", ex);
                throw new InternalErrorException("An unexpected error condition was triggered which made impossible to fulfill the request. Please try again later.");
            }
        }

        [HttpPost]
        [Route("div")]
        public async Task<DivResponseDto> Div([FromHeader(Name = "X-Evi-TrackingId")] string trackingId, [FromBody] DivDto div)
        {
            _loggerManager.LogInfo("Call 'Div' post method on CalculatorController");

            if (div == null)
            {
                _loggerManager.LogError("BadRequestException on calls 'Div' post method on CalculatorController");
                throw new BadRequestException("Unable to process request: Div");
            }

            try
            {
                var result = await _calculator.Division(div, trackingId);
                return result;
            }
            catch (System.Exception ex)
            {
                _loggerManager.LogError("InternalErrorException on calls 'Div' post method on CalculatorController", ex);
                throw new InternalErrorException("An unexpected error condition was triggered which made impossible to fulfill the request. Please try again later.");
            }
        }

        [HttpPost]
        [Route("sqrt")]
        public async Task<SqrtResponseDto> Sqrt([FromHeader(Name = "X-Evi-TrackingId")] string trackingId, [FromBody] SqrtDto sqr)
        {
            _loggerManager.LogInfo("Call 'Sqrt' post method on CalculatorController");

            if (sqr == null)
            {
                _loggerManager.LogError("BadRequestException on calls 'Sqrt' post method on CalculatorController");
                throw new BadRequestException("Unable to process request: Sqrt");
            }

            try
            {
                var result = await _calculator.SquareRoot(sqr, trackingId);
                return result;
            }
            catch (System.Exception ex)
            {
                _loggerManager.LogError("InternalErrorException on calls 'Sqrt' post method on CalculatorController", ex);
                throw new InternalErrorException("An unexpected error condition was triggered which made impossible to fulfill the request. Please try again later.");
            }
        }

        [HttpGet]
        [Route("query")]
        public IActionResult JournalQuery(string Id)
        {
            _loggerManager.LogInfo("Call 'JournalQuery' get method on CalculatorController");

            try
            {
                var result = _calculator.JournalQuery(Id);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                _loggerManager.LogError("InternalErrorException on calls 'JournalQuery' get method on CalculatorController", ex);
                throw new InternalErrorException("An unexpected error condition was triggered which made impossible to fulfill the request. Please try again later.");
            }
        }
    }
}