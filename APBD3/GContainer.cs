namespace APBD3;

public class GContainer(
    double loadWeight,
    double totalHeight,
    double ownWeight,
    double depth,
    double maxCapacity,
    double pressure,
    char symbol = 'G') : Container(loadWeight, totalHeight, ownWeight, depth, maxCapacity, symbol),
    IHazardNotifier
{
    
    public double Pressure { get; set; } = pressure;
    
    public override void EmptyTheLoad()
    {
        LoadWeight *= 0.05;
    }

    public void DangerWarning(string serialNumber)
    {
        Console.WriteLine("Danger occured! Gas Container number: " + serialNumber);
    }

    public override string ToString()
    {
        return $"{base.ToString()}, " +
               $"{nameof(Pressure)}: {Pressure}";
    }
}