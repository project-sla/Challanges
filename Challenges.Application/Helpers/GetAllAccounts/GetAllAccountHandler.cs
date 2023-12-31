using System.Text.Json;
using Challenges.Application.Commands.Account.GetAllAccounts;
using RestSharp;

namespace Challenges.Application.Helpers.GetAllAccounts;

public class GetAllAccountHandler
{
    public virtual async Task<Account?> GetAllAccounts(string id)
    {
        var options = new RestClientOptions("https://10.1.23.200:7107")
        {
            RemoteCertificateValidationCallback = (_, _, _, _) => true
        };

        var client = new RestClient(options);
        var request = new RestRequest("api/accounts", Method.Post);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Accept", "application/json");
        request.AddHeader("TenantId", "public");

        request.AddJsonBody(new GetAllAccountsCommand(
            id,
            true,
            1,
            1));
        var response = await client.ExecutePostAsync(request);
        var content = response.Content;

        // if account inside content is null, return null
        if (content.Contains("[]")) return null;

        // Define JSON serializer options
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        var accountsWrapper = JsonSerializer.Deserialize<AccountsWrapper>(content, jsonOptions);
        var account = accountsWrapper?.accounts.FirstOrDefault();

        return account;
    }
}

