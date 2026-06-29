using UnityEngine;

public class FuseBox : MonoBehaviour
{
    [Header("Sockets & Installed Wires")]
    public GameObject wireRedInstalled;  
    public GameObject wireBlueInstalled; 

    [Header("State")]
    public bool isRedFixed = false;
    public bool isBlueFixed = false;
    public bool powerIsOn = false;

    [Header("References")]
    public Transform leverTransform;     
    // Thay vì 1 nhóm, ta dùng danh sách để chứa nhiều nhóm đèn
    public GameObject[] allLightGroups; 

    private void Start()
    {
        if (wireRedInstalled) wireRedInstalled.SetActive(false);
        if (wireBlueInstalled) wireBlueInstalled.SetActive(false);
        
        // Tắt tất cả nhóm đèn ngay khi game bắt đầu
        foreach (GameObject group in allLightGroups)
        {
            if (group != null) group.SetActive(false);
        }
    }

    public void InteractWithBox(PlayerInventory playerInv)
    {
        if (powerIsOn) return;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            RemoveWires(playerInv);
            return;
        }

        bool installed = false;
        if (!isRedFixed && playerInv.hasRedWire)
        {
            isRedFixed = true;
            playerInv.hasRedWire = false;
            if (wireRedInstalled) wireRedInstalled.SetActive(true);
            installed = true;
        }

        if (!isBlueFixed && playerInv.hasBlueWire)
        {
            isBlueFixed = true;
            playerInv.hasBlueWire = false;
            if (wireBlueInstalled) wireBlueInstalled.SetActive(true);
            installed = true;
        }

        if (isRedFixed && isBlueFixed)
        {
            ActivatePower();
        }
    }

    void RemoveWires(PlayerInventory playerInv)
    {
        if (isRedFixed) { isRedFixed = false; playerInv.hasRedWire = true; if (wireRedInstalled) wireRedInstalled.SetActive(false); }
        if (isBlueFixed) { isBlueFixed = false; playerInv.hasBlueWire = true; if (wireBlueInstalled) wireBlueInstalled.SetActive(false); }
    }

    void ActivatePower()
    {
        powerIsOn = true;
        
        // 1. Xoay cần gạt (Đảm bảo đã Remove Component Animator trên vật thể này)
        if (leverTransform != null) 
        {
            leverTransform.localEulerAngles = new Vector3(0, 180, 0); 
        }

        // 2. Bật tất cả các nhóm đèn đã gán
        foreach (GameObject group in allLightGroups)
        {
            if (group != null)
            {
                group.SetActive(true);
                // Ép tất cả đèn con trong nhóm đó sáng lên
                foreach (Transform child in group.transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
        Debug.Log("Cầu dao đã gạt! Toàn bộ đèn đã sáng.");
    }
}