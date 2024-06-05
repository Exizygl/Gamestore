using GameStore.Api;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using Microsoft.VisualBasic;


var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

app.MapGamesEndpoints();

app.MigrateDb();

app.Run();
