using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour 
{
    public static List<string> items = new List<string>();
    public static void AddItem(string itemName) { items.Add(itemName); }
    public static bool HasItem(string itemName) { return items.Contains(itemName); 
    }
}