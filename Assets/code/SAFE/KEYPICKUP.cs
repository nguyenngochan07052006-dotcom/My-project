using UnityEngine;
using TMPro;

public class KeyPickup : MonoBehaviour
{
    [Header("Thông tin chìa khóa")]
    public string keyName = "Chìa khóa tủ";

    [Header("UI Prompt")]
    public TextMeshProUGUI pickupPrompt;     // Kéo Text "Nhấn E để nhặt chìa khóa" vào đây

    [Header("Hiệu ứng")]
    public GameObject pickupEffect;          // Tùy chọn: hiệu ứng khi nhặt

    private bool canPickup = false;
    private bool isActive = false;           // Kiểm soát chìa khóa đã hiện chưa

    void Update()
    {
        // Hiển thị prompt chỉ khi chìa khóa đã active và player gần
        if (pickupPrompt != null)
        {
            pickupPrompt.gameObject.SetActive(canPickup && isActive);
        }

        // Nhấn E để nhặt
        if (canPickup && isActive && Input.GetKeyDown(KeyCode.E))
        {
            PickupKey();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = false;
        }
    }

    private void PickupKey()
    {
        Debug.Log($"Đã nhặt {keyName}!");

        if (pickupPrompt != null)
            pickupPrompt.gameObject.SetActive(false);

        // Tạo hiệu ứng nhặt (nếu có)
        if (pickupEffect != null)
        {
            Instantiate(pickupEffect, transform.position, Quaternion.identity);
        }

        // Xóa chìa khóa sau khi nhặt
        Destroy(gameObject);
    }

    // Hàm này được gọi từ script Safe khi mở tủ thành công
    public void ActivateKey()
    {
        isActive = true;
        gameObject.SetActive(true);

        Debug.Log("Chìa khóa đã sẵn sàng để nhặt!");
    }
}