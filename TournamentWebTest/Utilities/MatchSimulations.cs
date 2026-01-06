using System;
using TournamentLibrary;
using TournamentLibrary.Matches;

namespace TournamentWebTest.Utilities
{
    public static class MatchSimulations
    {
        private static Random _random = new Random();

        public static void SimulateFixtures(Fixture matches)
        {

        }

        public static void SetScoreAndFinish(Match match, int scoreA, int scoreB)
        {
            if (match == null)
            {
                return;
            }
            SetScore(match, scoreA, scoreB);
        }

        /// <summary>
        /// Ergebnis setzen
        /// </summary>
        /// <param name="match"></param>
        /// <param name="scoreA"></param>
        /// <param name="scoreB"></param>
        public static void SetScore(Match match, int scoreA, int scoreB)
        {
            if (match == null)
            {
                return;
            }

            for (int i = 0; i < scoreA; i++)
            {
                if (match.TeamA != null && match.TeamA.Players != null && match.TeamA.Players.Any())
                {
                    match.AddGoalTeamA(new Tuple<int, TimeSpan>(i + 1, new TimeSpan(0, i + 1, 0)), match.TeamA.Players[_random.Next(0, match.TeamA.Players.Count)]);
                }
                else
                {
                    match.AddGoalTeamA(new Tuple<int, TimeSpan>(i + 1, new TimeSpan(0, i + 1, 0)));
                }
            }

            for (int i = 0; i < scoreB; i++)
            {
                if (match.TeamB != null && match.TeamB.Players != null && match.TeamB.Players.Any())
                {
                    match.AddGoalTeamB(new Tuple<int, TimeSpan>(i + 1, new TimeSpan(0, i + 1, 0)), match.TeamB.Players[_random.Next(0, match.TeamB.Players.Count)]);
                }
                else
                {
                    match.AddGoalTeamB(new Tuple<int, TimeSpan>(i + 1, new TimeSpan(0, i + 1, 0)));
                }

            }
        }

        /// <summary>
        /// Match simulieren
        /// </summary>
        /// <param name="match"></param>
        public static void SimulateMatch(Match match)
        {
            if (match == null)
            {
                return;
            }

            var a = _random.Next(0, 10);
            var b = _random.Next(0, 10);

            while (a == b && match.IsPlayoffsMatch)
            {
                b = _random.Next(0, 10);
            }

            match.ClearGoals();

            SetScore(match, a, b);
            if (match.HasNotBeenStarted)
            {
                match.TimestampStarted = DateTime.Now;
            }

            match.State = TournamentLibrary.Matches.State.Finished;
            match.TimestampEnded = DateTime.Now;
        }
    }
}
