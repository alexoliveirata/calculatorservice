using AutoMapper;
using CalculatorService.Server.Dto;
using Data.Repository.Models;


namespace CalculatorService.Server.Profiles
{
    public class OperationProfile : Profile
    {
        public OperationProfile()
        {
            CreateMap<Operations, JournalResponseDto>();
        }
    }
}
