namespace Be.Vlaanderen.Basisregisters.AspNetCore.Mvc.Middleware
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Add the http context trace identifier as a claim (urn:basisregisters:vlaanderen:correlation).
    /// </summary>
    public class AddCorrelationIdMiddleware
    {
        public const string UrnBasisregistersVlaanderenCorrelationId = "urn:basisregisters:vlaanderen:correlation";

        private readonly RequestDelegate _next;

        public AddCorrelationIdMiddleware(RequestDelegate next) => _next = next;

        public Task Invoke(HttpContext context)
        {
            var correlationId = context.Request.HttpContext.TraceIdentifier;

            if (context.User.Identity is ClaimsIdentity user && !user.HasClaim(x => x.Type == UrnBasisregistersVlaanderenCorrelationId))
                user.AddClaim(new Claim(UrnBasisregistersVlaanderenCorrelationId, correlationId, ClaimValueTypes.String));

            return _next(context);
        }
    }
}
