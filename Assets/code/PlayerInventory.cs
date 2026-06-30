using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasRedWire = false;
    public bool hasBlueWire = false;
    public bool hasCrowbar = false; // Thêm biến này để lưu trạng thái xà beng

    // Thêm hàm này để FuseBox có thể kiểm tra
    public bool HasItem(string itemName)
    {
        if (itemName == "Crowbar") return hasCrowbar;
        if (itemName == "RedWire") return hasRedWire;
        if (itemName == "BlueWire") return hasBlueWire;
        
        return false;
    }
}