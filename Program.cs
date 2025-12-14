using System;
using System.Data.SQLite;
using System.Collections.Generic;

class Program {
    static void Main(string[] args) {
        SQLiteConnection conn = SQLiteDatabase.Connect("Inventory.db");
        InventoryDb.CreateTable(conn);

        Collection cardCollection = new Collection("Cards", "Magic the Gathering");
        Collection miniatureCollection = new Collection("Miniatures", "Warhammer");
        Collection gameCollection = new Collection("Video Games", "RPG");

        Card card1 = new Card("Black Lotus", "Cards", "Magic the Gathering", 1, "Near Mint", "Very valuable card", "Alpha");
        Miniature mini1 = new Miniature("Space Marine Captain", "Miniatures", "Warhammer", 1, "Good", "Needs touch-up paint", true);
        Game game1 = new Game("Baldur's Gate 3", "Video Games", "RPG", 1, "Digital", "Completed once", "PC");

        cardCollection.AddItem(card1);
        miniatureCollection.AddItem(mini1);
        gameCollection.AddItem(game1);

        InventoryDb.AddItem(conn, card1, "Card", card1.CardSet);
        InventoryDb.AddItem(conn, mini1, "Miniature", mini1.Painted.ToString());
        InventoryDb.AddItem(conn, game1, "Game", game1.Platform);

        Console.WriteLine("\nLoading collections from database:\n");

        List<Collection> loadedCollections = InventoryDb.LoadCollections(conn);
        foreach (Collection collection in loadedCollections) {
            collection.DisplayCollection();
        }

        Console.WriteLine("Loading all items from database:\n");

        List<Item> dbItems = InventoryDb.GetAllItems(conn);
        foreach (Item item in dbItems) {
            item.DisplayDetails();
        }
        
        bool running = true;

        while (running) {
            Console.Write("\nChoose an action: (a)dd, (d)elete, (q)uit: ");
            string choice = Console.ReadLine();

            if (choice.Equals("a", StringComparison.OrdinalIgnoreCase)) {
                bool keepAdding = true;

                while (keepAdding) {
                    Console.Write("\nWould you like to add a new item? (y/n): ");
                    string response = Console.ReadLine();

                    if (response.Equals("y", StringComparison.OrdinalIgnoreCase)) {
                        Item newItem = CreateItemFromUser();

                        if (newItem != null) {
                            string itemType = newItem.GetType().Name;
                            string extra = "";

                            if (newItem is Card card) {
                                extra = card.CardSet;
                            } else if (newItem is Game game) {
                                extra = game.Platform;
                            } else if (newItem is Miniature mini) {
                                extra = mini.Painted.ToString();
                            }
                            InventoryDb.AddItem(conn, newItem, itemType, extra);
                            Console.WriteLine("Item added successfully.");
                        }
                    } else {
                        keepAdding = false;
                    }
                }
            } else if (choice.Equals("d", StringComparison.OrdinalIgnoreCase)) {
                bool keepDeleting = true;

                while (keepDeleting) {
                    Console.Write("\nWould you like to delete an item? (y/n): ");
                    string response = Console.ReadLine();

                    if (response.Equals("y", StringComparison.OrdinalIgnoreCase)) {
                        Console.Write("Enter the name of the item to delete: ");
                        string name = Console.ReadLine();

                        string category = InventoryDb.GetCategoryByItemName(conn, name);

                        InventoryDb.DeleteItem(conn, name);
                        Console.WriteLine("Item deleted (if it existed).");

                        if (!string.IsNullOrEmpty(category)) {
                            Console.WriteLine($"\nUpdated items in category: {category}\n");

                            List<Item> updatedItems = InventoryDb.GetItemsByCategory(conn, category);
                            foreach (Item item in updatedItems) {
                                item.DisplayDetails();
                            }
                        }
                    } else {
                        keepDeleting = false;
                    }
                }
            } else if (choice.Equals("q", StringComparison.OrdinalIgnoreCase)) {
                running = false;
                Console.ReadKey();
            }
        } 
    }

    static Item CreateItemFromUser() {
        Console.Write("Enter item type (Card / Game / Miniature): ");
        string itemType = Console.ReadLine();

        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("Category: ");
        string category = Console.ReadLine();

        Console.Write("SubCategory: ");
        string subCategory = Console.ReadLine();

        Console.Write("Quantity: ");
        int quantity = int.Parse(Console.ReadLine());

        Console.Write("Condition: ");
        string condition = Console.ReadLine();

        Console.Write("Notes: ");
        string notes = Console.ReadLine();

        if (itemType.Equals("Card", StringComparison.OrdinalIgnoreCase)) {
            Console.Write("Card Set: ");
            string cardSet = Console.ReadLine();
            return new Card(name, category, subCategory, quantity, condition, notes, cardSet);
        } else if (itemType.Equals("Game", StringComparison.OrdinalIgnoreCase)) {
            Console.Write("Platform: ");
            string platform = Console.ReadLine();
            return new Game(name, category, subCategory, quantity, condition, notes, platform);
        } else if (itemType.Equals("Miniature", StringComparison.OrdinalIgnoreCase)) {
            Console.Write("Painted (true/false): ");
            bool painted = bool.Parse(Console.ReadLine());
            return new Miniature(name, category, subCategory, quantity, condition, notes, painted);
        } else {
            Console.WriteLine("Invalid item type.");
            return null;
        }
    }
}
