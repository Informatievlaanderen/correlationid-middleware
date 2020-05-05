# Be.Vlaanderen.Basisregisters.AspNetCore.Mvc.Middleware.AddCorrelationId [![Build Status](https://github.com/Informatievlaanderen/correlationid-middleware/workflows/CI/badge.svg)](https://github.com/Informatievlaanderen/correlationid-middleware/actions)

Middleware component which adds a correlation id as a claim for the user on the request context.

## Usage

```csharp
public void Configure(IApplicationBuilder app, ...)
{
    app
        ...
        .UseMiddleware<AddCorrelationIdToLogContextMiddleware>()
        .UseMiddleware<AddCorrelationIdToResponseMiddleware>()
        .UseMiddleware<AddCorrelationIdMiddleware>()
        ...
}
```
