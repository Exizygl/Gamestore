using GameStore.Api.Dtos;
using Microsoft.VisualBasic;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
const string GetGameEndPointName = "GetGame";


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
    )

];

//get
app.MapGet("games", () => games);

app.MapGet("games/{id}", (int id) => 

{
    GameDto? game  = games.Find(game => game.Id == id); 

    return game is null ? Results.NotFound() : Results.Ok(game);}
    ).WithName(GetGameEndPointName);

//post
app.MapPost("games", (CreateGameDto newGame) =>
{
    
    GameDto game = new(games.Count +1,
    newGame.Name,
     newGame.Genre,
     newGame.Price,
     newGame.ReleaseDate);
     games.Add(game);

     return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id}, game);
}

);

//PUT
app.MapPut("games/{id}" , (int id, UpdateGameDto updateGame) =>
{
    
    var index = games.FindIndex(game => game.Id == id);

    if (index == -1)
    {
        return Results.NotFound();
    }

    games[index] = new GameDto(
        id,
        updateGame.Name,
        updateGame.Genre,
        updateGame.Price,
        updateGame.ReleaseDate
    );
    
    return Results.NoContent();
});

//Delete

app.MapDelete("games/{id}", (int id) =>
{
    games.RemoveAll(game => game.Id == id);

    return Results.NoContent();
});
app.MapGet("/", () => "Hello World!");

app.Run();
