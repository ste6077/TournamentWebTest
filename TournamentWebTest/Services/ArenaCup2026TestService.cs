using Microsoft.AspNetCore.Components;
using TournamentLibrary;
using TournamentLibrary.Teams;
using static System.Net.WebRequestMethods;

namespace TournamentWebTest.Services
{
    public class ArenaCup2026TestService
    {
        public TournamentLibrary.Tournament Tournament { get; private set; }
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public string CurrentTime { get; set; } = "unknown";

        public ArenaCup2026TestService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
            Init();

            //timer.SetTime(new TimeSpan(0, 10, 0)); // 10 minutes
            //timer.Tick += Timer_Tick;
            //timer.Start();

            //InitStage1(teams);
            //teams.RemoveAt(0);
            //teams.RemoveAt(0);
            //InitStage2(teams);
        }

        public async void Init()
        {
            var teamService = new TeamService(_http, _navigationManager);
            await teamService.LoadTeamsAsync();

            Console.WriteLine("Erstelle Arena Cup 2026 Testturnier...");

            Tournament = Utilities.Wizard.GetBwk2026Tournament();

            if (Tournament == null)
                return;

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
