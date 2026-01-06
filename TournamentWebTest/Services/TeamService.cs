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

        public static bool IsLoaded { get; private set; } = false;

        public static List<Team> Teams { get; private set; } = new();
        private object _lock = new object();

        public TeamService(HttpClient http, NavigationManager navigationManager)
        {
            Console.WriteLine("TeamService loading...");
            _http = http;
            _navigationManager = navigationManager;
        }

        public async Task<List<Team>> LoadTeamsAsync()
        {
            if (IsLoaded) return Teams;

            var tmpTeams = new List<Team>();

            try
            {
                // WICHTIG: kein führender Slash
                var indexPath = "Data/Teams/index.json";

                var filenames = await _http.GetFromJsonAsync<List<string>>(indexPath);
                if (filenames == null) return new List<Team>();

                foreach (var file in filenames)
                {
                    try
                    {
                        var json = await _http.GetStringAsync($"Data/Teams/{file}");
                        var teamFile = TournamentLibrary.Utilities.JsonConverter.Deserialize<TournamentLibrary.Teams.File>(json);
                        var team = teamFile.GetTeam(file);
                        team.Base64LogoBig = teamFile.Base64LogoBig;
                        if (team != null) tmpTeams.Add(team);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Fehler beim Laden von {file}: {ex.Message}");
                    }
                }

                IsLoaded = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Laden der Teamliste: {ex.Message}");
            }

            lock (_lock)
            {
                Teams.Clear();
                Teams.AddRange(tmpTeams);
                Console.WriteLine($"Teams geladen: {tmpTeams.Count}");
                return Teams;
            }
        }

        public async Task<List<Team>> LoadTeamsAsync1()
        {
            if (IsLoaded)
                return Teams;
            var tmpTeams = new List<Team>();
            try
            {
                Console.WriteLine($"Teams laden...");
                string basePath = _navigationManager.BaseUri.Contains("github.io") ? "TournamentWebTest/" : "";
                var indexPath = _navigationManager.BaseUri.Contains("github.io") ? $"{basePath}data/teams/index.json" : $"/Data/Teams/index.json";
                Console.WriteLine($"Pfad: {indexPath}");
                var filenames = await _http.GetFromJsonAsync<List<string>>(indexPath);
                if (filenames == null)
                {
                    Console.WriteLine($"Keine Teams gefunden: {indexPath}");
                    return new List<Team>();
                }

                foreach (var file in filenames)
                {
                    try
                    {
                        var json = await _http.GetStringAsync($"{basePath}data/teams/{file}");
                        var team = TournamentLibrary.Utilities.JsonConverter.Deserialize<TournamentLibrary.Teams.File>(json).GetTeam(file);
                        if (team != null)
                            tmpTeams.Add(team);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Fehler beim Laden von {file}: {ex.Message}");
                    }
                }
                IsLoaded = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Laden der Teamliste: {ex.Message}");
            }

            lock(_lock)
            {
                Teams.Clear();
                Teams.AddRange(tmpTeams);
                Console.WriteLine($"Teams geladen: {tmpTeams.Count}");
                return Teams;
            }
        }
    }
}
