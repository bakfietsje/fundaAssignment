using Moq;
using AutoMapper;
using FundaAssignment.QueryHandlers;
using FundaAssignment.Queries;
using FundaAssignment.Services;
using FundaAssignment.DTOs;
using FundaAssignment.Enums;
using FundaAssignment.Models;

public class BrokersQueryHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsMappedBrokers()
    {
        // Arrange
        var mockService = new Mock<IBrokerStatisticService>();
        var mockMapper = new Mock<IMapper>();

        var domainBrokers = new List<Broker>
        {
            new (1, "Makelaar A"),
            new (2, "Makelaar B"),
        };

        var mappedDtos = new List<BrokerDto>
        {
            new (1, "Broker A", 10),
            new (2, "Broker B", 5 )
        };

        mockService.Setup(s =>
                s.GetBrokerStatisticsAsync("/amsterdam", FundaOfferTypes.koop, It.IsAny<CancellationToken>()))
            .ReturnsAsync(domainBrokers);

        mockMapper.Setup(m => m.Map<IEnumerable<BrokerDto>>(domainBrokers))
            .Returns(mappedDtos);

        var handler = new BrokersQueryHandler(mockService.Object, mockMapper.Object);
        var query = new BrokersQuery("/amsterdam", FundaOfferTypes.koop);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

       var resultList = result.ToList();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, resultList.Count);
    }

    [Fact]
    public async Task GetBrokers_WithIncorrectUserInput_ReturnsEmptyList()
    {
        // Arrange
        var mockService = new Mock<IBrokerStatisticService>();
        var mockMapper = new Mock<IMapper>();

        var emptyDomainBrokers = new List<Broker>();
        var emptyMappedDtos = new List<BrokerDto>();

        mockService.Setup(s =>
                s.GetBrokerStatisticsAsync("/jdashjkdah", FundaOfferTypes.koop, It.IsAny<CancellationToken>()))
            .ReturnsAsync(emptyDomainBrokers);

        mockMapper.Setup(m => m.Map<IEnumerable<BrokerDto>>(emptyDomainBrokers))
            .Returns(emptyMappedDtos);

        var handler = new BrokersQueryHandler(mockService.Object, mockMapper.Object);
        var query = new BrokersQuery("/jdashjkdah", FundaOfferTypes.koop);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        var resultList = result.ToList();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(resultList);
    }
}