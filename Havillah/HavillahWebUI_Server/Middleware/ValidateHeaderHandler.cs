namespace HavillahWebUI_Server.Middleware;

public class ValidateHeaderHandler: DelegatingHandler
{
  protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
  {
   return base.SendAsync(request, cancellationToken);
  }
}