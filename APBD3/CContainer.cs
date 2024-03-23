namespace APBD3;

public class CContainer(
    double loadWeight,
    double totalHeight,
    double ownWeight,
    double depth,
    double maxCapacity,
    Product productType,
    double temperatureInside,
    char symbol = 'C') : Container(loadWeight, totalHeight, ownWeight, depth, maxCapacity, symbol)
{
    private Dictionary<Product, double> _products = new()
    {
        { Product.Bananas, 13.3 },
        { Product.Chocolate, 18 },
        { Product.Fish, 2 },
        { Product.Meat, -15 },
        { Product.IceCream, -18 },
        { Product.FrozenPizza, -30 },
        { Product.Cheese, 7.2 },
        { Product.Sausages, 5 },
        { Product.Butter, 20.5 },
        { Product.Eggs, 19 }
    };

    public Product ProductType { get; } = productType;

    public double TemperatureInside { get; set; } = temperatureInside;

    public void AddWeight(double weight, Product type)
    {
        if (type.Equals(ProductType) && TemperatureInside >= _products[type])
        {
            base.AddWeight(weight);
        }
        else
        {
            Console.WriteLine("Type or temperature in container is incorrect");
        }
    }

    public override string ToString()
    {
        return $"{base.ToString()}, " +
               $"{nameof(ProductType)}: {ProductType}, " +
               $"{nameof(TemperatureInside)}: {TemperatureInside}";
    }
}