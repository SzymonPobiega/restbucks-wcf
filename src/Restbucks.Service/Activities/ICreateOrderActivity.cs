using Restbucks.Service.Representations;

namespace Restbucks.Service.Activities
{
    public interface ICreateOrderActivity
    {
        OrderRepresentation Create(OrderRepresentation orderRepresentation, string baseUri);
    }
}