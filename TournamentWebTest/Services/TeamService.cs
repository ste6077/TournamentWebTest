using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text.Json;
using TournamentLibrary.Teams;

namespace TournamentWebTest.Services
{
    public class TeamService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public bool IsLoaded { get; private set; } = false;

        public List<Team> Teams { get; private set; } = new();

        public TeamService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public async Task LoadTeamsAsync()
        {
            if (IsLoaded) 
                return;
            IsLoaded = true;
            try
            {
                string basePath = _navigationManager.BaseUri.Contains("github.io")
                    ? "TournamentWebTest/"
                    : "";

                var filenames = await _http.GetFromJsonAsync<List<string>>($"{basePath}data/teams/index.json");
                if (filenames == null) return;

                foreach (var file in filenames)
                {
                    try
                    {
                        var json = await _http.GetStringAsync($"{basePath}data/teams/{file}");
                        var team = TournamentLibrary.Utilities.JsonConverter.Deserialize<TournamentLibrary.Teams.File>(json).GetTeam(file);
                        if (team != null)
                            Teams.Add(team);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Fehler beim Laden von {file}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Laden der Teamliste: {ex.Message}");
            }
        }
    }
}
