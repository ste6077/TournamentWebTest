using System.Text.Json.Serialization;
using TournamentLibrary.Matches;
using TournamentLibrary.Teams;
using TournamentWebTest.Interfaces;

namespace TournamentWebTest.Model
{
    public class MatchEntry : IMatchEntry
    {
        public string GameNumber { get; set; }
        public string Group { get; set; } = "";
        public string NameTeamA { get; set; } = "";
        public string NameTeamB { get; set; } = "";
        public int ScoreA { get; set; }
        public int ScoreB { get; set; }

        public Team? TeamA { get; set; }
        public Team? TeamB { get; set; }
        public Match? MatchLink { get; set; } = null;

        public int GoldenGoalRound { get; set; } = 0; 
        public int GoldenGoalSecond { get; set; } = 1;

        public bool IsFinished { get; set; } = false;

        public MatchEntry() { }
        public MatchEntry(TournamentLibrary.Matches.Match match)
        {
            try
            {
                MatchLink = match;
                Refresh();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Fehler beim MatchEntry Konstruktor -> {match.OverallMatchNumber}: {e.Message}");
            }

        }

        public void Refresh()
        {
            GameNumber = MatchLink.FixtureNumberAsText;
            Group = MatchLink.GroupName;
            ScoreA = (int)MatchLink.ScoreA;
            ScoreB = (int)MatchLink.ScoreB;
            TeamA = MatchLink.TeamA;
            TeamB = MatchLink.TeamB;
            NameTeamA = MatchLink.NameTeamA;
            NameTeamB = MatchLink.NameTeamB;
            IsFinished = MatchLink.IsFinished;
        }
    }
}
