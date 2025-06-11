using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using Polly;
using Polly.Extensions.Http;
using ServiceA.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddHttpClient<ProductService>(opt =>
{
    opt.BaseAddress = new Uri("https://localhost:5002/api/");
}).AddPolicyHandler(GetCircuitBreakerPolicy());



IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
{
    return HttpPolicyExtensions.HandleTransientHttpError().CircuitBreakerAsync(3, TimeSpan.FromSeconds(10),
        onBreak: () =>
        {

        },
        onReset: () =>
        {

        },
        onHalfOpen: () =>
        {

        } );
}



IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    return HttpPolicyExtensions.HandleTransientHttpError().OrResult(msg => 
            msg.StatusCode == HttpStatusCode.NotFound)
        .WaitAndRetryAsync(5,
            retryAttempt =>
            {
                Debug.WriteLine($"Retry Count  :{retryAttempt}");
                return TimeSpan.FromSeconds(10);
            },onRetryAsync:OnRetryAsync);
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
