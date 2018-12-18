# Be.Vlaanderen.Basisregisters.AspNetCore.Mvc.Middleware.AddCorrelationId

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
