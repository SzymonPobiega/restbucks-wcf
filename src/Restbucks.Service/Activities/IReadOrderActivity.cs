using Restbucks.Service.Representations;

namespace Restbucks.Service.Activities
{
    public interface IReadOrderActivity
    {
        OrderRepresentation Read(int orderId, string baseUri);
    }
}