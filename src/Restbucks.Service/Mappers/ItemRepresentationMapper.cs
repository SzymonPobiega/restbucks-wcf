using Restbucks.Service.Domain;
using Restbucks.Service.Representations;

namespace Restbucks.Service.Mappers
{
    public class ItemRepresentationMapper
    {
        public ItemRepresentation GetRepresentation(Item item)
        {
            return new ItemRepresentation
                       {
                           Drink = item.Drink,
                           Milk = item.Milk,
                           Size = item.Size
                       };
        }

        public Item GetDomainObject(ItemRepresentation itemRepresentation)
        {
            return new Item(itemRepresentation.Drink, itemRepresentation.Size, itemRepresentation.Milk);
        }
    }
}