namespace Be.Vlaanderen.Basisregisters.AspNetCore.Mvc.Middleware.AddCorrelationId.Tests
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Xunit;

    public class AddCorrelationIdMiddlewareTests
    {
        [Fact]
        public async Task AddsExpectedCorrelationIdToDefaultUserClaims()
        {
            // Arrange
            var middleware = new AddCorrelationIdMiddleware((innerHttpContext) => Task.CompletedTask);
            var context = new DefaultHttpContext();
            var traceIdentifier = Guid.NewGuid().ToString("D");

            context.Request.HttpContext.TraceIdentifier = traceIdentifier;

            // Act
            await middleware.Invoke(context);

            // Assert
            context.User.Should().NotBeNull();
            context.User.HasClaim(AddCorrelationIdMiddleware.UrnBasisregistersVlaanderenCorrelationId, traceIdentifier).Should().BeTrue();
        }

        [Fact]
        public async Task AddsExpectedCorrelationIdToUserClaims()
        {
            // Arrange
            var claimName = "urn:dummy";
            var middleware = new AddCorrelationIdMiddleware((innerHttpContext) => Task.CompletedTask, claimName);
            var context = new DefaultHttpContext();
            var traceIdentifier = Guid.NewGuid().ToString("D");

            context.Request.HttpContext.TraceIdentifier = traceIdentifier;

            // Act
            await middleware.Invoke(context);

            // Assert
            context.User.Should().NotBeNull();
            context.User.HasClaim(claimName, traceIdentifier).Should().BeTrue();
        }
    }
}
