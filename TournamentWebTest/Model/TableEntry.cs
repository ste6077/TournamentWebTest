using TournamentLibrary.Teams;

namespace TournamentWebTest.Model
{
    public class TableEntry
    {
        public int Place { get; set; } = 5;
        public string Name { get; set; } = "";
        public int Played { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int Points => Wins * 3 + Draws;
        public Team? Team { get; set; }
    }
}
