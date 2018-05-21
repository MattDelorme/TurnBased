using System;
using System.Collections.Generic;
using Shared;

namespace TurnBased
{
    public class TurnManager
    {
        public event Action<int> OnTurnChanged;

        private readonly List<List<ITurnEntity>> teamsList;

        public int CurrentTeamIndex { get; private set; }

        public TurnManager(List<List<ITurnEntity>> teamsList)
        {
            this.teamsList = teamsList;
        }

        public void AddTeam(List<ITurnEntity> team)
        {
            teamsList.Add(team);
        }

        public void Start()
        {
            for (int i = 0; i < teamsList.Count; i++)
            {
                teamBegin(teamsList[i]);
            }

            CurrentTeamIndex = 0;
            if (teamsList.Count > 1)
            {
                teamStartTurn(teamsList[CurrentTeamIndex]);
            }
        }

        private static void teamBegin(IList<ITurnEntity> team)
        {
            for (int i = 0; i < team.Count; i++)
            {
                team[i].Begin();
            }
        }

        private static void teamStartTurn(IList<ITurnEntity> team)
        {
            for (int i = 0; i < team.Count; i++)
            {
                team[i].StartTurn();
            }
        }

        private static void teamEndTurn(IList<ITurnEntity> team)
        {
            for (int i = 0; i < team.Count; i++)
            {
                team[i].EndTurn();
            }
        }

        public void TakeAction<T>(ITurnAction<T> turnAction, T turnEntity) where T : ITurnEntity
        {
            if (turnAction.CanTakeAction(turnEntity))
            {
                turnAction.TakeAction(turnEntity);
            }
        }

        public virtual void ChangeTurn()
        {
            teamEndTurn(teamsList[CurrentTeamIndex]);

            CurrentTeamIndex++;
            CurrentTeamIndex = CurrentTeamIndex < teamsList.Count ? CurrentTeamIndex : 0;

            teamStartTurn(teamsList[CurrentTeamIndex]);

            OnTurnChanged.Dispatch(CurrentTeamIndex);
        }
    }
}
