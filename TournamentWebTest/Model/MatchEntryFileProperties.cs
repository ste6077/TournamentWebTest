using TournamentWebTest.Interfaces;

namespace TournamentWebTest.Model
{
    public class MatchEntryFileProperties : IMatchEntry
    {
        public string GameNumber { get; set; }
        public int ScoreA { get; set; }
        public int ScoreB { get; set; }
        public string NameTeamA { get; set; } = "";
        public string NameTeamB { get; set; } = "";
        public int GoldenGoalRound { get; set; }
        public int GoldenGoalSecond { get; set; }
        public bool IsFinished { get; set; }

        public MatchEntryFileProperties()
        {
            
        }

        public MatchEntryFileProperties(MatchEntry entry)
        {
            TournamentLibrary.Utilities.Conversion.CopyDataWithSameName(entry, this);
            IsFinished = entry.MatchLink?.IsFinished ?? false;
        }
    }
}
