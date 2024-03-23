namespace APBD3;

public abstract class Container(
    double loadWeight,
    double totalHeight,
    double ownWeight,
    double depth,
    double maxCapacity,
    char symbol
)
{
    private static int _nextId = 1;
    
    public double LoadWeight { get; protected set; } = loadWeight;
    public double TotalHeight { get; private set; } = totalHeight;
    public double OwnWeight { get; protected set; } = ownWeight;
    public double Depth { get; protected set; } = depth;
    public double MaxCapacity { get; protected set; } = maxCapacity;
    public string SerialNumber { get; } = "KON-" + symbol +  "-" + _nextId++;
    //public string SerialNumber => "KON-" + symbol +  "-" + Id;

    
    public virtual void EmptyTheLoad()
    {
        LoadWeight = 0;
    }

    protected void AddWeight(double weight)
    {
        if (LoadWeight + weight > MaxCapacity)
        {
            throw new OverfillException("load weight exceeded");
        }

        LoadWeight += weight;
    }

    public override string ToString()
    {
        return $"{nameof(LoadWeight)}: {LoadWeight}, " +
               $"{nameof(TotalHeight)}: {TotalHeight}, " +
               $"{nameof(OwnWeight)}: {OwnWeight}, " +
               $"{nameof(Depth)}: {Depth}, " +
               $"{nameof(MaxCapacity)}: {MaxCapacity}, " +
               $"{nameof(SerialNumber)}: {SerialNumber}";
    }
}

internal class OverfillException(string message) : Exception(message);