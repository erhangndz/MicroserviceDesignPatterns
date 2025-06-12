using System.Diagnostics;
using System.Net;
using Polly;
using Polly.Extensions.Http;
using ServiceA.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.  

builder.Services.AddHttpClient<ProductService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:5002/api/");
}).AddPolicyHandler(GetAdvancedCircuitBreakerPolicy());



IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
{
    return HttpPolicyExtensions.HandleTransientHttpError().CircuitBreakerAsync(3, TimeSpan.FromSeconds(10),
        onBreak: (arg1, arg2) =>
        {
            Debug.WriteLine("Circuit Breaker Status => On Break");
        },
        onReset: () =>
        {
            Debug.WriteLine("Circuit Breaker Status => On Reset");
        },
        onHalfOpen: () =>
        {
            Debug.WriteLine("Circuit Breaker Status => On Half Open");
        });
}

IAsyncPolicy<HttpResponseMessage> GetAdvancedCircuitBreakerPolicy()
{
    return HttpPolicyExtensions.HandleTransientHttpError().AdvancedCircuitBreakerAsync(
        failureThreshold: 0.1, // %10 oranýnda baþarýsýz istekleri sayacak 
        samplingDuration: TimeSpan.FromSeconds(30), //30 saniye boyunca devre çalýþýr durumda
        minimumThroughput: 3, //30 saniye boyunca 3 istek baþarýsýz olursa devre kesilecek
        durationOfBreak: TimeSpan.FromSeconds(30), //yeniden açýlmadan önce 30 saniye kapalý kalacak
        onBreak: (arg1, arg2) =>
        {
            Debug.WriteLine("Circuit Breaker Status => On Break");
        },
        onReset: () =>
        {
            Debug.WriteLine("Circuit Breaker Status => On Reset");
        },
        onHalfOpen: () =>
        {
            Debug.WriteLine("Circuit Breaker Status => On Half Open");
        });
}

IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    return HttpPolicyExtensions.HandleTransientHttpError().OrResult(message =>
            message.StatusCode == HttpStatusCode.NotFound)
        .WaitAndRetryAsync(5,
            retryAttempt =>
            {
                Debug.WriteLine($"Retry Count  :{retryAttempt}");
                return TimeSpan.FromSeconds(10);
            }, onRetryAsync: OnRetryAsync);
}

Task OnRetryAsync(DelegateResult<HttpResponseMessage> arg1, TimeSpan arg2)
{
    Debug.WriteLine($"Request is made again : {arg2.TotalMilliseconds}");
    return Task.CompletedTask;
}

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle  
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.  
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
