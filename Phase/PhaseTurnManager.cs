using System;
using System.Collections.Generic;
using Shared;

namespace TurnBased
{
    public class PhaseTurnManager : TurnManager
    {
        public event Action<int> TurnPhaseChanged;

        private readonly List<int> turnPhases;
        private int currentTurnPhaseIndex;

        public int CurrentTurnPhase {  get { return turnPhases[currentTurnPhaseIndex]; } }

        public PhaseTurnManager(List<int> turnPhases, List<List<ITurnEntity>> teamsList) : base(teamsList)
        {
            if (turnPhases == null)
            {
                throw new ArgumentNullException("turnPhases");
            }

            if (turnPhases.Count < 1)
            {
                throw new ArgumentException("Must have at least one turn phase");
            }

            this.turnPhases = turnPhases;
            
            currentTurnPhaseIndex = 0;
        }

        public override void ChangeTurn()
        {
            currentTurnPhaseIndex = 0;
            base.ChangeTurn();
        }

        public bool HasNextPhase()
        {
            return currentTurnPhaseIndex < (turnPhases.Count - 1);
        }

        public void ChangeTurnPhase()
        {
            if (!HasNextPhase())
            {
                throw new InvalidOperationException("No next phase to move to");
            }
            TurnPhaseChanged.Dispatch(currentTurnPhaseIndex + 1);
            currentTurnPhaseIndex++;
        }
    }
}
