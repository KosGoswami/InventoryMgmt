var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Services to all the Transient 
builder.Services.AddTransient<InventoryMgmtBL.IModule.IBeerBL, InventoryMgmtBL.Module.BeerBL>();
builder.Services.AddTransient<InventoryMgmtBL.IModule.IBarBL,InventoryMgmtBL.Module.BarBL>();
builder.Services.AddTransient<InventoryMgmtBL.IModule.ILogBL, InventoryMgmtBL.Module.LogBL>();
builder.Services.AddTransient<InventoryMgmtBL.IModule.IBeerBarBL,InventoryMgmtBL.Module.BeerBarBL>();
builder.Services.AddTransient<InventoryMgmtBL.IModule.IBeerBreweryBL,InventoryMgmtBL.Module.BeerBreweryBL>();
builder.Services.AddTransient<InventoryMgmtBL.IModule.IBreweryBL, InventoryMgmtBL.Module.BreweryBL>();
builder.Services.AddTransient<InventoryMgmtDL.IDataLayer.IBeerDL,InventoryMgmtDL.DataLayer.BeerDL>();
builder.Services.AddTransient<InventoryMgmtDL.IDataLayer.IBarDL,InventoryMgmtDL.DataLayer.BarDL>();
builder.Services.AddTransient<InventoryMgmtDL.IDataLayer.ILogDL, InventoryMgmtDL.DataLayer.LogDL>();
builder.Services.AddTransient<InventoryMgmtDL.IDataLayer.IBeerBarDL,InventoryMgmtDL.DataLayer.BeerBarDL>();
builder.Services.AddTransient<InventoryMgmtDL.IDataLayer.IBeerBreweryDL,InventoryMgmtDL.DataLayer.BeerBreweryDL>();
builder.Services.AddTransient<InventoryMgmtDL.IDataLayer.IBreweryDL, InventoryMgmtDL.DataLayer.BreweryDL>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.UseStaticFiles();
//app.UseRouting();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller}/{action}/{id}");

app.Run();