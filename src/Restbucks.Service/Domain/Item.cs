namespace Restbucks.Service.Domain
{
    public class Item
    {
        private readonly Size _size;
        private readonly Milk _milk;
        private readonly Drink _drink;

        public Milk Milk
        {
            get { return _milk; }
        }

        public Size Size
        {
            get { return _size; }
        }

        public Drink Drink
        {
            get { return _drink; }
        }

        public Item(Drink drink, Size size, Milk milk)
        {
            _milk = milk;
            _drink = drink;
            _size = size;
        }
    }
}