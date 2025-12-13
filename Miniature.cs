/*
* Miniature class extends the Item base class and represents
* a miniature or figurine item. It adds properties specific
* to miniatures and overrides display behavior to include
* miniature-related details.
*/
public class Miniature : Item {
    public bool Painted { get; set; }

    public Miniature(string name, string category, string subCategory, int quantity, string condition, string notes, bool painted)
        : base(name, category, subCategory, quantity, condition, notes) {
        Painted = painted;
    }

    public override void DisplayDetails() {
        Console.WriteLine("=== Miniature Item ===");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Category: {Category}");
        Console.WriteLine($"SubCategory: {SubCategory}");
        Console.WriteLine($"Painted: {(Painted ? "Yes" : "No")}");
        Console.WriteLine($"Quantity: {Quantity}");
        Console.WriteLine($"Condition: {Condition}");
        Console.WriteLine($"Notes: {Notes}");
        Console.WriteLine();
    }
}