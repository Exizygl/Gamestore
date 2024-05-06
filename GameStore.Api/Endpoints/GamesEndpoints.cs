using GameStore.Api.Dtos;

namespace GameStore.Api;

public static class GamesEndpoints
{

    const string GetGameEndPointName = "GetGame";


    private static readonly List<GameDto> games = [
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

   

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games");
        //get
        group.MapGet("/", () => games);

        group.MapGet("/{id}", (int id) =>

        {
            GameDto? game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);
        }
            ).WithName(GetGameEndPointName);

        //post
        group.MapPost("/", (CreateGameDto newGame) =>
        {

            GameDto game = new(games.Count + 1,
            newGame.Name,
            newGame.Genre,
            newGame.Price,
            newGame.ReleaseDate);
            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game);
        }

        );

        //PUT
        group.MapPut("/{id}", (int id, UpdateGameDto updateGame) =>
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

        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });
        return group;

    }

}