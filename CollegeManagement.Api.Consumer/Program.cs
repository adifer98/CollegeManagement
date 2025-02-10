using System.Net.Http.Headers;
using System.Text.Json;
using CollegeManagement.Api.Consumer;
using CollegeManagement.Api.SDK;
using Microsoft.Extensions.DependencyInjection;
using Refit;

Console.WriteLine("Hello!!!");

var services = new ServiceCollection();

services.AddRefitClient<ICollegeManagementApi>(s => new RefitSettings
    {
        AuthorizationHeaderValueGetter = () => s.GetRequiredService<AuthTokenProvider>().GetTokenAsync()
    })
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri("http://localhost:5113");
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer",
                "YOUR_TOKEN");
    });

var provider = services.BuildServiceProvider();
var collegeManagementApi = provider.GetRequiredService<ICollegeManagementApi>();

// var collegeManagementApi = RestService.For<ICollegeManagementApi>("http://localhost:5113");

// var user = await collegeManagementApi.GetUserAsync("adi-fermon-herzliya");

var users = await collegeManagementApi.GetAllUsersAsync();

// Console.WriteLine(JsonSerializer.Serialize(user));

users.items.ToList().ForEach(item => Console.WriteLine(JsonSerializer.Serialize(item)));
