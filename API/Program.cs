
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(x=>x.UseSqlite("Data Source=skinet.db"));
builder.Services.AddEndpointsApiExplorer(); // Required for minimal APIs or OpenAPI

var app = builder.Build();
// Force the database creation (if not already created)
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<StoreContext>();
        context.Database.EnsureCreated();  // Ensures the database is created
    }


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); // Map controller routes

app.Run();
