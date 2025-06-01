using LittlePawsGame;

using System.Collections.Generic;

public class Item
{
    public required string Name { get; set; }
    public ItemTypeForLittlePaws Type { get; set; }
    public required List<PetTypeForLittlePaws> CompatibleWith { get; set; }
    public PetStatForLittlePaws AffectedStat { get; set; }
    public int EffectAmount { get; set; }
    public float Duration { get; set; }

    // Returns all items compatible with a specific pet type
    public static IEnumerable<Item> GetItemsForPet(PetTypeForLittlePaws petType)
    {
        return ItemDatabase.AllItems.Where(item => item.CompatibleWith.Contains(petType));
    }

    // Returns all items of a specific type (e.g., Food, Toy) for a pet type
    public static IEnumerable<Item> GetItemsForPetAndType(PetTypeForLittlePaws petType, ItemTypeForLittlePaws itemType)
    {
        return ItemDatabase.AllItems
            .Where(item => item.Type == itemType && item.CompatibleWith.Contains(petType));
    }

    // Returns all items that affect a specific stat for a pet type
    public static IEnumerable<Item> GetItemsForPetAndStat(PetTypeForLittlePaws petType, PetStatForLittlePaws stat)
    {
        return ItemDatabase.AllItems
            .Where(item => item.AffectedStat == stat && item.CompatibleWith.Contains(petType));
    }

    // Returns the best item (highest effect) for a pet type and stat
    public static Item? GetBestItemForPetAndStat(PetTypeForLittlePaws petType, PetStatForLittlePaws stat)
    {
        return ItemDatabase.AllItems
            .Where(item => item.AffectedStat == stat && item.CompatibleWith.Contains(petType))
            .OrderByDescending(item => item.EffectAmount)
            .FirstOrDefault();
    }
}

public static class ItemDatabase
{
    public static List<Item> AllItems = new List<Item>
    {
        // Foods for Raccoon
        new Item {
            Name = "Sweet Berries",
            Type = ItemTypeForLittlePaws.Food,
            CompatibleWith = new List<PetTypeForLittlePaws> { PetTypeForLittlePaws.Raccoon },
            AffectedStat = PetStatForLittlePaws.Hunger,
            EffectAmount = 20,
            Duration = 2.0f
        },
        new Item {
            Name = "Fish Scraps",
            Type = ItemTypeForLittlePaws.Food,
            CompatibleWith = new List<PetTypeForLittlePaws> { PetTypeForLittlePaws.Raccoon },
            AffectedStat = PetStatForLittlePaws.Hunger,
            EffectAmount = 25,
            Duration = 3.0f
        },

        // Foods for Rabbit
        new Item {
            Name = "Fresh Carrots",
            Type = ItemTypeForLittlePaws.Food,
            CompatibleWith = new List<PetTypeForLittlePaws> { PetTypeForLittlePaws.Rabbit },
            AffectedStat = PetStatForLittlePaws.Hunger,
            EffectAmount = 20,
            Duration = 3.5f
        },
        new Item {
            Name = "Leafy Greens",
            Type = ItemTypeForLittlePaws.Food,
            CompatibleWith = new List<PetTypeForLittlePaws> { PetTypeForLittlePaws.Rabbit },
            AffectedStat = PetStatForLittlePaws.Hunger,
            EffectAmount = 25,
            Duration = 4.0f
        },

        // Foods for Parrot
        new Item {
            Name = "Sunflower Seeds",
            Type = ItemTypeForLittlePaws.Food,
            CompatibleWith = new List<PetTypeForLittlePaws> { PetTypeForLittlePaws.Parrot },
            AffectedStat = PetStatForLittlePaws.Hunger,
            EffectAmount = 15,
            Duration = 1.5f
        },
        new Item {
            Name = "Tropical Fruit Mix",
            Type = ItemTypeForLittlePaws.Food,
            CompatibleWith = new List<PetTypeForLittlePaws> { PetTypeForLittlePaws.Parrot },
            AffectedStat = PetStatForLittlePaws.Hunger,
            EffectAmount = 30,
            Duration = 2.5f
        },

        // Foods for Otter
        new Item {
            Name = "Fresh Fish",
            Type = ItemTypeForLittlePaws.Food,
            CompatibleWith = new List<PetTypeForLittlePaws> { PetTypeForLittlePaws.Otter },
            AffectedStat = PetStatForLittlePaws.Hunger,
            EffectAmount = 30,
            Duration = 2.0f
        },
        new Item {
            Name = "Shellfish Treats",
            Type = ItemTypeForLittlePaws.Food,
            CompatibleWith = new List<PetTypeForLittlePaws> { PetTypeForLittlePaws.Otter },
            AffectedStat = PetStatForLittlePaws.Hunger,
            EffectAmount = 25,
            Duration = 3.0f
        },

        // Foods for Hedgehog
        new Item {
            Name = "Insect Mix",
            Type = ItemTypeForLittlePaws.Food,
            CompatibleWith = new List<PetTypeForLittlePaws> { PetTypeForLittlePaws.Hedgehog },
            AffectedStat = PetStatForLittlePaws.Hunger,
            EffectAmount = 20,
            Duration = 2.0f
        },
        new Item {
            Name = "Hedgehog Kibble",
            Type = ItemTypeForLittlePaws.Food,
            CompatibleWith = new List<PetTypeForLittlePaws> { PetTypeForLittlePaws.Hedgehog },
            AffectedStat = PetStatForLittlePaws.Hunger,
            EffectAmount = 25,
            Duration = 2.5f
        },

        // Universal Foods
        new Item {
            Name = "Vitamin Treats",
            Type = ItemTypeForLittlePaws.Treat,
            CompatibleWith = new List<PetTypeForLittlePaws> {
                PetTypeForLittlePaws.Raccoon, PetTypeForLittlePaws.Rabbit,
                PetTypeForLittlePaws.Parrot, PetTypeForLittlePaws.Otter, PetTypeForLittlePaws.Hedgehog
            },
            AffectedStat = PetStatForLittlePaws.Hunger,
            EffectAmount = 15,
            Duration = 1.0f
        },

        // Toys for Fun/Whimsy
        new Item {
            Name = "Shiny Ball",
            Type = ItemTypeForLittlePaws.Toy,
            CompatibleWith = new List<PetTypeForLittlePaws> { PetTypeForLittlePaws.Raccoon },
            AffectedStat = PetStatForLittlePaws.Whimsy,
            EffectAmount = 25,
            Duration = 3.0f
        },
        new Item {
            Name = "Puzzle Box",
            Type = ItemTypeForLittlePaws.Puzzle,
            CompatibleWith = new List<PetTypeForLittlePaws> { PetTypeForLittlePaws.Raccoon },
            AffectedStat = PetStatForLittlePaws.Curiosity,
            EffectAmount = 30,
            Duration = 5.0f
        },

        new Item {
            Name = "Bunny Tunnel",
            Type = ItemTypeForLittlePaws.PlayTunnel,
            CompatibleWith = new List<PetTypeForLittlePaws> { PetTypeForLittlePaws.Rabbit },
            AffectedStat = PetStatForLittlePaws.Zoomies,
            EffectAmount = 35,
            Duration = 4.0f
        },
        new Item {
            Name = "Carrot Chew Stick",
            Type = ItemTypeForLittlePaws.ChewStick,
            CompatibleWith = new List<PetTypeForLittlePaws> { PetTypeForLittlePaws.Rabbit },
            AffectedStat = PetStatForLittlePaws.Whimsy,
            EffectAmount = 20,
            Duration = 3.5f
        },

        new Item {
            Name = "Colorful Perch",
            Type = ItemTypeForLittlePaws.Toy,
            CompatibleWith = new List<PetTypeForLittlePaws> { PetTypeForLittlePaws.Parrot },
            AffectedStat = PetStatForLittlePaws.Sassiness,
            EffectAmount = 25,
            Duration = 2.0f
        },
        new Item {
            Name = "Mirror Toy",
            Type = ItemTypeForLittlePaws.Toy,
            CompatibleWith = new List<PetTypeForLittlePaws> { PetTypeForLittlePaws.Parrot },
            AffectedStat = PetStatForLittlePaws.SocialMeter,
            EffectAmount = 30,
            Duration = 3.0f
        },

        new Item {
            Name = "Water Slide",
            Type = ItemTypeForLittlePaws.Toy,
            CompatibleWith = new List<PetTypeForLittlePaws> { PetTypeForLittlePaws.Otter },
            AffectedStat = PetStatForLittlePaws.Zoomies,
            EffectAmount = 40,
            Duration = 4.5f
        },
        new Item {
            Name = "Floating Ball",
            Type = ItemTypeForLittlePaws.Toy,
            CompatibleWith = new List<PetTypeForLittlePaws> { PetTypeForLittlePaws.Otter },
            AffectedStat = PetStatForLittlePaws.Whimsy,
            EffectAmount = 25,
            Duration = 3.0f
        },

        new Item {
            Name = "Tiny Exercise Wheel",
            Type = ItemTypeForLittlePaws.Toy,
            CompatibleWith = new List<PetTypeForLittlePaws> { PetTypeForLittlePaws.Hedgehog },
            AffectedStat = PetStatForLittlePaws.Zoomies,
            EffectAmount = 30,
            Duration = 3.5f
        },
        new Item {
            Name = "Hedgehog Hide",
            Type = ItemTypeForLittlePaws.Habitat,
            CompatibleWith = new List<PetTypeForLittlePaws> { PetTypeForLittlePaws.Hedgehog },
            AffectedStat = PetStatForLittlePaws.Sneakiness,
            EffectAmount = 35,
            Duration = 2.0f
        },

        // Blankets for Snuggle Factor
        new Item {
            Name = "Soft Fleece Blanket",
            Type = ItemTypeForLittlePaws.Blanket,
            CompatibleWith = new List<PetTypeForLittlePaws> {
                PetTypeForLittlePaws.Raccoon, PetTypeForLittlePaws.Rabbit, PetTypeForLittlePaws.Hedgehog
            },
            AffectedStat = PetStatForLittlePaws.SnuggleFactor,
            EffectAmount = 30,
            Duration = 5.0f
        },
        new Item {
            Name = "Heated Blanket",
            Type = ItemTypeForLittlePaws.Blanket,
            CompatibleWith = new List<PetTypeForLittlePaws> {
                PetTypeForLittlePaws.Raccoon, PetTypeForLittlePaws.Rabbit,
                PetTypeForLittlePaws.Parrot, PetTypeForLittlePaws.Hedgehog
            },
            AffectedStat = PetStatForLittlePaws.SnuggleFactor,
            EffectAmount = 35,
            Duration = 6.0f
        },

        // Beds
        new Item {
            Name = "Cozy Pet Bed",
            Type = ItemTypeForLittlePaws.Bed,
            CompatibleWith = new List<PetTypeForLittlePaws> {
                PetTypeForLittlePaws.Raccoon, PetTypeForLittlePaws.Rabbit, PetTypeForLittlePaws.Hedgehog
            },
            AffectedStat = PetStatForLittlePaws.SnuggleFactor,
            EffectAmount = 40,
            Duration = 7.0f
        },
        new Item {
            Name = "Nest Box",
            Type = ItemTypeForLittlePaws.Bed,
            CompatibleWith = new List<PetTypeForLittlePaws> { PetTypeForLittlePaws.Parrot },
            AffectedStat = PetStatForLittlePaws.SnuggleFactor,
            EffectAmount = 35,
            Duration = 4.0f
        },

        // Water Bottles
        new Item {
            Name = "Fresh Water Bottle",
            Type = ItemTypeForLittlePaws.WaterBottle,
            CompatibleWith = new List<PetTypeForLittlePaws> {
                PetTypeForLittlePaws.Rabbit, PetTypeForLittlePaws.Hedgehog
            },
            AffectedStat = PetStatForLittlePaws.Hunger,
            EffectAmount = 10,
            Duration = 1.0f
        },

        // Grooming Kits for Fluffiness
        new Item {
            Name = "Deluxe Grooming Kit",
            Type = ItemTypeForLittlePaws.GroomingKit,
            CompatibleWith = new List<PetTypeForLittlePaws> {
                PetTypeForLittlePaws.Raccoon, PetTypeForLittlePaws.Rabbit,
                PetTypeForLittlePaws.Parrot, PetTypeForLittlePaws.Hedgehog
            },
            AffectedStat = PetStatForLittlePaws.Fluffiness,
            EffectAmount = 40,
            Duration = 4.0f
        },
        new Item {
            Name = "Waterproof Grooming Set",
            Type = ItemTypeForLittlePaws.GroomingKit,
            CompatibleWith = new List<PetTypeForLittlePaws> { PetTypeForLittlePaws.Otter },
            AffectedStat = PetStatForLittlePaws.Fluffiness,
            EffectAmount = 35,
            Duration = 3.0f
        },

        // Medicine
        new Item {
            Name = "Health Booster",
            Type = ItemTypeForLittlePaws.Medicine,
            CompatibleWith = new List<PetTypeForLittlePaws> {
                PetTypeForLittlePaws.Raccoon, PetTypeForLittlePaws.Rabbit,
                PetTypeForLittlePaws.Parrot, PetTypeForLittlePaws.Otter, PetTypeForLittlePaws.Hedgehog
            },
            AffectedStat = PetStatForLittlePaws.Hunger,
            EffectAmount = 50,
            Duration = 1.0f
        },

        // Clothing & Accessories
        new Item {
            Name = "Cute Bow Tie",
            Type = ItemTypeForLittlePaws.Clothing,
            CompatibleWith = new List<PetTypeForLittlePaws> {
                PetTypeForLittlePaws.Rabbit, PetTypeForLittlePaws.Hedgehog
            },
            AffectedStat = PetStatForLittlePaws.Sassiness,
            EffectAmount = 30,
            Duration = 0.5f
        },
        new Item {
            Name = "Tiny Hat",
            Type = ItemTypeForLittlePaws.Accessory,
            CompatibleWith = new List<PetTypeForLittlePaws> {
                PetTypeForLittlePaws.Parrot, PetTypeForLittlePaws.Hedgehog
            },
            AffectedStat = PetStatForLittlePaws.Sassiness,
            EffectAmount = 25,
            Duration = 0.5f
        },

        // Scratching Posts
        new Item {
            Name = "Mini Scratching Post",
            Type = ItemTypeForLittlePaws.ScratchingPost,
            CompatibleWith = new List<PetTypeForLittlePaws> {
                PetTypeForLittlePaws.Raccoon, PetTypeForLittlePaws.Hedgehog
            },
            AffectedStat = PetStatForLittlePaws.Whimsy,
            EffectAmount = 20,
            Duration = 2.5f
        },

        // Habitats
        new Item {
            Name = "Tree Hollow Habitat",
            Type = ItemTypeForLittlePaws.Habitat,
            CompatibleWith = new List<PetTypeForLittlePaws> { PetTypeForLittlePaws.Raccoon },
            AffectedStat = PetStatForLittlePaws.SnuggleFactor,
            EffectAmount = 45,
            Duration = 8.0f
        },
        new Item {
            Name = "Aquatic Playground",
            Type = ItemTypeForLittlePaws.Habitat,
            CompatibleWith = new List<PetTypeForLittlePaws> { PetTypeForLittlePaws.Otter },
            AffectedStat = PetStatForLittlePaws.Zoomies,
            EffectAmount = 50,
            Duration = 6.0f
        },
        new Item {
            Name = "Tropical Perch Setup",
            Type = ItemTypeForLittlePaws.Habitat,
            CompatibleWith = new List<PetTypeForLittlePaws> { PetTypeForLittlePaws.Parrot },
            AffectedStat = PetStatForLittlePaws.SocialMeter,
            EffectAmount = 40,
            Duration = 5.0f
        }
    };
}// Generic command interface for Little Paws
public interface ICommand<TTarget, TContext>
{
    void Execute(TTarget target, TContext context);
}

// Example: FeedCommand for a specific pet type and item
public class FeedCommand<TPet> : ICommand<TPet, Item>
    where TPet : Enum // e.g., PetTypeForLittlePaws
{
    public void Execute(TPet petType, Item item)
    {
        // Example logic: only allow feeding if item is compatible
        if (item.CompatibleWith.Contains((PetTypeForLittlePaws)(object)petType) &&
            item.Type == ItemTypeForLittlePaws.Food)
        {
            // Apply item effect to pet (pseudo-code, actual pet object not shown)
            // e.g., pet.Hunger += item.EffectAmount;
            System.Console.WriteLine($"{petType} is fed {item.Name} (+{item.EffectAmount} Hunger)");
        }
        else
        {
            System.Console.WriteLine($"{item.Name} is not compatible with {petType} or is not food.");
        }
    }
}

// Example: PlayCommand for a specific pet type and toy item
public class PlayCommand<TPet> : ICommand<TPet, Item>
    where TPet : Enum
{
    public void Execute(TPet petType, Item item)
    {
        if (item.CompatibleWith.Contains((PetTypeForLittlePaws)(object)petType) &&
            (item.Type == ItemTypeForLittlePaws.Toy || item.Type == ItemTypeForLittlePaws.Puzzle))
        {
            // Apply play effect (pseudo-code)
            System.Console.WriteLine($"{petType} plays with {item.Name} (+{item.EffectAmount} {item.AffectedStat})");
        }
        else
        {
            System.Console.WriteLine($"{item.Name} is not a valid toy for {petType}.");
        }
    }
}

