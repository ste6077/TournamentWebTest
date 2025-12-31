using TournamentLibrary.Matches;
using TournamentLibrary.Teams;

namespace TournamentWebTest.Model
{
    public class MatchEntry
    {
        public DateTime Time { get; set; } = DateTime.Now;
        public int Sequence { get; set; }
        public int GameNumber { get; set; }
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

        public MatchEntry() { }
        public MatchEntry(TournamentLibrary.Matches.Match match)
        {
            try
            {
                Group = match.GroupName;
                TeamA = match.TeamA;
                TeamB = match.TeamB;
                NameTeamA = match.NameTeamA;
                NameTeamB = match.NameTeamB;
                MatchLink = match;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Fehler beim MatchEntry Konstruktor -> {match.OverallMatchNumber}: {e.Message}");
            }

        }
    }
}
