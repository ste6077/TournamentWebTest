using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using System.Reflection;
using TournamentLibrary;
using TournamentLibrary.Teams;
using TournamentLibrary.Utilities;
using TournamentWebTest.Model;
using TournamentWebTest.Services;
using static System.Net.WebRequestMethods;

namespace TournamentWebTest.Utilities
{
    public static class Wizard
    {

        #region BWK 2026

        /// <summary>
        /// Arena Cup 2026 Turnier mit festen Teams
        /// </summary>
        /// <returns></returns>
        public static Tournament GetBwk2026Tournament()
        {
            var config = TournamentLibrary.Modes.BwkArenaCup2026.GetDefaultConfiguration();
            config.Name = $"BWK Arena Cup 2026";

            var teams = TeamService.Teams;

            config.Teams.AddRange(GetArenaCup2026Teams(teams));
            config.NonCompetitiveTeams.Add(teams.Find(t => t.Id == TeamEnum.TSV_Ilshofen.GetDescription()));
            var tmpTeams = new TeamList(config.Teams);

            foreach (var group in config.GroupStage1.Groups)
            {
                group.Teams.Clear();
                group.Teams.AddRange(tmpTeams.Take((int)config.GroupStage1.Settings.GroupSize).ToList());
                int slotIndex = 0;
                group.Teams.ForEach(t =>
                {
                    group.Settings.GroupSlots[slotIndex].Team = t;
                    t.GroupSlotStage1 = group.Settings.GroupSlots[slotIndex];
                    slotIndex++;
                    tmpTeams.Remove(t);
                });
            }

            int cntr = 0;
            foreach (var match in config.ShowMatches)
            {
                match.TeamA = config.NonCompetitiveTeams.FirstOrDefault();

                switch (cntr)
                {
                    case 0:
                        match.TeamB = config.Teams.Find(t => t.Id == TeamEnum.Ajax_Amsterdam.GetDescription());
                        break;
                    case 1:
                        match.TeamB = config.Teams.Find(t => t.Id == TeamEnum.Borussia_M_Gladbach.GetDescription());
                        break;
                    case 2:
                        match.TeamB = config.Teams.Find(t => t.Id == TeamEnum.VfB_Stuttgart.GetDescription());
                        break;
                }

                cntr++;
            }

            var tournament = new Tournament(config);
            return tournament;
        }

        #endregion

        #region Layer 2026

        public static Tournament GetHartmutLayer2026()
        {
            var config = TournamentLibrary.Modes.HartmutLayerCup2026.GetDefaultConfiguration();
            config.Name = $"Hartmut Layer Cup 2026";

            var teams = TeamService.Teams;

            config.Teams.AddRange(GetHartmutLayerCup2026Teams(teams));
            var tmpTeams = new TeamList(config.Teams);

            foreach (var group in config.GroupStage1.Groups)
            {
                group.Teams.Clear();
                group.Teams.AddRange(tmpTeams.Take((int)config.GroupStage1.Settings.GroupSize).ToList());
                int slotIndex = 0;
                group.Teams.ForEach(t =>
                {
                    group.Settings.GroupSlots[slotIndex].Team = t;
                    t.GroupSlotStage1 = group.Settings.GroupSlots[slotIndex];
                    slotIndex++;
                    tmpTeams.Remove(t);
                });
            }

            var tournament = new Tournament(config);
            return tournament;
        }

        #endregion

        #region Sonstiges

        public static List<Team> GetArenaCup2026Teams(List<Team> teams)
        {
            var searcher = new List<TeamEnum>()
            {
                TeamEnum.VfB_Stuttgart, TeamEnum.RB_Leipzig, TeamEnum.Bayer_04_Leverkusen, TeamEnum.Red_Bull_Akademie, TeamEnum.Ajax_Amsterdam,
                TeamEnum.VfL_Wolfsburg, TeamEnum._1_FSV_Mainz_05, TeamEnum.Borussia_Dortmund, TeamEnum.Borussia_M_Gladbach, TeamEnum.Sport_Lisboa_e_Benfica,
                TeamEnum.Eintracht_Frankfurt, TeamEnum.FC_Bayern_Muenchen, TeamEnum._1_FC_Koeln, TeamEnum.Hamburger_SV, TeamEnum.Manchester_City_FC,
            };
            var result = new List<Team>();
            foreach (var teamEnum in searcher)
            {
                var team = teams.Find(t => t.Id == teamEnum.GetDescription());
                if (team != null)
                {
                    result.Add(team);
                }
            }
            return result;
        }

        public static List<Team> GetHartmutLayerCup2026Teams(List<Team> teams)
        {
            var searcher = new List<TeamEnum>()
            {
                TeamEnum._1_FC_Heidenheim, TeamEnum._1_FC_Kaiserslautern, TeamEnum.VfB_Stuttgart, TeamEnum._1_FC_Nuernberg,
                TeamEnum.TSG_1899_Hoffenheim, TeamEnum.SV_Stuttgarter_Kickers, TeamEnum.FC_Basel, TeamEnum.TSV_Ilshofen,

            };
            var result = new List<Team>();
            foreach (var teamEnum in searcher)
            {
                var team = teams.Find(t => t.Id == teamEnum.GetDescription());
                if (team != null)
                {
                    result.Add(team);
                }
            }
            return result;
        }

        #endregion
    }
}
