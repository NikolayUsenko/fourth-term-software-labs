using Microsoft.AspNetCore.Mvc;

namespace fourth_term_software_labs.Controllers
{
    public class GamingController : Controller
    {
        [Route("gaming")]
        [Route("games")]
        public IActionResult Index()
        {
            ViewBag.PageTitle = "Gaming Hub - Портал для геймеров";
            ViewBag.FeaturedGames = new[] { "Cyberpunk 2077", "The Witcher 3", "Red Dead Redemption 2", "Elden Ring" };
            ViewBag.FeaturedPlatforms = new[] { "PC", "PlayStation", "Xbox", "Nintendo Switch" };

            return View();
        }

        [Route("gaming/games")]
        [Route("games/list")]
        public IActionResult Games()
        {
            ViewBag.PageTitle = "Все игры";

            var games = new[]
            {
                new { Title = "Cyberpunk 2077", Genre = "RPG", Rating = 4.2, Platform = "PC, PS, Xbox, Switch" },
                new { Title = "The Witcher 3", Genre = "RPG", Rating = 4.8, Platform = "PC, PS, Xbox, Switch" },
                new { Title = "Red Dead Redemption 2", Genre = "Action-Adventure", Rating = 4.9, Platform = "PC, PS, Xbox" },
                new { Title = "Elden Ring", Genre = "Action RPG", Rating = 4.9, Platform = "PC, PS, Xbox, Switch" },
                new { Title = "God of War Ragnarök", Genre = "Action-Adventure", Rating = 4.8, Platform = "PC, PS" },
                new { Title = "The Last of Us Part I", Genre = "Action-Adventure", Rating = 4.7, Platform = "PC, PS" },
                new { Title = "Starfield", Genre = "RPG", Rating = 4.5, Platform = "PC, Xbox" },
                new { Title = "Baldur's Gate 3", Genre = "Turn-based Strategy", Rating = 4.9, Platform = "PC, PS, Xbox" },
                new { Title = "Spider-Man 2", Genre = "Action-Adventure", Rating = 4.9, Platform = "PS, Xbox" },
                new { Title = "Horizon Forbidden West", Genre = "RPG", Rating = 4.8, Platform = "PC, PS" },
                new { Title = "Halo Infinite", Genre = "FPS", Rating = 4.5, Platform = "PC, Xbox" },
                new { Title = "Forza Horizon 5", Genre = "Racing", Rating = 4.9, Platform = "PC, PS, Xbox" },
                new { Title = "Gears 5", Genre = "FPS", Rating = 4.8, Platform = "PC, Xbox" },
                new { Title = "The Legend of Zelda: Tears of the Kingdom", Genre = "RPG", Rating = 4.9, Platform = "Switch" },
                new { Title = "Super Mario Odyssey", Genre = "Platformer", Rating = 4.5, Platform = "Switch" },
                new { Title = "Metroid Prime", Genre = "FPS", Rating = 4.8, Platform = "Switch" }
            };
            ViewBag.Games = games;
            ViewBag.TotalGames = games.Length;

            return View();
        }

        [Route("gaming/game/{gameTitle}")]
        [Route("game/{gameTitle}")]
        public IActionResult Game(string gameTitle)
        {
            ViewBag.PageTitle = $"{gameTitle} - информация об игре";
            ViewBag.GameTitle = gameTitle;

            Dictionary<string, double> ratings = new Dictionary<string, double>
            {
                { "Cyberpunk 2077", 4.2 },
                { "The Witcher 3", 4.8 },
                { "Red Dead Redemption 2", 4.9 },
                { "Elden Ring", 4.9 },
                { "God of War Ragnarök", 4.8 },
                { "The Last of Us Part I", 4.7 },
                { "Starfield", 4.5 },
                { "Baldur's Gate 3", 4.9 },
                { "Spider-Man 2", 4.7 },
                { "Horizon Forbidden West", 4.8 },
                { "Halo Infinite", 4.5 },
                { "Forza Horizon 5", 4.9 },
                { "Gears 5", 4.8 },
                { "The Legend of Zelda: Tears of the Kingdom", 4.9 },
                { "Super Mario Odyssey", 4.5 },
                { "Metroid Prime", 4.8 }
            };

            ViewBag.Rating = ratings.ContainsKey(gameTitle) ? ratings[gameTitle] : 4.0;

            Dictionary<string, string> genres = new Dictionary<string, string>
            {
                { "Cyberpunk 2077", "RPG, Action-Adventure" },
                { "The Witcher 3", "RPG, Action-Adventure" },
                { "Red Dead Redemption 2", "Action-Adventure, Open World" },
                { "Elden Ring", "Action RPG, Souls-like" },
                { "God of War Ragnarök", "Action-Adventure" },
                { "The Last of Us Part I", "Action-Adventure, Survival" },
                { "Starfield", "RPG, Open World" },
                { "Baldur's Gate 3", "RPG, Turn-based Strategy" },
                { "Spider-Man 2", "Action-Adventure" },
                { "Horizon Forbidden West", "RPG, Open World" },
                { "Halo Infinite", "FPS" },
                { "Forza Horizon 5", "Racing, Open World" },
                { "Gears 5", "FPS" },
                { "The Legend of Zelda: Tears of the Kingdom", "RPG, Action-Adventure, Open World" },
                { "Super Mario Odyssey", "Platformer" },
                { "Metroid Prime", "FPS, Action-Adventure" }
            };

            ViewBag.Genre = genres.ContainsKey(gameTitle) ? genres[gameTitle] : "Unknown";

            Dictionary<string, string> platforms = new Dictionary<string, string>
            {
                { "Cyberpunk 2077", "PC, PS, Xbox, Switch" },
                { "The Witcher 3", "PC, PS, Xbox, Switch" },
                { "Red Dead Redemption 2", "PC, PS, Xbox" },
                { "Elden Ring", "PC, PS, Xbox, Switch" },
                { "God of War Ragnarök", "PC, PS" },
                { "The Last of Us Part I", "PC, PS" },
                { "Starfield", "PC, Xbox" },
                { "Baldur's Gate 3", "PC, PS, Xbox" },
                { "Spider-Man 2", "PS, Xbox" },
                { "Horizon Forbidden West", "PC, PS" },
                { "Halo Infinite", "PC, Xbox" },
                { "Forza Horizon 5", "PC, PS, Xbox" },
                { "Gears 5", "PC, Xbox" },
                { "The Legend of Zelda: Tears of the Kingdom", "Switch" },
                { "Super Mario Odyssey", "Switch" },
                { "Metroid Prime", "Switch" }
            };

            ViewBag.Platforms = platforms.ContainsKey(gameTitle) ? platforms[gameTitle] : "PC, PS, Xbox, Switch";

            ViewBag.SystemRequirements = new Dictionary<string, string>
            {
                { "OS", "Windows 10/11 (64-bit)" },
                { "Processor", "Intel Core i5-8400 / AMD Ryzen 5 2600" },
                { "Memory", "16 GB RAM" },
                { "Graphics", "NVIDIA GeForce GTX 1060 6GB / AMD Radeon RX 580" },
                { "DirectX", "Version 12" },
                { "Storage", "70 GB available space" }
            };

            return View();
        }

        [Route("gaming/platform/{platform}")]
        [Route("platform/{platform}")]
        public IActionResult Platform(string platform)
        {
            ViewBag.PageTitle = $"Игры на {platform}";
            ViewBag.Platform = platform;

            Dictionary<string, string[]> platformGames = new Dictionary<string, string[]>
            {
                { "PC", new[] { "Cyberpunk 2077", "The Witcher 3", "Red Dead Redemption 2", "Elden Ring", "Starfield", "Baldur's Gate 3, God of War Ragnarök," +
                "The Last of Us Part I", "Horizon Forbidden West", "Halo Infinite", "Forza Horizon 5", "Gears 5" } },
                { "PlayStation", new[] { "Cyberpunk 2077", "Elden Ring", "God of War Ragnarök", "The Last of Us Part I", "Baldur's Gate 3", "Spider-Man 2",
                    "Horizon Forbidden West", "Forza Horizon 5" } },
                { "Xbox", new[] { "Cyberpunk 2077", "The Witcher 3", "Red Dead Redemption 2", "Elden Ring", "Starfield", "Baldur's Gate 3", "Spider-Man 2",
                    "Halo Infinite", "Forza Horizon 5", "Gears 5" } },
                { "Nintendo Switch", new[] { "Cyberpunk 2077", "The Witcher 3", "Elden Ring", "The Legend of Zelda: Tears of the Kingdom",
                    "Super Mario Odyssey", "Metroid Prime" } }
            };

            ViewBag.PlatformGames = platformGames[platform];
            ViewBag.GamesCount = platformGames[platform].Length;


            return View();
        }
    }
}