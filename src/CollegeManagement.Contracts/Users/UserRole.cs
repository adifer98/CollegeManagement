using System.Text.Json.Serialization;

namespace CollegeManagement.Contracts.Users;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserRole
{
    Student,
    Admin
}