using Microsoft.AspNetCore.Components;
using TournamentLibrary;
using TournamentLibrary.Groups;
using TournamentLibrary.Groupstage;
using TournamentLibrary.Matches;
using TournamentLibrary.Teams;

namespace TournamentWebTest.Services
{
    public class TableTestService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public bool IsLoaded { get; private set; } = false;

        public Group TestGroupStage1 { get; private set; }
        public Group TestGroupStage2 { get; private set; }
        public TournamentLibrary.Tournament Tournament { get; private set; }

        public TableTestService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;

            Tournament = new Tournament(new object()) { Autosave = false };
            TournamentLibrary.Timers.Timer timer = new TournamentLibrary.Timers.Timer();
            timer.SetTime(new TimeSpan(0, 10, 0)); // 10 minutes
            timer.Tick += Timer_Tick;
            timer.Start();

            var teams = new List<Team>
            {
                new Team("Borussia Dortmund"),
                new Team("VfB Stuttgart"),
                new Team("Manchester City"),
                new Team("1. FC Köln"),
                new Team("FC Bayern München"),
            };

            foreach(var t in teams)
            {
                Tournament.Teams.Add(t);
            }

            InitStage1(teams);
            teams.RemoveAt(0);
            teams.RemoveAt(0);
            InitStage2(teams);
        }

        public string CurrentTime { get; set; } = "unknown";

        private void Timer_Tick(object? sender, TimeSpan e)
        {
            CurrentTime = e.ToString(@"hh\:mm\:ss");
        }

        private void InitStage1(List<Team> teams)
        {
            Tournament.GroupStage1 = new GroupStage("Stage 1", TournamentLibrary.Types.RoundType.GroupStage1)
            {
                Settings = new TournamentLibrary.Groupstage.Settings { AmountGroups = 1, GroupSize = 5 }
            };
            TestGroupStage1 = new Group("Test Group Stage 1", Tournament.GroupStage1, new TeamList(teams));
            Tournament.GroupStage1.AddGroup(TestGroupStage1);
            var team1 = teams[0];
            var team2 = teams[1];
            var team3 = teams[2];
            var team4 = teams[3];
            var team5 = teams[4];
            int i = 1;
            Tournament.Fixtures.AddRange(new List<Match>
            {
                new Match() { OverallMatchNumber = i++, TeamA = team1, TeamB = team2, Round = TournamentLibrary.Types.RoundType.GroupStage1, State = State.Finished, Group = TestGroupStage1 },
                new Match() { OverallMatchNumber = i++, TeamA = team3, TeamB = team4, Round = TournamentLibrary.Types.RoundType.GroupStage1, State = State.Finished, Group = TestGroupStage1 },
                new Match() { OverallMatchNumber = i++, TeamA = team5, TeamB = team1, Round = TournamentLibrary.Types.RoundType.GroupStage1, State = State.Finished, Group = TestGroupStage1 },
                new Match() { OverallMatchNumber = i++, TeamA = team2, TeamB = team3, Round = TournamentLibrary.Types.RoundType.GroupStage1, State = State.Finished, Group = TestGroupStage1 },
                new Match() { OverallMatchNumber = i++, TeamA = team4, TeamB = team5, Round = TournamentLibrary.Types.RoundType.GroupStage1, State = State.Finished, Group = TestGroupStage1 },
                new Match() { OverallMatchNumber = i++, TeamA = team3, TeamB = team1, Round = TournamentLibrary.Types.RoundType.GroupStage1, State = State.Finished, Group = TestGroupStage1 },
                new Match() { OverallMatchNumber = i++, TeamA = team4, TeamB = team2, Round = TournamentLibrary.Types.RoundType.GroupStage1, State = State.Finished, Group = TestGroupStage1 },
                new Match() { OverallMatchNumber = i++, TeamA = team5, TeamB = team3, Round = TournamentLibrary.Types.RoundType.GroupStage1, State = State.Finished, Group = TestGroupStage1 },
                new Match() { OverallMatchNumber = i++, TeamA = team1, TeamB = team4, Round = TournamentLibrary.Types.RoundType.GroupStage1, State = State.Finished, Group = TestGroupStage1 },
                new Match() { OverallMatchNumber = i++, TeamA = team2, TeamB = team5, Round = TournamentLibrary.Types.RoundType.GroupStage1, State = State.Finished, Group = TestGroupStage1 },
            });
        }

        private void InitStage2(List<Team> teams)
        {
            Tournament.GroupStage2 = new GroupStage("Stage 2", TournamentLibrary.Types.RoundType.GroupStage2)
            {
                Settings = new TournamentLibrary.Groupstage.Settings { AmountGroups = 1, GroupSize = 3 }
            };
            TestGroupStage2 = new Group("Test Group Stage 2", Tournament.GroupStage2, new TeamList(teams));
            Tournament.GroupStage2.AddGroup(TestGroupStage2);

            var team1 = teams[0];
            var team2 = teams[1];
            var team3 = teams[2];
            int i = 11;
            Tournament.Fixtures.AddRange(new List<Match>
            {
                new Match() { OverallMatchNumber = i++, TeamA = team1, TeamB = team2, Round = TournamentLibrary.Types.RoundType.GroupStage2, State = State.Finished, Group = TestGroupStage2 },
                new Match() { OverallMatchNumber = i++, TeamA = team2, TeamB = team3, Round = TournamentLibrary.Types.RoundType.GroupStage2, State = State.Finished, Group = TestGroupStage2 },
                new Match() { OverallMatchNumber = i++, TeamA = team3, TeamB = team1, Round = TournamentLibrary.Types.RoundType.GroupStage2, State = State.Finished, Group = TestGroupStage2 },
            });
        }

    }
}
