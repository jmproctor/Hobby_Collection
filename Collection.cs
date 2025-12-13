/*
* Collection class manages a group of inventory items based
* on category and subcategory. It provides functionality to
* add, remove, and display items while leveraging polymorphism
* through the Item base class.
*/
using System;
using System.Collections.Generic;
using System.Linq;

public class Collection {
    public string Category { get; set; }
    public string SubCategory { get; set; }
    public List<Item> Items { get; set; }

    public Collection(string category, string subCategory) {
        Category = category;
        SubCategory = subCategory;
        Items = new List<Item>();
    }

    public void AddItem(Item item) {
        Items.Add(item);
    }

    public void RemoveItem(string name) {
        Item itemToRemove = Items.FirstOrDefault(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (itemToRemove != null)
        {
            Items.Remove(itemToRemove);
        }
    }

    public void DisplayCollection() {
        Console.WriteLine($"=== Collection: {Category} - {SubCategory} ===");

        if (Items.Count == 0) {
            Console.WriteLine("No items in this collection.");
            return;
        }

        foreach (Item item in Items) {
            item.DisplayDetails();
        }
    }
}
