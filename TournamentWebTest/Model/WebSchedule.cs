using TournamentLibrary.Matches;
using TournamentWebTest.Utilities;

namespace TournamentWebTest.Model
{
    public class WebSchedule
    {
        public List<MatchEntry> MatchEntries { get; set; } = new List<MatchEntry>();
        public string Title { get; set; } = "";

        public WebSchedule() { }
        public WebSchedule(List<Match> matchEntries, string title)
        {
            Title = title;
            Refresh(matchEntries);
        }

        public void Refresh(List<Match> matchEntries)
        {
            if (matchEntries != null)
            {
                matchEntries.RefreshScheduleList(MatchEntries, false);
            }
        }
    }
}
