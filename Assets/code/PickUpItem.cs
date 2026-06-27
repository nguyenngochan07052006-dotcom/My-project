using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public bool isRedWire; 
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inv = other.GetComponent<PlayerInventory>();
            if (inv != null)
            {
                if (isRedWire) inv.hasRedWire = true;
                else inv.hasBlueWire = true;
                
                Debug.Log("Đã nhặt dây điện!");
                Destroy(gameObject); 
            }
        }
    }
}