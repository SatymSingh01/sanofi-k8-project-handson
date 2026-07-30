var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var appOneBaseUrl = builder.Configuration["AppOne:BaseUrl"] ?? "http://localhost:5003";
builder.Services.AddHttpClient("AppOne", client =>
{
    client.BaseAddress = new Uri(appOneBaseUrl);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
