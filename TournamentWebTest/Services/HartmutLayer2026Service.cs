using Microsoft.AspNetCore.Components;
using TournamentLibrary;
using TournamentLibrary.Teams;
using static System.Net.WebRequestMethods;

namespace TournamentWebTest.Services
{
    public class HartmutLayer2026TestService
    {
        public TournamentLibrary.Tournament Tournament { get; private set; }
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public string CurrentTime { get; set; } = "unknown";

        public HartmutLayer2026TestService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
            Init();
        }

        public async void Init()
        {
            var teamService = new TeamService(_http, _navigationManager);
            await teamService.LoadTeamsAsync();

            Console.WriteLine("Erstelle Hartmut Layer Cup 2026 Testturnier...");

            Tournament = Utilities.Wizard.GetHartmutLayer2026();

            foreach (var g in Tournament.GroupStage1.Groups)
            {
                Console.WriteLine($"Stage1: {g.Name}");
            }

            Tournament.Start();
            TournamentLibrary.Timers.Timer timer = new TournamentLibrary.Timers.Timer();
        }

        #region Timer

        private void Timer_Tick(object? sender, TimeSpan e)
        {
            CurrentTime = e.ToString(@"hh\:mm\:ss");
        }

        #endregion
    }
}
