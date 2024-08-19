// Project Objectives: create a C# console app that calculates the price of a housing project
// inputs: user enters the length, width, height, and material type of the house
// outputs: the program calculates the price of the house and displays it to the user

class Program
{
    static void Main(string[] args)
    {
        double width = GetValidDoubleInput("Enter the width of the house:");
        double length = GetValidDoubleInput("Enter the length of the house:");
        double height = GetValidDoubleInput("Enter the height of the house:");
        int materialType = GetValidIntInput("Enter the material type (1-5) of the house:", 1, 5);

        double floorArea = CalculateFloorArea(width, length);
        double wallArea = CalculateWallArea(width, height);
        double roofArea = CalculateRoofArea(floorArea);
        double basementArea = CalculateBasementArea(roofArea);
        double totalVolume = CalculateTotalVolume(floorArea, wallArea, roofArea, basementArea);
        double housePrice = CalculateHousePrice(totalVolume, materialType);

        Console.WriteLine($"The price of the house is: ${housePrice}");
        Console.ReadLine();
    }

    static double CalculateFloorArea(double width, double length)
    {
        return width * length * 2;
    }

    static double CalculateWallArea(double width, double height)
    {
        return width * height * 5;
    }

    static double CalculateRoofArea(double floorArea)
    {
        return floorArea * 4 / 5;
    }

    static double CalculateBasementArea(double roofArea)
    {
        return roofArea * 4 / 5;
    }

    static double CalculateTotalVolume(double floorArea, double wallArea, double roofArea, double basementArea)
    {
        return floorArea + wallArea + roofArea + basementArea;
    }

    static double CalculateHousePrice(double totalVolume, int materialType)
    {
        double materialPrice = materialType switch
        {
            1 => 100,
            2 => 150,
            3 => 200,
            4 => 250,
            5 => 300,
            _ => throw new ArgumentException("Invalid material type.")
        };

        return totalVolume * materialPrice;
    }

    static double GetValidDoubleInput(string prompt)
    {
        double input;
        Console.WriteLine(prompt);
        while (!double.TryParse(Console.ReadLine(), out input) || input <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a positive number:");
        }
        return input;
    }

    static int GetValidIntInput(string prompt, int minValue, int maxValue)
    {
        int input;
        Console.WriteLine(prompt);
        while (!int.TryParse(Console.ReadLine(), out input) || input < minValue || input > maxValue)
        {
            Console.WriteLine($"Invalid input. Please enter an integer between {minValue} and {maxValue}:");
        }
        return input;
    }
}
