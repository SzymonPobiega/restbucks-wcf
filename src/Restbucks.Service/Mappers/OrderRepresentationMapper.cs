using System.Linq;
using Restbucks.Service.Domain;
using Restbucks.Service.Representations;

namespace Restbucks.Service.Mappers
{
    public class OrderRepresentationMapper
    {
        public OrderRepresentation GetRepresentation(Order order)
        {
            var itemMapper = new ItemRepresentationMapper();
            return new OrderRepresentation
                       {
                           Cost = order.Total,
                           Items = order.Items.Select(itemMapper.GetRepresentation).ToList(),
                           Location = order.Location,
                           Status = order.Status,
                       };
        }
    }
}