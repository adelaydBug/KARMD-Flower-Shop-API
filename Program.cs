using KarmdFlowerShopApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.TagActionsBy(api =>
    {
        var controller = api.ActionDescriptor.RouteValues["controller"];

        return controller switch
        {
            "Flower" => new[] { "1. Products" },
            "Customer" => new[] { "2. Customer Management" },
            "Order" => new[] { "3. Order Management" },
            "OrderDetail" => new[] { "3. Order Management" },
            "Delivery" => new[] { "4. Fulfillment & Payment" },
            "Payment" => new[] { "4. Fulfillment & Payment" },
            _ => new[] { "5. Other" }
        };
    });
});

builder.Services.AddDbContext<KarmdFlowerShopDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(cfg => { }, typeof(Program).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();