using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using LittlePawsGame;

public class PetStats
{
    public Dictionary<PetStatForLittlePaws, int> Stats { get; private set; }

    public PetStats()
    {
        Stats = new Dictionary<PetStatForLittlePaws, int>();

        // Initialize only the core stats as per requirements: Hunger, Sleep, Fun
        Stats[PetStatForLittlePaws.Hunger] = 50;
        Stats[PetStatForLittlePaws.SnuggleFactor] = 50; // Sleep equivalent
        Stats[PetStatForLittlePaws.Whimsy] = 50; // Fun equivalent

        // Initialize additional creative stats
        foreach (PetStatForLittlePaws stat in Enum.GetValues(typeof(PetStatForLittlePaws)))
        {
            if (!Stats.ContainsKey(stat))
                Stats[stat] = 50;
        }
    }

    public void DecreaseAll(int amount)
    {
        var keys = Stats.Keys.ToList();
        foreach (var stat in keys)
        {
            Stats[stat] = Math.Max(0, Stats[stat] - amount);
        }
    }

    public bool HasCriticalStat()
    {
        return Stats.Values.Any(value => value == 0);
    }
}

public class AdoptedPet
{
    public required string Name { get; set; }
    public PetTypeForLittlePaws Type { get; set; }
    public PetStats Stats { get; set; } = new PetStats();
}

public class PetStatusChangedEventArgs : EventArgs
{
    public AdoptedPet Pet { get; }
    public string StatusMessage { get; }

    public PetStatusChangedEventArgs(AdoptedPet pet, string statusMessage)
    {
        Pet = pet;
        StatusMessage = statusMessage;
    }
}

public class PetStatManager
{
    private List<AdoptedPet> adoptedPets = new List<AdoptedPet>();
    private CancellationTokenSource cts = new CancellationTokenSource();
    private readonly int decreaseAmount = 1;
    private readonly int intervalSeconds = 5; // Decrease every 5 seconds

    // Event for pet status changes
    public event EventHandler<PetStatusChangedEventArgs>? PetStatusChanged;

    public void AdoptPet(string name, PetTypeForLittlePaws type)
    {
        var pet = new AdoptedPet { Name = name, Type = type };
        adoptedPets.Add(pet);

        // Trigger event
        PetStatusChanged?.Invoke(this, new PetStatusChangedEventArgs(pet, $"{name} has been adopted!"));
    }

    public void StartStatDecreaseLoop()
    {
        _ = Task.Run(() => ContinuousStatDecreaseAsync(cts.Token));
    }

    private async Task ContinuousStatDecreaseAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(intervalSeconds * 1000, cancellationToken);

                for (int i = adoptedPets.Count - 1; i >= 0; i--)
                {
                    var pet = adoptedPets[i];
                    pet.Stats.DecreaseAll(decreaseAmount);

                    if (pet.Stats.HasCriticalStat())
                    {
                        // Pet dies - remove from list and notify
                        PetStatusChanged?.Invoke(this, new PetStatusChangedEventArgs(
                            pet, $"Your pet {pet.Name} the {pet.Type} has died due to neglect."));
                        adoptedPets.RemoveAt(i);
                    }
                }
            }
            catch (TaskCanceledException)
            {
                break;
            }
        }
    }

    public async Task<bool> UseItemOnPetAsync(AdoptedPet pet, PetStatForLittlePaws stat, int amount)
    {
        if (!adoptedPets.Contains(pet) || !pet.Stats.Stats.ContainsKey(stat))
            return false;

        // Simulate item usage time
        await Task.Delay(1000);

        // Apply item effect
        pet.Stats.Stats[stat] = Math.Min(100, pet.Stats.Stats[stat] + amount);

        // Trigger event
        PetStatusChanged?.Invoke(this, new PetStatusChangedEventArgs(
            pet, $"Used item on {pet.Name}: {stat} increased by {amount}"));

        return true;
    }

    public void StopStatDecreaseLoop()
    {
        cts.Cancel();
    }

    public List<AdoptedPet> GetAdoptedPets() => new List<AdoptedPet>(adoptedPets);
}
