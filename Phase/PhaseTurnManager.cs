using System;
using System.Collections.Generic;
using Shared;

namespace TurnBased
{
    public class PhaseTurnManager<TEnum> : TurnManager where TEnum : struct, IConvertible, IComparable, IFormattable
    {
        public event Action<TEnum> TurnPhaseChanged;

        private readonly List<TEnum> turnPhases;
        private int currentTurnPhaseIndex;

        public TEnum CurrentTurnPhase {  get { return turnPhases[currentTurnPhaseIndex]; } }

        public PhaseTurnManager(List<TEnum> turnPhases, List<List<ITurnEntity>> teamsList) : base(teamsList)
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
            TurnPhaseChanged.Dispatch(CurrentTurnPhase);
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
            currentTurnPhaseIndex++;
            TurnPhaseChanged.Dispatch(CurrentTurnPhase);
        }
    }
}
