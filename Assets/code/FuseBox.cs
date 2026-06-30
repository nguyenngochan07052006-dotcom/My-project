using UnityEngine;

public class FuseBox : MonoBehaviour
{
    [Header("Cấu hình mở thùng")]
    public bool isBoxLocked = true;
    public string requiredItemName = "Crowbar";
    public Animator boxAnimator;

    [Header("Sockets & Installed Wires")]
    public GameObject wireRedInstalled;  
    public GameObject wireBlueInstalled;

    [Header("State")]
    public bool isRedFixed = false;
    public bool isBlueFixed = false;
    public bool powerIsOn = false;

    [Header("References")]
    public Transform leverTransform;    
    public GameObject[] allLightGroups;

    private void Start()
    {
        // 1. Tắt các dây điện và đèn mặc định
        if (wireRedInstalled) wireRedInstalled.SetActive(false);
        if (wireBlueInstalled) wireBlueInstalled.SetActive(false);
        
        foreach (GameObject group in allLightGroups)
        {
            if (group != null) group.SetActive(false);
        }

        // 2. ÉP ANIMATOR DỪNG Ở TRẠNG THÁI ĐÓNG
        // Đảm bảo không có bất kỳ animation nào phát ra khi game start
        if (boxAnimator != null)
        {
            boxAnimator.speed = 0; // Tạm dừng animation ngay lập tức
            boxAnimator.Play("Idle", 0, 0f); // Ép về trạng thái Idle tại giây 0
        }
    }

    public void InteractWithBox(PlayerInventory playerInv)
    {
        // 1. XỬ LÝ MỞ KHÓA
        if (isBoxLocked)
        {
            if (playerInv.HasItem(requiredItemName))
            {
                isBoxLocked = false;
                
                // MỞ KHÓA ANIMATION VÀ KÍCH HOẠT
                if (boxAnimator != null) 
                {
                    boxAnimator.speed = 1; // Cho phép animation chạy
                    boxAnimator.SetTrigger("Open");
                }
                Debug.Log("Đã mở thùng bằng Xà beng!");
            }
            else
            {
                Debug.Log("Cần có Xà beng để mở thùng này!");
            }
            return; 
        }

        // 2. XỬ LÝ LẮP DÂY
        if (powerIsOn) return;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            RemoveWires(playerInv);
            return;
        }

        if (!isRedFixed && playerInv.hasRedWire)
        {
            isRedFixed = true;
            playerInv.hasRedWire = false;
            if (wireRedInstalled) wireRedInstalled.SetActive(true);
        }

        if (!isBlueFixed && playerInv.hasBlueWire)
        {
            isBlueFixed = true;
            playerInv.hasBlueWire = false;
            if (wireBlueInstalled) wireBlueInstalled.SetActive(true);
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
        if (leverTransform != null) leverTransform.localEulerAngles = new Vector3(0, 180, 0);
        
        foreach (GameObject group in allLightGroups)
        {
            if (group != null)
            {
                group.SetActive(true);
                foreach (Transform child in group.transform) child.gameObject.SetActive(true);
            }
        }
        Debug.Log("Cầu dao đã gạt! Toàn bộ đèn đã sáng.");
    }
}