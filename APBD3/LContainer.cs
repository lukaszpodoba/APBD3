namespace APBD3;

public class LContainer(
    double loadWeight,
    double totalHeight,
    double ownWeight,
    double depth,
    double maxCapacity,
    char symbol = 'L') : Container(loadWeight, totalHeight, ownWeight, depth, maxCapacity, symbol), 
    IHazardNotifier
{
    
    public void DangerWarning(string serialNumber)
    {
        Console.WriteLine("Danger occured! Liquid Container number: " + serialNumber);
    }

    public void AddWeight(int weight, bool isHazardous)
    {
        var totalWeight = LoadWeight + weight;
        
        if ((isHazardous && totalWeight > MaxCapacity * 0.5) || totalWeight > MaxCapacity * 0.9 )
        {
            DangerWarning(SerialNumber);
        }
        
        base.AddWeight(weight);
    }

    public override string ToString()
    {
        return $"{base.ToString()}";
    }
}