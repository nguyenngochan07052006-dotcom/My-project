using UnityEngine;

public class FuseBox : MonoBehaviour
{
    [Header("Sockets & Installed Wires")]
    // Kéo các Object dây ẩn (_Installed) trong hộp điện vào đây
    public GameObject wireRedInstalled;  
    public GameObject wireBlueInstalled; 

    [Header("State")]
    public bool isRedFixed = false;
    public bool isBlueFixed = false;
    public bool powerIsOn = false;

    [Header("References")]
    public Animator leverAnimator;     // Cầu dao Main Switch
    public GameObject houseLights;     // Hệ thống đèn nhà

    private void Start()
    {
        // Lúc đầu game, ẩn 2 sợi dây trong hộp điện đi
        if (wireRedInstalled) wireRedInstalled.SetActive(false);
        if (wireBlueInstalled) wireBlueInstalled.SetActive(false);
        if (houseLights) houseLights.SetActive(false);
    }

    // Hàm xử lý khi người chơi bấm tương tác vào Hộp điện
    public void InteractWithBox(PlayerInventory playerInv)
    {
        // 1. Kiểm tra sửa dây đỏ
        if (!isRedFixed && playerInv.hasRedWire)
        {
            isRedFixed = true;
            playerInv.hasRedWire = false; // Trừ khỏi kho đồ của player
            if (wireRedInstalled) wireRedInstalled.SetActive(true); // Hiện dây đỏ trong hộp
            Debug.Log("Đã nối xong dây điện ĐỎ vào socket!");
            return; // Thoát ra để tránh chạy logic gạt cần ngay lập tức
        }

        // 2. Kiểm tra sửa dây xanh
        if (!isBlueFixed && playerInv.hasBlueWire)
        {
            isBlueFixed = true;
            playerInv.hasBlueWire = false; // Trừ khỏi kho đồ của player
            if (wireBlueInstalled) wireBlueInstalled.SetActive(true); // Hiện dây xanh trong hộp
            Debug.Log("Đã nối xong dây điện XANH vào socket!");
            return;
        }

        // 3. Nếu đã sửa đủ cả 2 dây nhưng chưa gạt cầu dao
        if (isRedFixed && isBlueFixed && !powerIsOn)
        {
            ActivatePower();
        }
        else if ((!isRedFixed || !isBlueFixed) && !powerIsOn)
        {
            Debug.Log("Hộp điện vẫn thiếu dây, chưa thể gạt cầu dao!");
        }
    }

    void ActivatePower()
    {
        powerIsOn = true;
        
        // Chạy animation gạt cần Main Switch
        if (leverAnimator != null) leverAnimator.SetTrigger("FlipSwitch");

        // Bật đèn toàn bộ nhà
        if (houseLights != null)
        {
            houseLights.SetActive(true);
            Debug.Log("Cầu dao đã gạt! Đèn toàn bộ ngôi nhà đã sáng.");
        }
    }
}