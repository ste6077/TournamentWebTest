using System.ComponentModel;
using System.Reflection;
using TournamentLibrary.Matches;
using TournamentLibrary.Tables;
using TournamentLibrary.Types;
using TournamentWebTest.Model;
using TournamentWebTest.Services;

namespace TournamentWebTest.Utilities
{
    public static class Extensions
    {
        /// <summary>
        /// Enum Description auslesen
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute? attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                        as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }
        
        /// <summary>
        /// Matches aktualisieren
        /// </summary>
        /// <param name="matchEntries"></param>
        public static void RefreshMatches(this List<MatchEntry> matchEntries)
        {
            if (matchEntries == null)
                return;

            foreach(var m in matchEntries)
            {
                m.Refresh();
            }
        }

        /// <summary>
        /// TableEntry Liste aktualisieren
        /// </summary>
        /// <param name="tableItems"></param>
        /// <param name="tableEntries"></param>
        public static void RefreshTableEntryList(this List<TableItem> tableItems, List<TableEntry> tableEntries)
        {
            tableEntries.Clear();
            foreach (var p in tableItems)
            {
                tableEntries.Add(new TableEntry
                {
                    Place = (int)p.Place,
                    Name = p.Team.Name,
                    Team = p.Team,
                    Played = p.Statistics.Games,
                    Wins = p.Statistics.Win,
                    Draws = p.Statistics.Draw,
                    Losses = p.Statistics.Loss,
                    GoalsFor = p.Statistics.GoalsFor,
                    GoalsAgainst = p.Statistics.GoalsAgainst
                });
            }
        }

        /// <summary>
        /// Schedule Liste aktualisieren
        /// </summary>
        /// <param name="matches"></param>
        /// <param name="entries"></param>
        public static void RefreshScheduleList(this List<Match> matches, List<MatchEntry> entries, bool setToFinished)
        {
            entries.Clear();
            entries.AddRange(InitializeSchedule(matches, setToFinished));
        }

        /// <summary>
        /// Schedule Liste aktualisieren
        /// </summary>
        /// <param name="matches"></param>
        /// <param name="entries"></param>
        public static void RefreshScheduleList(this Fixture matches, List<MatchEntry> entries, bool setToFinished)
        {
            entries.Clear();
            entries.AddRange(InitializeSchedule(matches, setToFinished));
        }

        /// <summary>
        /// Spielplan initialisieren
        /// </summary>
        /// <param name="matches"></param>
        /// <param name="setToFinished"></param>
        /// <returns></returns>
        private static List<MatchEntry> InitializeSchedule(List<Match> matches, bool setToFinished)
        {
            if (matches == null || !matches.Any())
            {
                Console.WriteLine("Keine Matches gefunden..");
                return new List<MatchEntry>();
            }

            try
            {
                var list = GetMatchEntries(matches);

                if (setToFinished)
                {
                    foreach (var match in matches)
                    {
                        match.State = match.HasGroup && match.Group.HasGroupStage && match.Group.GroupStage.Round == RoundType.GroupStage1 ? State.Finished : match.State;
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Initialisieren des Spielplans: " + ex.Message);
            }

            return new List<MatchEntry>();
        }

        /// <summary>
        /// MatchEntries aus Matches generieren
        /// </summary>
        /// <param name="matches"></param>
        /// <returns></returns>
        private static List<MatchEntry> GetMatchEntries(List<Match> matches)
        {
            var list = new List<MatchEntry>();


            foreach (var match in matches)
            {
                try
                {
                    list.Add(new MatchEntry(match));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Fehler beim Generieren der MatchEntries: " + ex.Message);
                }
            }
            return list;
        }
    }
}
