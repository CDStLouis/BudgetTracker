using System.Net;
using System.Net.Http.Json;
using BudgetTracker.Api.Contracts;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BudgetTracker.Api.Tests;

public class TransactionsEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public TransactionsEndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetTransactions_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/transactions");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetTransactions_ReturnsNonEmptyCollection()
    {
        var transactions = await _client.GetFromJsonAsync<List<TransactionDto>>("/api/transactions");

        Assert.NotNull(transactions);
        Assert.NotEmpty(transactions!);
    }

    [Fact]
    public async Task GetTransactions_ReturnsNewestFirstOrder()
    {
        var transactions = await _client.GetFromJsonAsync<List<TransactionDto>>("/api/transactions");

        Assert.NotNull(transactions);
        Assert.NotEmpty(transactions!);
        Assert.True(transactions!.Zip(transactions.Skip(1), (left, right) => left.DateUtc >= right.DateUtc).All(value => value));
    }

    [Fact]
    public async Task GetTransactions_MapsAmountAndTypeConsistently()
    {
        var transactions = await _client.GetFromJsonAsync<List<TransactionDto>>("/api/transactions");

        Assert.NotNull(transactions);
        Assert.NotEmpty(transactions!);

        foreach (var transaction in transactions!)
        {
            Assert.Equal(Math.Abs(transaction.SignedAmount), transaction.AbsoluteAmount);

            var expectedType = transaction.SignedAmount < 0 ? "expense" : "income";
            Assert.Equal(expectedType, transaction.Type);
        }
    }

    [Fact]
    public async Task GetTransactions_ReturnsExpectedContractFields()
    {
        var transactions = await _client.GetFromJsonAsync<List<TransactionDto>>("/api/transactions");

        Assert.NotNull(transactions);
        Assert.NotEmpty(transactions!);

        foreach (var transaction in transactions!)
        {
            Assert.NotEqual(Guid.Empty, transaction.Id);
            Assert.False(string.IsNullOrWhiteSpace(transaction.AccountName));
            Assert.False(string.IsNullOrWhiteSpace(transaction.MonthKey));
            Assert.False(string.IsNullOrWhiteSpace(transaction.DateKey));
        }
    }
}
