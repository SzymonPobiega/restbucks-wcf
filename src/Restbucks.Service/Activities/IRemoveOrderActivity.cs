using Restbucks.Service.Representations;

namespace Restbucks.Service.Activities
{
    public interface IRemoveOrderActivity
    {
        OrderRepresentation Remove(int orderId);
    }
}