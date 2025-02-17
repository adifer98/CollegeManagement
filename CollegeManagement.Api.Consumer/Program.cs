
using System.Text.Json;
using CollegeManagement.Api.Consumer;
using CollegeManagement.Api.SDK;
using CollegeManagement.Contracts.Courses;
using CollegeManagement.Contracts.Users;
using Microsoft.Extensions.DependencyInjection;
using Refit;

Console.WriteLine("Hello!!!");

// Set up dependency injection
var services = new ServiceCollection();

// Register AuthTokenProvider
services.AddSingleton<AuthTokenProvider>();

// Configure Refit client with HttpMessageHandler
services.AddRefitClient<ICollegeManagementApi>()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri("http://localhost:5113");
    })
    .AddHttpMessageHandler<AuthTokenHandler>();

// Register custom handler
services.AddTransient<AuthTokenHandler>();

// Build the service provider
var provider = services.BuildServiceProvider();
var collegeManagementApi = provider.GetRequiredService<ICollegeManagementApi>();


// Call API
// var user = await collegeManagementApi.GetUserAsync("adi-fermon-herzliya");
// Console.WriteLine(JsonSerializer.Serialize(user));


var users = await collegeManagementApi.GetAllUsersAsync();
users.items.ToList().ForEach(item => Console.WriteLine(JsonSerializer.Serialize(item)));

// var createUserRequest = new CreateUserRequest(
// "Arak Ashkelon",
// "Araka@1234.com",
// "Ashkelon",
// UserRole.Teacher);


// var createdUser = await collegeManagementApi.CreateUserAsync(createUserRequest);
//
// Console.WriteLine(JsonSerializer.Serialize(createdUser));

// var createCourseRequest = new CreateCourseRequest(
//     Title: "Biology",
//     Description: "A nice course about the field",
//     Hours: 13,
//     Price: 250);
    
// var course = await collegeManagementApi.CreateCourseAsync(createCourseRequest);
//
// Console.WriteLine(JsonSerializer.Serialize(course));

// await collegeManagementApi.DeleteCourseAsync(new Guid("26ebcd07-fbb7-4b85-90d9-aa934c613240"));

var courses = await collegeManagementApi.GetAllCoursesAsync();
courses.items.ToList().ForEach(item => Console.WriteLine(JsonSerializer.Serialize(item)));