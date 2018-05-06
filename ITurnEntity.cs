using Shared;

namespace TurnBased
{
    public interface ITurnEntity : IEntity
    {
        void Begin();

        void StartTurn();

        void EndTurn();
    }
}
