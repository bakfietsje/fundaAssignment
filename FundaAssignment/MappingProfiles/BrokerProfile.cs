using AutoMapper;
using FundaAssignment.DTOs;
using FundaAssignment.Models;

namespace FundaAssignment.MappingProfiles;

public class BrokerProfile : Profile
{
    public BrokerProfile()
    {
        CreateMap<Broker, BrokerDto>();
    }
}