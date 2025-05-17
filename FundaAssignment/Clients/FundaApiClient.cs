using System.Net;
using FundaAssignment.DTOs.FundaDtos;
using FundaAssignment.Enums;
using FundaAssignment.Models;
using Microsoft.Extensions.Options;
using Polly;

namespace FundaAssignment.Clients;

public class FundaApiClient(HttpClient httpClient, IOptions<FundaOptions> options) : IFundaApiClient
{
    private readonly FundaOptions _options = options.Value;

    private const int PageSize = 25;

    private readonly AsyncPolicy<HttpResponseMessage> _retryPolicy =
        Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .OrResult(x => x.StatusCode is >= HttpStatusCode.TooManyRequests or HttpStatusCode.RequestTimeout or HttpStatusCode.NotFound)
            .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

    public async Task<IEnumerable<Broker>> GetListingsAsync(string searchPath, FundaOfferTypes type, CancellationToken cancellationToken)
    {
        var brokers = new List<Broker>();
        var currentPage = 1;
        var totalPages = 100;

        while (currentPage <= totalPages)
        {
            var url = $"{_options.BaseUrl}{_options.ApiKey}/?type={type}&zo=/{searchPath}&page={currentPage}&pageSize={PageSize}";

            var httpResponse = await _retryPolicy.ExecuteAsync(() =>
                httpClient.GetAsync(url, cancellationToken));

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error upon calling API: {url}: {httpResponse.StatusCode}");
            }

            var response = await httpResponse.Content.ReadFromJsonAsync<FundaApiResponse>(cancellationToken);

            if (response?.Objects == null || response.Objects.Count == 0)
            {
                break;
            }

            if (currentPage == 1)
            {
                var apiTotalPages = response.Paging.TotalPages;
                totalPages = Math.Min(apiTotalPages, 100);
            }

            var currentBrokers = response.Objects.Select(x => new Broker(
                x.BrokerId,
                x.BrokerName));

            brokers.AddRange(currentBrokers);

            currentPage++;
        }

        return brokers;
    }
}