using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add session support
builder.Services.AddDistributedMemoryCache(); // Required for session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // You can set your session timeout here
    options.Cookie.HttpOnly = true;  // Ensures cookies are HttpOnly (more secure)
});

//Add DB context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    // apply any pending migrations
    context.Database.Migrate();

    // call seed method
    DbInitializer.Seed(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Enable session middleware
app.UseSession();

app.UseRouting();
app.UseAuthorization();

// Redirect root URL ("/") to "/Login"
app.MapGet("/", () => Results.Redirect("/Login"));

app.MapRazorPages();

app.Run();