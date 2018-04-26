
namespace TurnBased
{
    public interface ITurnAction<T> where T : ITurnEntity
    {
        bool CanTakeAction(T turnEntity);

        void TakeAction(T turnEntity);
    }
}
