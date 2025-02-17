using System.Net.Http.Headers;

namespace CollegeManagement.Api.Consumer;

public class AuthTokenHandler : DelegatingHandler
{
    private readonly AuthTokenProvider _tokenProvider;

    public AuthTokenHandler(AuthTokenProvider tokenProvider)
    {
        _tokenProvider = tokenProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _tokenProvider.GetTokenAsync();
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return await base.SendAsync(request, cancellationToken);
    }
}