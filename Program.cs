using Bookfy.Books.Api.Adapters;
using Bookfy.Books.Api.Ports;
using Flurl.Http.Configuration;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration
        .GetSection(nameof(MongoDbSettings))
);

builder.Services.AddSingleton<IMongoClient>(new MongoClient(
    builder.Configuration
        .GetSection(nameof(MongoDbSettings))
        .GetValue<string>("ConnectionString")
));

builder.Services.AddSingleton<IFlurlClientCache>(
    new FlurlClientCache());


builder.Services.Configure<AuthorHttpSettings>(
    builder.Configuration.GetSection(nameof(AuthorHttpSettings)));
builder.Services.Configure<PublisherHttpSettings>(
    builder.Configuration.GetSection(nameof(PublisherHttpSettings)));

builder.Services.AddScoped<IBookRepository, BookMongoDb>();
builder.Services.AddScoped<IAuthorRepository, AuthorHttpClient>();
builder.Services.AddScoped<IPublisherRepository, PublisherHttpClient>();

builder.Services.AddScoped<IBookUseCase, BookService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup("v1")
    .MapBookRoutes();

app.Run();