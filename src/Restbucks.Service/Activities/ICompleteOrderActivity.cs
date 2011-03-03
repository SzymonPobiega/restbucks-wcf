using Restbucks.Service.Representations;

namespace Restbucks.Service.Activities
{
    public interface ICompleteOrderActivity
    {
        OrderRepresentation Complete(int orderId);
    }
}