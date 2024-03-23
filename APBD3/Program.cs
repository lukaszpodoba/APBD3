namespace APBD3;

class MyClass
{
    static List<Ship> _ships = new();
    
    public static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Lista kontenerowców: ");
            if (_ships.Count == 0)
            {
                Console.WriteLine("Brak");
            }
            else
            {
                foreach (var ship in _ships)
                {
                    Console.WriteLine(
                        $"Ship number [{ship.ShipNumber}] (speed={ship.Speed}, maxContainerNum={ship.MaxContainerAmount}, maxWeight={ship.MaxTransportWeight})");
                }
            }

            Console.WriteLine("Lista kontenerów: ");
            bool anyContainers = false;

            foreach (var ship in _ships)
            {
                if (ship.Containers.Count > 0)
                {
                    anyContainers = true;

                    foreach (var container in ship.Containers)
                    {
                        Console.WriteLine($"[{ship.ShipNumber}] {container}");
                    }
                }
            }

            if (!anyContainers)
            {
                Console.WriteLine("Brak");
            }

            Console.WriteLine("1. Dodaj kontenerowiec");
            Console.WriteLine("2. Usun kontenerowiec");
            Console.WriteLine("3. Dodaj kontener");
            Console.WriteLine("4. Usun kontener");
            Console.WriteLine("5. Przenieś kontener na inny statek");
            Console.WriteLine("6. Wyświetl informacje na temat wybranego statku");
            Console.WriteLine("7. Zakończ działanie programu");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddShip();
                    break;
                case 2:
                    RemoveShip();
                    break;
                case 3:
                    AddCon();
                    break;
                case 4:
                    RemoveCon();
                    break;
                case 5:
                    TransferCon();
                    break;
                case 6:
                    PrintShipInfo();
                    break;
                case 7:
                    return;
            }
        }
    }
    
    private static void AddShip()
    {
        Console.WriteLine("Podaj prędkość statku: ");
        double speed = Convert.ToDouble(Console.ReadLine());
        
        Console.WriteLine("Podaj maksymalną ilość kontenerów: ");
        int maxContainerAmmount = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Podaj maksymalną wagę, jaką kontenerowiec może przetransportować: ");
        double maxTransportWeight = Convert.ToDouble(Console.ReadLine());
        
        _ships.Add(new Ship(speed, maxContainerAmmount, maxTransportWeight));
    }

    private static void RemoveShip()
    {
        if (_ships.Count == 0)
        {
            Console.WriteLine("Brak statków do usunięcia.");
            return;
        }
        
        Console.WriteLine("Podaj nazwę statku do usunięcia:");
        string? name = Console.ReadLine();

        Ship? shipToRemove = _ships.Find(s => s.ShipNumber.Equals(name));
        if (shipToRemove != null)
        {
            _ships.Remove(shipToRemove);
            Console.WriteLine("Usunięto statek");
        }
        else
        {
            Console.WriteLine("Nie znaleziono statku o podanej nazwie");
        }
    }

    private static void AddCon()
    {
        if (_ships.Count == 0)
        {
            Console.WriteLine("Najpierw musisz dodać kontenerowiec");
            return;
        }
        
        Console.WriteLine("Na który statek chcesz dodać kontener?");
        foreach (var ship in _ships)
        {
            Console.WriteLine(ship.ShipNumber);
        }
        string? pickedShip = Console.ReadLine();
        int index = _ships.FindIndex(c => c.ShipNumber.Equals(pickedShip));
        
        Console.WriteLine("Jaki rodzaj konteneru chcesz dodać? L - Płyny; G - Gas; C - Chłodniczy");
        string? containerType = Console.ReadLine();

        Console.WriteLine("Podaj wagę ładunku w kontenerze: ");
        double loadWeight = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Podaj wysokość kontenera: ");
        double totalHeight = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Podaj wagę własną kontenera: ");
        double ownWeight = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Podaj głębokość kontenera: ");
        double depth = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Maksymalna pojemność kontenera: ");
        double maxCapacity = Convert.ToDouble(Console.ReadLine());
        
        switch (containerType)
        {
            case "L":
                _ships[index].AddContainer(new LContainer(loadWeight, totalHeight ,ownWeight, depth, maxCapacity));
                break;
            case "G":
                Console.WriteLine("Podaj wartość ciśnienia w kontenerze: ");
                double pressure = Convert.ToDouble(Console.ReadLine());
                _ships[index].AddContainer(new GContainer(loadWeight, totalHeight, ownWeight, depth, maxCapacity, pressure));
                break;
            case "C":
                Console.WriteLine("Podaj rodzaj produktu w kontenerze: ");
                foreach (Product value in Enum.GetValues(typeof(Product)))
                {
                    Console.Write(value + ", ");
                }
                Console.WriteLine();
                
                Console.WriteLine("Podaj rodzaj produktu (wpisz odpowiednią liczbę):");
                foreach (var value in Enum.GetValues(typeof(Product)))
                {
                    Console.WriteLine($"{(int)value}: {value}");
                }

                int productTypeInput;
                bool isValidInput;
                do
                {
                    Console.Write("Wybierz typ produktu: ");
                    isValidInput = int.TryParse(Console.ReadLine(), out productTypeInput);

                    if (!isValidInput || !Enum.IsDefined(typeof(Product), productTypeInput))
                    {
                        Console.WriteLine("Błąd! Wprowadź prawidłową liczbę odpowiadającą typowi produktu.");
                    }
                } while (!isValidInput || !Enum.IsDefined(typeof(Product), productTypeInput));
                
                
                Console.WriteLine("Podaj temperaturę w środku kontenera: ");
                double temperatureInside = Convert.ToDouble(Console.ReadLine());

                CContainer cContainer = new CContainer(loadWeight, totalHeight, ownWeight, depth, maxCapacity,
                    (Product)productTypeInput, temperatureInside);
                
                _ships[index].AddContainer(cContainer);

                Console.WriteLine("Podaj ilość produktu, którą chcesz przechowywać w tym kontenerze: ");
                double productWeight = Convert.ToDouble(Console.ReadLine());
                cContainer.AddWeight(productWeight, (Product)productTypeInput);
                
                break;
        }
    }

    private static void RemoveCon()
    {
        Console.WriteLine("Wybierz statek, z którego chcesz usunąć kontener: ");
        int index = ShipIndex();

        if (_ships[index].Containers.Count == 0)
        {
            Console.WriteLine("Wybrany statek nie ma kontenerów do usunięcia");
            return;
        }

        Console.WriteLine("Czy chcesz usunąc wszystkie kontenery na statku? tak/nie");
        string? decision = Console.ReadLine();
        if (decision != null && decision.Equals("tak"))
        {
            _ships[index].EmptyTheShip();
        }
        else
        {
            Console.WriteLine("Wybierz kontener do usunięcia: ");
            foreach (var c in _ships[index].Containers)
            {
                Console.WriteLine(c.SerialNumber + ", ");
            }
            Console.WriteLine();
            string? number = Console.ReadLine();
            if (number != null) _ships[index].DeleteContainer(number);
        }
    }
    
    private static void TransferCon()
    {
        Console.WriteLine("Wybierz statek, z którego chcesz przenisć kontener: ");
        int fromIndex = ShipIndex();
        
        Console.WriteLine("Wybierz statek, na który checsz przenieść kontener: ");
        int toIndex = ShipIndex();

        Console.WriteLine("Wybierz kontener, który chciałbyś przenieść: ");
        foreach (var c in _ships[fromIndex].Containers)
        {
            Console.WriteLine(c.SerialNumber + ", ");
        }
        Console.WriteLine();
        string? number = Console.ReadLine();

        if (number != null) _ships[fromIndex].TransferContainer(_ships[toIndex], number);
    }

    public static void PrintShipInfo()
    {
        Console.WriteLine("Wybierz statek, którego dane chcesz poznać: ");
        int index = ShipIndex();
        
        _ships[index].ShipInfo();
    }

    private static int ShipIndex()
    {
        foreach (var ship in _ships)
        {
            Console.Write(ship.ShipNumber + ", ");
        }
        Console.WriteLine();
        string? shipNumber = Console.ReadLine();
        
        return _ships.FindIndex(c => c.ShipNumber.Equals(shipNumber));
    }
}