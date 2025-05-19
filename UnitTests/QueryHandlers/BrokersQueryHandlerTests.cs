using AutoMapper;
using FundaAssignment.DTOs;
using FundaAssignment.Enums;
using FundaAssignment.Models;
using FundaAssignment.Queries;
using FundaAssignment.QueryHandlers;
using FundaAssignment.Services;
using Moq;
using Xunit;

namespace UnitTests.QueryHandlers;

public class BrokersQueryHandlerTests
{
    [Theory]
    [InlineData(10, 5)]
    [InlineData(5, 20)]
    public async Task GetBrokers_WithValidCounts_ReturnsCorrectlyMappedBrokers
        (int firstBrokerCount, int secondBrokerCount)
    {
        // Arrange
        var mockService = new Mock<IBrokerStatisticService>();
        var mockMapper = new Mock<IMapper>();

        var domainBrokers = new List<Broker>
        {
            new (1, "Broker A")
            {
                Count = firstBrokerCount
            },
            new (2, "Broker B")
            {
                Count = secondBrokerCount
            },
        };

        var mappedDtos = new List<BrokerDto>
        {
            new (1, "Broker A", firstBrokerCount),
            new (2, "Broker B", secondBrokerCount)
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
        Assert.Equal("Broker A", resultList[0].Name);
        Assert.Equal(firstBrokerCount, resultList[0].Count);
        Assert.Equal("Broker B", resultList[1].Name);
        Assert.Equal(secondBrokerCount, resultList[1].Count);
    }

    [Fact]
    public async Task GetBrokers_WithIncorrectUserInput_ReturnsEmptyListAndDoesNotThrow()
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