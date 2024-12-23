using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients;

public class GamesClient
{
    private readonly List<GameSummary> _games =
    [
        new()
        {
            Id = 1,
            Name = "Street Fighter II",
            Genre = "Fighting",
            Price = 19.99m,
            ReleaseDate = new DateOnly(1992, 7, 15)
        },
        new()
        {
            Id = 2,
            Name = "Final Fantasy XIV",
            Genre = "Roleplaying",
            Price = 59.99m,
            ReleaseDate = new DateOnly(2010, 9, 30)
        },
        new()
        {
            Id = 3,
            Name = "FIFA 23",
            Genre = "Sports",
            Price = 69.99m,
            ReleaseDate = new DateOnly(2022, 9, 27)
        }
    ];

    private readonly Genre[] _genres = new GenresClient().GetGenres();

    public GameSummary[] GetGames() => _games.ToArray();

    public void AddGame(GameDetails game)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(game.GenreId);
        var genre = GetGenreById(game.GenreId);

        var gameSummary = new GameSummary
        {
            Id = _games.Count + 1,
            Name = game.Name,
            Genre = genre.Name,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };

        _games.Add(gameSummary);
    }

    private Genre GetGenreById(string? genreId)
    {
        var genre = _genres.Single(x => x.Id.ToString() == genreId);
        return genre;
    }

    private Genre GetGenreIdByName(string? genreName)
    {
        var genre = _genres.Single(x => x.Name == genreName);
        return genre;
    }


    public GameDetails GetGame(int id)
    {
        var game = _games.Single(x => x.Id == id);
        var genre = GetGenreIdByName(game.Genre);

        return new GameDetails()
        {
            Id = game.Id,
            Name = game.Name,
            GenreId = genre.Id.ToString(),
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }

    public void UpdateGame(GameDetails game)
    {
        var genre = GetGenreById(game.GenreId);
        var gameToUpdate = _games.Single(x => x.Id == game.Id);
        gameToUpdate.Name = game.Name;
        gameToUpdate.Price = game.Price;
        gameToUpdate.ReleaseDate = game.ReleaseDate;
        gameToUpdate.Genre = genre.Name;    
    }

    public void DeleteGame(int id)
    {
        var gameToDelete = _games.Single(x => x.Id == id);
        _games.Remove(gameToDelete);
    }
}