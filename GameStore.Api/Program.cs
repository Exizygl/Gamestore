using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


List<GameDto> games = [
    new (
        1,
        "FF14",
        "MMORPG",
        59.99M,
        new DateOnly(2010, 3, 30)
    ),
     new (
        2,
        "Nier Automata",
        "RPG",
        59.99M,
        new DateOnly(2016, 4, 28)
    ),
     new (
        3,
        "Hades",
        "Roguelike",
        29.99M,
        new DateOnly(2022, 9, 15)
    ),

];

app.MapGet("games", () => games);

app.MapGet("/", () => "Hello World!");

app.Run();
