using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LittlePawsGame;

public class PetStats
{
    public Dictionary<PetStatForLittlePaws, int> Stats { get; private set; }

    public PetStats()
    {
        Stats = new Dictionary<PetStatForLittlePaws, int>();
        foreach (PetStatForLittlePaws stat in Enum.GetValues(typeof(PetStatForLittlePaws)))
        {
            Stats[stat] = 50;
        }
    }

    public void DecreaseAll(int amount)
    {
        foreach (var stat in Stats.Keys)
        {
            Stats[stat] = Math.Max(0, Stats[stat] - amount);
        }
    }

    public bool HasAnyZero()
    {
        foreach (var value in Stats.Values)
        {
            if (value == 0)
                return true;
        }
        return false;
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

public class PetCommandEventArgs : EventArgs
{
    public AdoptedPet Pet { get; }
    public string Command { get; }

    public PetCommandEventArgs(AdoptedPet pet, string command)
    {
        Pet = pet;
        Command = command;
    }
}

public class PetStatManager
{
    private List<AdoptedPet> adoptedPets = new List<AdoptedPet>();
    private CancellationTokenSource cts = new CancellationTokenSource();
    private int decreaseAmount = 1;
    private int intervalSeconds = 3;

    public event EventHandler<PetStatusChangedEventArgs>? PetStatusChanged;
    public event EventHandler<PetCommandEventArgs>? PetCommandIssued;

    public void AdoptPet(string name, PetTypeForLittlePaws type)
    {
        var pet = new AdoptedPet { Name = name, Type = type };
        adoptedPets.Add(pet);
        PetStatusChanged?.Invoke(this, new PetStatusChangedEventArgs(pet, "Adopted"));
    }

    public void IssueCommand(AdoptedPet pet, string command)
    {
        PetCommandIssued?.Invoke(this, new PetCommandEventArgs(pet, command));
    }

    public void StartStatDecreaseLoop()
    {
        _ = ContinuePetConditionUpdatesAsync(cts.Token);
    }

    public async Task ContinuePetConditionUpdatesAsync(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            for (int i = adoptedPets.Count - 1; i >= 0; i--)
            {
                var pet = adoptedPets[i];
                pet.Stats.DecreaseAll(decreaseAmount);
                if (pet.Stats.HasAnyZero())
                {
                    PetStatusChanged?.Invoke(this, new PetStatusChangedEventArgs(pet, $"Your pet {pet.Name} the {pet.Type} has died due to a stat reaching zero."));
                    adoptedPets.RemoveAt(i);
                }
                else
                {
                    PetStatusChanged?.Invoke(this, new PetStatusChangedEventArgs(pet, $"Stats updated for {pet.Name}."));
                }
            }
            await Task.Delay(intervalSeconds * 1000, cancellationToken);
        }
    }

    public async Task<bool> UseItemOnPetAsync(AdoptedPet pet, PetStatForLittlePaws stat, int amount, CancellationToken cancellationToken = default)
    {
        if (!adoptedPets.Contains(pet))
            return false;

        if (!pet.Stats.Stats.ContainsKey(stat))
            return false;

        // Visual feedback: notify start of item usage
        PetStatusChanged?.Invoke(this, new PetStatusChangedEventArgs(pet, $"Using item on {pet.Name} to increase {stat}..."));

        // Simulate item usage delay for user feedback
        await Task.Delay(800, cancellationToken);

        pet.Stats.Stats[stat] = Math.Min(100, pet.Stats.Stats[stat] + amount);

        // Visual feedback: notify completion of item usage
        PetStatusChanged?.Invoke(this, new PetStatusChangedEventArgs(pet, $"Used item on {pet.Name}, {stat} increased by {amount}."));

        return true;
    }

    public void StopStatDecreaseLoop()
    {
        cts.Cancel();
    }

    public List<AdoptedPet> GetAdoptedPets() => adoptedPets;
}

