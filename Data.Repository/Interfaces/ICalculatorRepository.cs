using Data.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repository.Interfaces
{
    public interface ICalculatorRepository
    {
        Task Insert(Operations operations);

        List<Operations> Read(string trackingId);
    }
}