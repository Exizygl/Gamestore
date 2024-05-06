using GameStore.Api;
using GameStore.Api.Dtos;
using Microsoft.VisualBasic;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.MapGamesEndpoints();

app.Run();
