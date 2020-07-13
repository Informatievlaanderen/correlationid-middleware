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
        private readonly string _claimName;

        public AddCorrelationIdMiddleware(
            RequestDelegate next,
            string claimName = UrnBasisregistersVlaanderenCorrelationId)
        {
            _next = next;
            _claimName = claimName;
        }

        public Task Invoke(HttpContext context)
        {
            var correlationId = context.Request.HttpContext.TraceIdentifier;

            if (context.User.Identity is ClaimsIdentity user && !user.HasClaim(x => x.Type == _claimName))
                user.AddClaim(new Claim(_claimName, correlationId, ClaimValueTypes.String));

            return _next(context);
        }
    }
}
