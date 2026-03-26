using System.Net.Http.Json;
using FakeStoreApiClient;

var httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("https://fakestoreapi.com/");

await DeleteProductAsync(1);
await DeleteProductAsync(9999);

var users = await GetUsersAsync();
if (users != null && users.Count > 0)
{
    Console.WriteLine($"User found: {users[0].Username}");

    var carts = await GetUserCartsAsync(users[0].Id);
    Console.WriteLine($"User {users[0].Username} has {carts?.Count ?? 0} carts.");
}

var token = await LoginAsync("johnd", "m38rmF$");
Console.WriteLine($"Token: {token}");


async Task DeleteProductAsync(int id)
{
    try
    {
        var response = await httpClient.DeleteAsync($"products/{id}");
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Product {id} deleted successfully.");
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Console.WriteLine($"Product {id} not found.");
        }
        else
        {
            Console.WriteLine($"Failed to delete product {id}. Status: {response.StatusCode}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Request failed: {ex.Message}");
    }
}

async Task<List<User>?> GetUsersAsync()
{
    try
    {
        return await httpClient.GetFromJsonAsync<List<User>>("users");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error fetching users: {ex.Message}");
        return null;
    }
}

async Task<List<Cart>?> GetUserCartsAsync(int userId)
{
    try
    {
        return await httpClient.GetFromJsonAsync<List<Cart>>($"carts/user/{userId}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error fetching carts: {ex.Message}");
        return null;
    }
}

async Task<string?> LoginAsync(string username, string password)
{
    try
    {
        var response = await httpClient.PostAsJsonAsync("auth/login", new Auth { Username = username, Password = password });
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<Auth>();
            return data?.Token;
        }

        Console.WriteLine($"Login failed. Status: {response.StatusCode}");
        return null;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Login request failed: {ex.Message}");
        return null;
    }
}
