using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public enum ItemType { RedWire, BlueWire, Crowbar }
    public ItemType itemToPickUp; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inv = other.GetComponent<PlayerInventory>();
            if (inv != null)
            {
                if (itemToPickUp == ItemType.RedWire) inv.hasRedWire = true;
                else if (itemToPickUp == ItemType.BlueWire) inv.hasBlueWire = true;
                else if (itemToPickUp == ItemType.Crowbar) inv.hasCrowbar = true;
                
                Debug.Log("Đã nhặt: " + itemToPickUp.ToString());
                Destroy(gameObject); 
            }
        }
    }
}