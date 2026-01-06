using TournamentLibrary.Groups;
using TournamentWebTest.Utilities;

namespace TournamentWebTest.Model
{
    public class WebGroup
    {
        public Group? Group { get; set; }
        public string Name => Group != null ? Group.Name : string.Empty;

        public List<TableEntry> Entries { get; set; } = new List<TableEntry>();

        public WebGroup() { }
        public WebGroup(Group group) 
        {
            Group = group;
        }

        public void Refresh()
        {
            if (Group != null)
            {
                Group.GetTable().RefreshTableEntryList(Entries);
            }
        }
    }
}
