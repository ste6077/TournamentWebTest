using TournamentLibrary;
using TournamentLibrary.Matches;
using TournamentWebTest.Utilities;
using TournamentLibrary.Types;

namespace TournamentWebTest.Services
{
    public class SimulationService
    {
        public Tournament Tournament { get; set; } = null;

        #region Events

        public event EventHandler<TimeSpan> OnMatchTimeChanged;

        public event Action? StateChanged;

        #endregion

        public SimulationService() { }

        #region Turniererstellung

        public void CreateTournament(TournamentMode mode)
        {
            if (mode == TournamentMode.Bwk2026)
                CreateArenaCup2026();

            if (mode == TournamentMode.HartmutLayerCup2026)
                CreateHartmutLayerCup2026();
        }

        /// <summary>
        /// Arena Cup erstellen
        /// </summary>
        public void CreateArenaCup2026()
        {
            if (!CreateTournament(Wizard.GetBwk2026Tournament))
                return;
        }

        /// <summary>
        /// Hartmut Layer Cup erstellen
        /// </summary>
        public void CreateHartmutLayerCup2026()
        {
            if (!CreateTournament(Wizard.GetHartmutLayer2026))
                return;
        }

        /// <summary>
        /// Turnier erstellen
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        private bool CreateTournament(Func<Tournament> action)
        {
            var tmp = action.Invoke();

            if (tmp == null)
                return false;

            if (Tournament != null)
            {
                Tournament.MatchTimer_Tick -= Tournament_MatchTimer_Tick;
                Tournament.CurrentMatch_StateChanged -= Tournament_CurrentMatch_StateChanged;
            }

            Tournament = tmp;
            Tournament.Start();
            Tournament.MatchTimer_Tick += Tournament_MatchTimer_Tick;
            Tournament.CurrentMatch_StateChanged += Tournament_CurrentMatch_StateChanged;
            SetNextMatch();
            return true;
        }


        #endregion

        #region Match Handling

        /// <summary>
        /// Aktuelles Match
        /// </summary>
        /// <returns></returns>
        public Match? GetCurrentMatch() => Tournament != null ? Tournament.CurrentMatch : null;

        /// <summary>
        /// Nächste Match setzen
        /// </summary>
        /// <returns></returns>
        public Match? SetNextMatch()
        {
            if (Tournament == null)
                return null;
            
            Tournament.SetNextMatch();
            var matchup = Tournament.CurrentMatch != null ? Tournament.CurrentMatch.GetDebugMatchText() : "NULL";
            Console.WriteLine($"Nächstes Match erstellt: {matchup}");
            Notify();
            return Tournament.CurrentMatch;
        }

        /// <summary>
        /// Match Timer Tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tournament_MatchTimer_Tick(object? sender, TimeSpan e)
        {
            //OnMatchTimeChanged?.BeginInvoke(this, e, null, null);
            //Notify();
        }

        /// <summary>
        /// Match State changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tournament_CurrentMatch_StateChanged(object? sender, State e)
        {
            Notify();
        }


        /// <summary>
        /// Start Match
        /// </summary>
        public void StartMatch()
        {
            Tournament?.StartMatch();
            Notify();
        }

        /// <summary>
        /// Stop Match
        /// </summary>
        public void StopMatch()
        {
            Tournament?.StopMatch();
            Notify();
        }

        /// <summary>
        /// Pause Match
        /// </summary>
        public void PauseMatch()
        {
            Tournament?.PauseMatch();
            Notify();
        }

        /// <summary>
        /// Continue Match
        /// </summary>
        public void ContinueMatch()
        {
            Tournament?.ContinueMatch();
            Notify();
        }

        #endregion

        #region Tore

        /// <summary>
        /// Tor hinzufügen
        /// </summary>
        /// <param name="a"></param>
        public void AddGoal(bool a)
        {
            if (a)
                Tournament?.AddGoalTeamA();
            else
                Tournament?.AddGoalTeamB();
            Console.WriteLine("Goal added");
            Notify();
        }

        /// <summary>
        /// Tor entfernen
        /// </summary>
        /// <param name="a"></param>
        public void RemoveGoal(bool a)
        {
            if (a)
                Tournament?.RemoveLastGoalTeamA();
            else
                Tournament?.RemoveLastGoalTeamB();
            Console.WriteLine("Goal removed");
            Notify();
        }

        #endregion

        #region Notify

        /// <summary>
        /// Notify
        /// </summary>
        private void Notify() => StateChanged?.Invoke();

        #endregion
    }
}
