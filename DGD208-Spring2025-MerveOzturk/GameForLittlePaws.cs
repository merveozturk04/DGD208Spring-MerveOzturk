using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using LittlePawsGame;

public class Game
{
    private bool _isRunning;
    private string _selectedPet = "";
    private PetStatForLittlePaws _currentPetStats;
    private PetStatManager _petManager = new PetStatManager();
    private AdoptedPet? _currentPet = null;

    public async Task GameLoop()
    {
        Initialize();
        _isRunning = true;
        while (_isRunning)
        {
            string input = GetUserInput();
            await ProcessUserChoice(input);
        }
        Console.WriteLine("Thanks for playing Little Paws!");
    }

    private void Initialize()
    {
        Console.WriteLine("🐾 Welcome to Little Paws! 🐾");
        Console.WriteLine("Adopt a pet, take care of them, and enjoy your journey.");
        Console.WriteLine();
    }

    private string GetUserInput()
    {
        Console.WriteLine("Main Menu:");
        Console.WriteLine("1. Choose a Pet");
        Console.WriteLine("2. View Available Items");
        Console.WriteLine("3. View Pet Stats");
        Console.WriteLine("4. Exit");
        Console.Write("Enter your choice: ");
        return Console.ReadLine() ?? string.Empty;
    }

    private async Task ProcessUserChoice(string choice)
    {
        Console.WriteLine();
        switch (choice)
        {
            case "1":
                ChoosePet();
                break;
            case "2":
                DisplayItems();
                break;
            case "3":
                ShowPetStats();
                break;
            case "4":
                _isRunning = false;
                break;
            default:
                Console.WriteLine("Invalid input. Please try again.");
                break;
        }
        await Task.Delay(400);
        Console.WriteLine();
    }

    private void ChoosePet()
    {
        List<string> pets = new List<string>();
        foreach (var pet in Enum.GetValues(typeof(PetTypeForLittlePaws)))
        {
            string? petName = pet?.ToString();
            if (!string.IsNullOrEmpty(petName))
                pets.Add(petName);
        }
        Console.WriteLine("Available Pets:");
        for (int i = 0; i < pets.Count; i++)
            Console.WriteLine($"{i + 1}. {pets[i]}");

        Console.Write("Enter the number of your chosen pet: ");
        string? input = Console.ReadLine();
        if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int index) && index >= 1 && index <= pets.Count)
        {
            _selectedPet = pets[index - 1];
            _petManager.AdoptPet(_selectedPet, (PetTypeForLittlePaws)Enum.Parse(typeof(PetTypeForLittlePaws), _selectedPet));
            _currentPet = _petManager.GetAdoptedPets().Find(p => p.Name == _selectedPet);
            Console.WriteLine($"You adopted: {_selectedPet}");
        }
        else
        {
            Console.WriteLine("Invalid pet selection.");
        }
    }

    private void DisplayItems()
    {
        List<string> items = new List<string>();
        foreach (var item in Enum.GetValues(typeof(ItemTypeForLittlePaws)))
        {
            string? itemName = item?.ToString();
            if (!string.IsNullOrEmpty(itemName))
                items.Add(itemName);
        }
        Console.WriteLine("Available Items:");
        foreach (string item in items)
            Console.WriteLine($"- {item}");
    }

    private void ShowPetStats()
    {
        if (string.IsNullOrEmpty(_selectedPet))
        {
            Console.WriteLine("You haven't selected a pet yet.");
            return;
        }
        Console.WriteLine($"Stats for {_selectedPet}:");
        foreach (PetStatForLittlePaws stat in Enum.GetValues(typeof(PetStatForLittlePaws)))
            Console.WriteLine($"- {stat}");
    }
}
