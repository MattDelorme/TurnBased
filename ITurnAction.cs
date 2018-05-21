
namespace TurnBased
{
    public interface ITurnAction<T> where T : ITurnEntity
    {
        bool CanTakeAnyAction(T turnEntity);

        bool CanTakeAction(T turnEntity);

        void TakeAction(T turnEntity);
    }
}
