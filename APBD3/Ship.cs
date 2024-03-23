namespace APBD3;

public class Ship(
    double speed,
    int maxContainerAmount,
    double maxTransportWeight
    )
{ 
    private static int _nextId = 1;
    public double Speed { get; set; } = speed;
    public int MaxContainerAmount { get; set; } = maxContainerAmount;
    public double MaxTransportWeight { get; set; } = maxTransportWeight * 1000;

    public string ShipNumber { get; } = "S-" + _nextId++; 

    public List<Container> Containers = new();
    
    public void AddContainer(Container container)
    {
        double currentWeight = Containers.Sum(c => c.LoadWeight + c.OwnWeight);
        
        if (Containers.Count > MaxContainerAmount)
        {
            Console.WriteLine("Maximum number of containers has been reached");
        }
        else if (currentWeight > MaxTransportWeight)
        {
            Console.WriteLine("Maximum transport weight has been reached");
        }
        else
        {
            Containers.Add(container);   
        }
    }
    
    public void AddContainer(List<Container> con)
    {
        double receivedListWeight = con.Sum(c => c.LoadWeight + c.OwnWeight);
        double currentWeight = Containers.Sum(c => c.LoadWeight + c.OwnWeight);

        if (Containers.Count + con.Count > MaxContainerAmount)
        {
            Console.WriteLine("Maximum number of containers has been reached");
        }
        else if (currentWeight + receivedListWeight > MaxTransportWeight)
        {
            Console.WriteLine("Maximum transport weight has been reached");
        }
        else
        {
            foreach (var c in con)
            {
                Containers.Add(c);
            }   
        }
    }

    public void DeleteContainer(string number)
    {
        foreach (var c in Containers)
        {
            if (number.Equals(c.SerialNumber))
            {
                Containers.Remove(c);
            }
        }
    }

    public void EmptyTheShip()
    {
        Containers.Clear();
    }

    public void ChangeContainer(string toDelete, Container toAdd)
    {
        double currentWeight = Containers.Sum(c => c.LoadWeight + c.OwnWeight);
        double toAddWeight = toAdd.LoadWeight + toAdd.OwnWeight;
        
        int toDeleteIndex = Containers.FindIndex(c => c.SerialNumber.Equals(toDelete));
        double toDeleteWeight = Containers[toDeleteIndex].LoadWeight + Containers[toDeleteIndex].OwnWeight;
        
        if (currentWeight + toAddWeight - toDeleteWeight <= maxTransportWeight)
        {
            Containers.RemoveAll(container => container.SerialNumber == toDelete);
            AddContainer(toAdd);
        }
        else
        {
            Console.WriteLine("New container weight is too big");
        }
    }

    public void TransferContainer(Ship ship, string toTransfer)
    {
        int toTransferIndex = Containers.FindIndex(c => c.SerialNumber.Equals(toTransfer));
        double shipWeight = ship.Containers.Sum(c => c.LoadWeight + c.OwnWeight);
        double toTransferWeight = Containers[toTransferIndex].LoadWeight + Containers[toTransferIndex].OwnWeight;
        
        if (shipWeight + toTransferWeight <= ship.MaxTransportWeight && ship.Containers.Count  < ship.MaxContainerAmount)
        {
            Container containerToTransfer = Containers[toTransferIndex];
            Containers.RemoveAt(toTransferIndex);
            
            ship.Containers.Add(containerToTransfer);
        }
    }

    public void ContainerInfo(string serialNumber)
    {
        int index = Containers.FindIndex(c => c.SerialNumber.Equals(serialNumber));
        Console.WriteLine(Containers[index]);
    }
    
    public void ShipInfo()
    {
        Console.WriteLine("---------------------------");
        Console.WriteLine($"Ship number: {ShipNumber}");
        Console.WriteLine($"Ship Speed: {Speed}");
        Console.WriteLine($"Maximum Container Amount: {MaxContainerAmount}");
        Console.WriteLine($"Maximum Transport Weight: {MaxTransportWeight}");

        Console.WriteLine("Containers:");
        foreach (var container in Containers)
        {
            Console.WriteLine(container.ToString());
        }
        Console.WriteLine("---------------------------");
    }

}