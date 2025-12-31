using TournamentLibrary.Teams;

namespace TournamentWebTest.Model
{
    public class FinalTableItem
    {

        public int Position { get; set; }
        public Team? Team { get; set; }

        public string TeamName => Team != null ? Team.Name : "–";

        public FinalTableItem()
        {
        }

        public FinalTableItem(int position, Team team)
        {
            Position = position;
            Team = team;
        }
    }
}
