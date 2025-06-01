using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using LittlePawsGame;
using LittlePaws;

public class Game
{
    private bool _isRunning;
    private PetStatManager _petManager = new PetStatManager();
    private AdoptedPet? _currentPet = null;

    public async Task GameLoop()
    {
        Initialize();
        _petManager.StartStatDecreaseLoop();
        _petManager.PetStatusChanged += OnPetStatusChanged;

        _isRunning = true;
        while (_isRunning)
        {
            string input = GetUserInput();
            await ProcessUserChoice(input);
        }

        _petManager.StopStatDecreaseLoop();
        Console.WriteLine("Thanks for playing Little Paws!");
    }

    private void OnPetStatusChanged(object? sender, PetStatusChangedEventArgs e)
    {
        // Visual feedback for pet status changes
        if (e.StatusMessage.Contains("died"))
        {
            Console.WriteLine($"\n[ALERT] {e.StatusMessage}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

    private void Initialize()
    {
        Console.Clear();
        Console.WriteLine("Welcome to Little Paws - Pet Simulator");
        Console.WriteLine("=====================================");
        Console.WriteLine("Created by: Merve Ozturk (2305041043)");
        Console.WriteLine("\nPress any key to start...");
        Console.ReadKey();
    }

    private string GetUserInput()
    {
        Console.Clear();
        Console.WriteLine("Little Paws - Main Menu");
        Console.WriteLine("======================");
        Console.WriteLine("1. Adopt a Pet");
        Console.WriteLine("2. View Pet Stats");
        Console.WriteLine("3. Use Item on Pet");
        Console.WriteLine("4. View Available Items");
        Console.WriteLine("5. Show Creator Info");
        Console.WriteLine("6. Exit");
        Console.WriteLine();

        if (_currentPet != null)
        {
            Console.WriteLine($"Active Pet: {_currentPet.Name} the {_currentPet.Type}");
        }

        Console.Write("\nEnter your choice: ");
        return Console.ReadLine() ?? string.Empty;
    }

    private async Task ProcessUserChoice(string choice)
    {
        switch (choice)
        {
            case "1":
                AdoptPet();
                break;
            case "2":
                ShowPetStats();
                break;
            case "3":
                await UseItemOnPet();
                break;
            case "4":
                DisplayAvailableItems();
                break;
            case "5":
                ShowCreatorInfo();
                break;
            case "6":
                _isRunning = false;
                return;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }

        if (_isRunning)
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    private void AdoptPet()
    {
        var petTypes = Enum.GetValues(typeof(PetTypeForLittlePaws))
            .Cast<PetTypeForLittlePaws>().ToList();

        var menu = new MenuForLittlePaws<PetTypeForLittlePaws>(
            "Choose Your Pet Type",
            petTypes,
            pet => $"{pet} - A wonderful companion!"
        );

        var selectedType = menu.ShowAndGetSelection();
        if (!selectedType.Equals(default(PetTypeForLittlePaws)))
        {
            Console.Write($"Enter a name for your {selectedType}: ");
            string petName = Console.ReadLine() ?? selectedType.ToString();

            _petManager.AdoptPet(petName, selectedType);
            _currentPet = _petManager.GetAdoptedPets().Find(p => p.Name == petName);

            Console.WriteLine($"Congratulations! You adopted {petName} the {selectedType}!");
        }
    }

    private void ShowPetStats()
    {
        var adoptedPets = _petManager.GetAdoptedPets();

        if (!adoptedPets.Any())
        {
            Console.WriteLine("You haven't adopted any pets yet!");
            return;
        }

        var menu = new MenuForLittlePaws<AdoptedPet>(
            "Your Pets",
            adoptedPets,
            pet => $"{pet.Name} ({pet.Type}) - Health: {GetHealthStatus(pet)}"
        );

        var selectedPet = menu.ShowAndGetSelection();
        if (selectedPet != null)
        {
            DisplayDetailedStats(selectedPet);
        }
    }

    private void DisplayDetailedStats(AdoptedPet pet)
    {
        Console.Clear();
        Console.WriteLine($"Stats for {pet.Name} the {pet.Type}");
        Console.WriteLine("===============================");

        foreach (var stat in pet.Stats.Stats)
        {
            string bar = GetStatusBar(stat.Value);
            Console.WriteLine($"{stat.Key,-12}: {stat.Value,3}/100 {bar}");
        }

        Console.WriteLine($"\nOverall Health: {GetHealthStatus(pet)}");
        Console.WriteLine("\nPress any key to return...");
        Console.ReadKey();
    }

    private string GetStatusBar(int value)
    {
        int filled = value / 10;
        string bar = new string('#', filled) + new string('-', 10 - filled);
        return $"[{bar}]";
    }

    private string GetHealthStatus(AdoptedPet pet)
    {
        double avg = pet.Stats.Stats.Values.Average();
        return avg switch
        {
            >= 80 => "Excellent",
            >= 60 => "Good",
            >= 40 => "Fair",
            >= 20 => "Poor",
            _ => "Critical"
        };
    }

    private async Task UseItemOnPet()
    {
        var pets = _petManager.GetAdoptedPets();
        if (!pets.Any())
        {
            Console.WriteLine("You need to adopt a pet first!");
            return;
        }

        // Select pet using LINQ
        var petMenu = new MenuForLittlePaws<AdoptedPet>(
            "Select Pet",
            pets,
            pet => $"{pet.Name} ({pet.Type})"
        );

        var selectedPet = petMenu.ShowAndGetSelection();
        if (selectedPet == null) return;

        // Filter compatible items using LINQ
        var compatibleItems = ItemDatabase.AllItems
            .Where(item => item.CompatibleWith.Contains(selectedPet.Type))
            .ToList();

        if (!compatibleItems.Any())
        {
            Console.WriteLine($"No items available for {selectedPet.Type}!");
            return;
        }

        var itemMenu = new MenuForLittlePaws<Item>(
            $"Items for {selectedPet.Name}",
            compatibleItems,
            item => $"{item.Name} - +{item.EffectAmount} {item.AffectedStat}"
        );

        var selectedItem = itemMenu.ShowAndGetSelection();
        if (selectedItem == null) return;

        // Use async method for item usage
        Console.WriteLine($"Using {selectedItem.Name} on {selectedPet.Name}...");
        bool success = await _petManager.UseItemOnPetAsync(
            selectedPet,
            selectedItem.AffectedStat,
            selectedItem.EffectAmount
        );

        if (success)
        {
            Console.WriteLine($"Successfully used {selectedItem.Name}!");
            Console.WriteLine($"{selectedItem.AffectedStat} increased by {selectedItem.EffectAmount}!");
        }
    }

    private void DisplayAvailableItems()
    {
        if (_currentPet == null)
        {
            Console.WriteLine("Adopt a pet first to see compatible items!");
            return;
        }

        var items = ItemDatabase.AllItems
            .Where(item => item.CompatibleWith.Contains(_currentPet.Type))
            .GroupBy(item => item.Type)
            .ToList();

        Console.WriteLine($"Items available for {_currentPet.Name} ({_currentPet.Type}):");
        Console.WriteLine("================================================");

        foreach (var group in items)
        {
            Console.WriteLine($"\n{group.Key}:");
            foreach (var item in group)
            {
                Console.WriteLine($"  - {item.Name}: +{item.EffectAmount} {item.AffectedStat}");
            }
        }
    }

    private void ShowCreatorInfo()
    {
        Console.Clear();
        Console.WriteLine("Project Information");
        Console.WriteLine("==================");
        Console.WriteLine("Project: Little Paws Pet Simulator");
        Console.WriteLine("Course: DGD208 - Game Programming 2");
        Console.WriteLine("Creator: Merve Ozturk");
        Console.WriteLine("Student Number: 2305041043");
        Console.WriteLine("Semester: Spring 2025");
        Console.WriteLine("\nPress any key to return...");
        Console.ReadKey();
    }
}