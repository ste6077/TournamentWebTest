using TourFile = TournamentLibrary.Saving.File;

namespace TournamentWebTest.Model
{
    public class BackupFile
    {
        public List<MatchEntryFileProperties> Matches { get; set; } = new List<MatchEntryFileProperties>();
        public BackupFile()
        {
            
        }

        public BackupFile(List<MatchEntry> matches)
        {
            Matches = matches.Select(m => new MatchEntryFileProperties
            {
                GameNumber = m.GameNumber,
                ScoreA = m.ScoreA,
                ScoreB = m.ScoreB,
                GoldenGoalRound = m.GoldenGoalRound,
                GoldenGoalSecond = m.GoldenGoalSecond,
                IsFinished = m.MatchLink?.IsFinished ?? false
            }).ToList();
        }
    }
}
