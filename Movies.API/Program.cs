var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddDbContext<MoviesContext>(options => {
    options.UseInMemoryDatabase("movies");
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", option => {
        option.Authority ="https://localhost:5008";
        option.TokenValidationParameters = new TokenValidationParameters{
            ValidateAudience = false
        };
    });

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
    {
        builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    }));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using var scope = app.Services.CreateScope();


   var serviceScope = scope.ServiceProvider;
   var movieContext = serviceScope.GetRequiredService<MoviesContext>();
   MoviesContextSeed.SeedAsync(movieContext);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corsapp");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
