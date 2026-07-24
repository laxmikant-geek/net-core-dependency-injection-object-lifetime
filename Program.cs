var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<TransientCounter>();
builder.Services.AddScoped<ScopedCounter>();
builder.Services.AddSingleton<SingletonCounter>();

var app = builder.Build();

// Two of each service injected into ONE request shows the difference:
// transient = new instance per injection, scoped = same instance within
// the request, singleton = same instance across all requests forever.
app.MapGet("/", (TransientCounter t1, TransientCounter t2,
                 ScopedCounter s1, ScopedCounter s2,
                 SingletonCounter g1, SingletonCounter g2) => Results.Json(new
{
    transient = new { first = t1.Id, second = t2.Id, same = t1.Id == t2.Id },
    scoped = new { first = s1.Id, second = s2.Id, same = s1.Id == s2.Id },
    singleton = new { first = g1.Id, second = g2.Id, same = g1.Id == g2.Id },
}));

app.Run();

public class TransientCounter { public Guid Id { get; } = Guid.NewGuid(); }
public class ScopedCounter    { public Guid Id { get; } = Guid.NewGuid(); }
public class SingletonCounter { public Guid Id { get; } = Guid.NewGuid(); }
