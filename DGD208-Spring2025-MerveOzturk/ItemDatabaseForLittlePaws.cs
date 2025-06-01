using LittlePawsGame;
using System.Collections.Generic;
using System.Linq;

public class Item
{
    public required string Name { get; set; }
    public ItemTypeForLittlePaws Type { get; set; }
    public required List<PetTypeForLittlePaws> CompatibleWith { get; set; }
    public PetStatForLittlePaws AffectedStat { get; set; }
    public int EffectAmount { get; set; }
    public float Duration { get; set; }
}

public static class ItemDatabase
{
    public static List<Item> AllItems = new List<Item>
    {
        // Food items for different pets
        new Item {
            Name = "Premium Pet Food",
            Type = ItemTypeForLittlePaws.Food,
            CompatibleWith = new List<PetTypeForLittlePaws> {
                PetTypeForLittlePaws.Raccoon, PetTypeForLittlePaws.Rabbit,
                PetTypeForLittlePaws.Hedgehog, PetTypeForLittlePaws.Ferret
            },
            AffectedStat = PetStatForLittlePaws.Hunger,
            EffectAmount = 25,
            Duration = 2.0f
        },

        new Item {
            Name = "Fresh Fish",
            Type = ItemTypeForLittlePaws.Food,
            CompatibleWith = new List<PetTypeForLittlePaws> { PetTypeForLittlePaws.Otter },
            AffectedStat = PetStatForLittlePaws.Hunger,
            EffectAmount = 30,
            Duration = 2.5f
        },

        new Item {
            Name = "Tropical Seeds",
            Type = ItemTypeForLittlePaws.Food,
            CompatibleWith = new List<PetTypeForLittlePaws> { PetTypeForLittlePaws.Parrot },
            AffectedStat = PetStatForLittlePaws.Hunger,
            EffectAmount = 20,
            Duration = 1.5f
        },

        // Toys for fun/whimsy
        new Item {
            Name = "Interactive Toy",
            Type = ItemTypeForLittlePaws.Toy,
            CompatibleWith = new List<PetTypeForLittlePaws> {
                PetTypeForLittlePaws.Raccoon, PetTypeForLittlePaws.Rabbit,
                PetTypeForLittlePaws.Ferret
            },
            AffectedStat = PetStatForLittlePaws.Whimsy,
            EffectAmount = 30,
            Duration = 3.0f
        },

        new Item {
            Name = "Water Toy",
            Type = ItemTypeForLittlePaws.Toy,
            CompatibleWith = new List<PetTypeForLittlePaws> { PetTypeForLittlePaws.Otter },
            AffectedStat = PetStatForLittlePaws.Whimsy,
            EffectAmount = 35,
            Duration = 4.0f
        },

        new Item {
            Name = "Mirror Toy",
            Type = ItemTypeForLittlePaws.Toy,
            CompatibleWith = new List<PetTypeForLittlePaws> { PetTypeForLittlePaws.Parrot },
            AffectedStat = PetStatForLittlePaws.Whimsy,
            EffectAmount = 25,
            Duration = 2.0f
        },

        // Bedding for sleep/snuggle
        new Item {
            Name = "Cozy Blanket",
            Type = ItemTypeForLittlePaws.Blanket,
            CompatibleWith = new List<PetTypeForLittlePaws> {
                PetTypeForLittlePaws.Raccoon, PetTypeForLittlePaws.Rabbit,
                PetTypeForLittlePaws.Hedgehog, PetTypeForLittlePaws.Ferret
            },
            AffectedStat = PetStatForLittlePaws.SnuggleFactor,
            EffectAmount = 30,
            Duration = 3.5f
        },

        new Item {
            Name = "Luxury Pet Bed",
            Type = ItemTypeForLittlePaws.Bed,
            CompatibleWith = new List<PetTypeForLittlePaws> {
                PetTypeForLittlePaws.Raccoon, PetTypeForLittlePaws.Rabbit,
                PetTypeForLittlePaws.Hedgehog, PetTypeForLittlePaws.Ferret,
                PetTypeForLittlePaws.Otter
            },
            AffectedStat = PetStatForLittlePaws.SnuggleFactor,
            EffectAmount = 40,
            Duration = 4.0f
        },

        // Special items
        new Item {
            Name = "Health Supplement",
            Type = ItemTypeForLittlePaws.Medicine,
            CompatibleWith = new List<PetTypeForLittlePaws> {
                PetTypeForLittlePaws.Raccoon, PetTypeForLittlePaws.Rabbit,
                PetTypeForLittlePaws.Parrot, PetTypeForLittlePaws.Otter,
                PetTypeForLittlePaws.Hedgehog, PetTypeForLittlePaws.Ferret
            },
            AffectedStat = PetStatForLittlePaws.Trust,
            EffectAmount = 35,
            Duration = 1.0f
        },

        new Item {
            Name = "Grooming Kit",
            Type = ItemTypeForLittlePaws.GroomingKit,
            CompatibleWith = new List<PetTypeForLittlePaws> {
                PetTypeForLittlePaws.Raccoon, PetTypeForLittlePaws.Rabbit,
                PetTypeForLittlePaws.Parrot, PetTypeForLittlePaws.Hedgehog,
                PetTypeForLittlePaws.Ferret
            },
            AffectedStat = PetStatForLittlePaws.Fluffiness,
            EffectAmount = 25,
            Duration = 2.5f
        }
    };
}