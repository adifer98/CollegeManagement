using System.Text.Json;
using CollegeManagement.Api.SDK;
using Refit;

Console.WriteLine("Hello!!!");

var collegeManagementApi = RestService.For<ICollegeManagementApi>("http://localhost:5113");

var user = await collegeManagementApi.GetUserAsync("adi-fermon-herzliya");

Console.WriteLine(JsonSerializer.Serialize(user));