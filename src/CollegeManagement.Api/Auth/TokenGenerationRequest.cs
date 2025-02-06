namespace CollegeManagement.Api.Auth;

public class TokenGenerationRequest
{
    public Guid UserId { get; set; }
    
    public string UserName { get; set; }

    public Dictionary<string, object> CustomClaims { get; set; } = new();
}