namespace Restbucks.Service.Domain
{
    public interface IOrderRepository
    {
        Order FindById(int orderId);
        int Store(Order order);
    }
}