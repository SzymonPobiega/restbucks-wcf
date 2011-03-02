using System;

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

        public decimal CalculateCost()
        {
            return GetBasePrice(Drink) * GetSizeMultiplier(Size);
        }

        public static decimal GetBasePrice(Drink drink)
        {
            switch (drink)
            {
                case Drink.Cappuccino:
                    return 1.5m;
                case Drink.Espresso:
                    return 1;
                case Drink.Latte:
                    return 1.6m;
                case Drink.FlatWhite:
                    return 1.2m;
                default:
                    throw new NotSupportedException();
            }
        }

        public static decimal GetSizeMultiplier(Size size)
        {
            switch (size)
            {
                case Size.Small:
                    return 0.8m;
                case Size.Medium:
                    return 1;
                case Size.Large:
                    return 1.2m;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}