
namespace TurnBased
{
    public interface ITurnEntity
    {
        void Begin();

        void StartTurn();

        void EndTurn();
    }
}

