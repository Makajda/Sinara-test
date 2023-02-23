using System.Text;
using System.Text.Json;

namespace SinaraUi.Common;
public class HrClient {
    readonly HttpClient client;
    readonly JsonSerializerOptions serializerOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true };

    public HrClient(HttpClient client) {
        this.client = client;
    }

    public async Task<List<Customer>> Gets() {
        var response = await client.GetAsync(GetUri("gets"));
        response.EnsureSuccessStatusCode();
        return await GetResult<List<Customer>>(response);
    }

    public async Task<Customer> Get(int id = 0) {
        var response = await client.GetAsync(GetUri("get", id));
        response.EnsureSuccessStatusCode();
        return await GetResult<Customer>(response);
    }

    public async Task Add(Customer content) {
        var response = await client.PostAsync(GetUri("add"), GetStringContent(content));
        response.EnsureSuccessStatusCode();
    }

    public async Task Update(Customer item) {
        var response = await client.PutAsync(GetUri("update"), GetStringContent(item));
        response.EnsureSuccessStatusCode();
    }

    public async Task Delete(int id) {
        var response = await client.DeleteAsync(GetUri("delete", id));
        response.EnsureSuccessStatusCode();
    }

    public async Task<bool> Validation(string content) {
        var response = await client.PostAsync(GetUri("validation"), GetStringContent(new KeyValuePair<string, string>("value", content)));
        response.EnsureSuccessStatusCode();
        return await GetResult<bool>(response);
    }

    private async Task<T> GetResult<T>(HttpResponseMessage response) {
        var json = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<T>(json, serializerOptions);
        if (result is null)
            throw new InvalidOperationException("Сервис вернул неопределённое значение");
        else
            return result;
    }

    private StringContent GetStringContent<T>(T content) {
        var json = JsonSerializer.Serialize(content, serializerOptions);
        var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
        return stringContent;
    }

    private string GetUri(string action, int id = 0) {
        var result = $"/customer/{action}";
        if (id > 0)
            result += $"?id={id}";

        return result;
    }
}
