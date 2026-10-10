using TournamentLibrary.TimePenalties;

namespace TournamentWebTest.Model
{
    public class TimePenaltyEntry
    {
        public string Time { get; set; } = "";

        public TimePenaltyEntry()
        {
            
        }

        public TimePenaltyEntry(TimePenalty timePenalty)
        {
            var tmp = timePenalty.RemainingPenaltyTime;
            Time = $"{tmp.Minutes}:{tmp.Seconds}.{tmp.Milliseconds}";
        }
    }
}
