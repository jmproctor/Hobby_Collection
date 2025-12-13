/*
* Item is an abstract base class that implements the IInventory
* interface. It provides shared properties and functionality
* for all inventory items while requiring derived classes to
* override the DisplayDetails() method.
*/
public abstract class Item : IInventory
{
    public string Name { get; set; }
    public string Category { get; set; }
    public string SubCategory { get; set; }
    public int Quantity { get; set; }
    public string Condition { get; set; }
    public string Notes { get; set; }

    protected Item(string name, string category, string subCategory, int quantity, string condition, string notes)
    {
        Name = name;
        Category = category;
        SubCategory = subCategory;
        Quantity = quantity;
        Condition = condition;
        Notes = notes;
    }

    public abstract void DisplayDetails();

    public override string ToString()
    {
        return $"{Name} (Qty: {Quantity}, Condition: {Condition})";
    }
}