
namespace TurnBased
{
    public interface ITurnAction
    {
        bool CanTakeAction(ITurnEntity turnEntity);

        void TakeAction(ITurnEntity turnEntity);
    }
}
