using Restbucks.Service.Representations;

namespace Restbucks.Service.Activities
{
    public interface IReadReceiptActivity
    {
        ReceiptRepresentation Read(int orderId, string baseUri);
    }
}