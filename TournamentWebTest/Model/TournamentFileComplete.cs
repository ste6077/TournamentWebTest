using TournamentLibrary;
using TournamentLibrary.Teams;
using TourFile = TournamentLibrary.Saving.File;

namespace TournamentWebTest.Model
{
    public class TournamentFileComplete
    {

        public TourFile? File { get; set; }
        public List<TournamentLinkedFile>? LinkedFiles { get; set; } = new List<TournamentLinkedFile>();

        public TournamentFileComplete()
        {
            
        }

        public TournamentFileComplete(Tournament tournament)
        {
            File = TourFile.LoadFromTournament(tournament);
            LinkedFiles = File.Teams;
        }

        public Tournament? GetTournament()
        {
            if (File == null)
                return null;

            File.Teams = [.. LinkedFiles];

            return File.GetTournament();
        }
    }
}
