using System.Linq;
using Restbucks.Service.Domain;
using Restbucks.Service.Representations;

namespace Restbucks.Service.Mappers
{
    public class OrderRepresentationMapper
    {
        private readonly ItemRepresentationMapper _itemMapper;

        public OrderRepresentationMapper(ItemRepresentationMapper itemMapper)
        {
            _itemMapper = itemMapper;
        }

        public OrderRepresentation GetRepresentation(Order order)
        {
            return new OrderRepresentation
                       {
                           Cost = order.CalculateTotal(),
                           Items = order.Items.Select(_itemMapper.GetRepresentation).ToList(),
                           Location = order.Location,
                           Status = order.Status,
                       };
        }

        public Order GetDomainObject(OrderRepresentation orderRepresentation)
        {
            return new Order(orderRepresentation.Location, orderRepresentation.Items.Select(_itemMapper.GetDomainObject));
        }
    }
}