using System;

class Program
{
    static void Main(string[] args)
    {
        // Create collections
        Collection cardCollection = new Collection("Cards", "Magic the Gathering");
        Collection miniatureCollection = new Collection("Miniatures", "Warhammer");
        Collection gameCollection = new Collection("Video Games", "RPG");

        // Create items
        Card card1 = new Card(
            name: "Black Lotus",
            category: "Cards",
            subCategory: "Magic the Gathering",
            quantity: 1,
            condition: "Near Mint",
            notes: "Very valuable card",
            cardSet: "Alpha"
        );

        Miniature mini1 = new Miniature(
            name: "Space Marine Captain",
            category: "Miniatures",
            subCategory: "Warhammer",
            quantity: 1,
            condition: "Good",
            notes: "Needs touch-up paint",
            painted: true
        );

        Game game1 = new Game(
            name: "Baldur's Gate 3",
            category: "Video Games",
            subCategory: "RPG",
            quantity: 1,
            condition: "Digital",
            notes: "Completed once",
            platform: "PC"
        );

        // Add items to collections
        cardCollection.AddItem(card1);
        miniatureCollection.AddItem(mini1);
        gameCollection.AddItem(game1);

        // Display collections
        cardCollection.DisplayCollection();
        miniatureCollection.DisplayCollection();
        gameCollection.DisplayCollection();

        // Test removing an item
        Console.WriteLine("Removing Black Lotus...\n");
        cardCollection.RemoveItem("Black Lotus");

        // Display again to confirm removal
        cardCollection.DisplayCollection();

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}