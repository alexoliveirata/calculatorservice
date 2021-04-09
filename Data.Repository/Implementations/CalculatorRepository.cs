using Data.Repository.Interfaces;
using Data.Repository.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repository.Implementations
{
    public class CalculatorRepository : ICalculatorRepository
    {
        #region Global

        private readonly CalculatorContext _calculatorContext;
        private readonly IServiceScope _scope;

        #endregion

        #region Constructor
        public CalculatorRepository(IServiceProvider services)
        {
            _scope = services.CreateScope();
            _calculatorContext = _scope.ServiceProvider.GetRequiredService<CalculatorContext>();
        }

        #endregion

        #region Public methods

        public async Task Insert(Operations operations)
        {
            await _calculatorContext.Operations.AddAsync(operations);
            await _calculatorContext.SaveChangesAsync();
        }

        public List<Operations> Read(string trackingId)
        {
            var result = _calculatorContext.Operations
                .Where(o => o.TrackingId.Equals(trackingId))
                .OrderByDescending(o => o.Date)
                .ToList();

            return result;
        }

        #endregion
    }
}