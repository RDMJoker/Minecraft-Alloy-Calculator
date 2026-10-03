namespace Minecraft_Alloy_Calculator;

class Program
{
    static readonly List<Alloy> alloys = [];
    static int amount;
    static Alloy? alloyToCreate;

    static void Main()
    {
        PrintStartingMessage();
        //Create all alloys
        CreateAlloys();
        ConsoleWriteColorLine("All done... Starting program...", ConsoleColor.White);
        Thread.Sleep(700);
        while (true)
        {
            Console.Clear();
            PrintChoiceInfo();
            alloyToCreate = GetAlloyToCreateInput();
            if(alloyToCreate == null) continue;
            amount = GetAmountToCreateInput(alloyToCreate);
            Console.Clear();
            Console.WriteLine($"So you want to create {amount}mB of {alloyToCreate.Name}");
            var resolution = AlloyMath.ResolveAlloy(alloyToCreate, amount);
            if (resolution == null)
            {
                ConsoleWriteColorLine($"No valid recipe could be found for {amount}mB of {alloyToCreate.Name}.", ConsoleColor.Red);
                Console.ReadLine();
                continue;
            }
            PrintResolution(resolution);
            Console.WriteLine();
            Console.ReadLine();
        }
        
    }
    
    static void PrintResolution(AlloyResolution _resolution)
    {
        Console.WriteLine();
        Console.WriteLine($"Recipe for {_resolution.ActualAmount}mB of {_resolution.Alloy.Name}:");
        PrintResolutionTree(_resolution, 0);
        Console.WriteLine();
        Console.WriteLine("Raw materials required:");
        foreach (var material in _resolution.RawMaterials)
            Console.WriteLine($"{material.Key}: {material.Value}mB");
        if (_resolution.ActualAmount == _resolution.RequestedAmount) return;
        int additionalAmount = _resolution.ActualAmount - _resolution.RequestedAmount;
        ConsoleWriteColorLine($"Warning! No exact combination was found. The recipe produces {additionalAmount}mB more than requested.", ConsoleColor.Yellow);
    }
    
    static void PrintResolutionTree(AlloyResolution _resolution, int _depth)
    {
        string indent = new(' ', _depth * 4);
        foreach (var ingredient in _resolution.Ingredients)
        {
            ConsoleWriteColorLine($"{indent}{ingredient.Value.ActualAmount}mB {ingredient.Key.Name}", _depth % 2 == 0 ? ConsoleColor.Blue : ConsoleColor.DarkGreen);
            if (ingredient.Key.AlloyInformation.Count > 0)
                PrintResolutionTree(ingredient.Value, _depth + 1);
        }
    }

    static int GetAmountToCreateInput(Alloy _alloyToCreate)
    {
        Console.Clear();
        Console.WriteLine("Type the amount of mBs you want to create of " + _alloyToCreate.Name + "!");
        Console.WriteLine("It is possible to input any Number, but for best results it should be divisible by 144!");
        string input = Console.ReadLine()?.Trim() ?? throw new InvalidOperationException();
        if (!int.TryParse(input, out int inputAmount)) throw new NotImplementedException();
        return inputAmount;
    }

    static Alloy? GetAlloyToCreateInput()
    {
        Console.WriteLine("Type the corresponding number and confirm with ENTER");
        string? input = Console.ReadLine()?.Trim();
        if (input is { Length: 0 }) return null;
        if (!int.TryParse(input, out int enumIndex)) return null;
        if (!Enum.IsDefined(typeof(EAlloyName), enumIndex)) return null;
        var chosenIngot = (EAlloyName)enumIndex;
        var foundAlloy = alloys.FirstOrDefault(_alloy => _alloy.Name == chosenIngot);
        return foundAlloy ?? throw new NotImplementedException();
    }
    
    static void CreateAlloys()
    {
        alloys.Add(new BlackBronze());
        alloys.Add(new BismuthBronze());
        alloys.Add(new Bronze());
        alloys.Add(new BlackSteel());
        alloys.Add(new BlueSteel());
        alloys.Add(new RedSteel());
        alloys.Add(new Brass());
        alloys.Add(new RoseGold());
        alloys.Add(new TinAlloy());
        alloys.Add(new SterlingSilver());
        alloys.Add(new Copper());
        alloys.Add(new Gold());
        alloys.Add(new Nickel());
        alloys.Add(new Steel());
        alloys.Add(new Zinc());
        alloys.Add(new Tin());
        alloys.Add(new Bismuth());
        alloys.Add(new WeakBlueSteelIngot());
        alloys.Add(new WeakRedSteelIngot());
        alloys.Add(new WeakSteelIngot());
        alloys.Add(new Iron());
        alloys.Add(new PigIronIngot());
        alloys.Add(new Silver());
    }

    static void PrintChoiceInfo()
    {
        int i = 0;
        foreach (string ingot in Enum.GetNames(typeof(EAlloyName)))
        {
            if (i == 10) break;
            Console.WriteLine(i + " -- " + ingot);
            i++;
        }
    }

    static void ConsoleWriteColor(string _output, ConsoleColor _color)
    {
        ConsoleColor currentColor = Console.ForegroundColor;
        Console.ForegroundColor = _color;
        Console.Write(_output);
        Console.ForegroundColor = currentColor;
    }

    // Output Color with line break
    static void ConsoleWriteColorLine(string _output, ConsoleColor _color)
    {
        ConsoleWriteColor(_output + "\n", _color);
    }

    // Output Color with char input
    static void ConsoleWriteColorChar(char _output, ConsoleColor _color)
    {
        ConsoleColor currentColor = Console.ForegroundColor;
        Console.ForegroundColor = _color;
        Console.Write(_output);
        Console.ForegroundColor = currentColor;
    }

    static void PrintStartingMessage()
    {
        ConsoleWriteColorChar('[', ConsoleColor.White);
        ConsoleWriteColorChar('S', ConsoleColor.Red);
        ConsoleWriteColorChar('T', ConsoleColor.Yellow);
        ConsoleWriteColorChar('A', ConsoleColor.Green);
        ConsoleWriteColorChar('R', ConsoleColor.Blue);
        ConsoleWriteColorChar('T', ConsoleColor.Magenta);
        ConsoleWriteColorChar(']', ConsoleColor.White);
        ConsoleWriteColorLine("Welcome to the alloy calculator for Minecraft-TerraFirmaGreg!", ConsoleColor.White);
        ConsoleWriteColorLine("WARNING: This program is early in development. Please report any issues you find!", ConsoleColor.Yellow);
        ConsoleWriteColorLine("Creating alloy data...", ConsoleColor.Green);
    }
}