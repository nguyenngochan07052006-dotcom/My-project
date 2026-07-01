using UnityEngine;
using TMPro;

public class KeyPickup : MonoBehaviour
{
    [Header("Thông tin chìa khóa")]
    public string keyName = "Chìa khóa tủ";

    [Header("UI Prompt")]
    public TextMeshProUGUI pickupPrompt;

    [Header("Hiệu ứng")]
    public GameObject pickupEffect;

    private bool canPickup = false;
    private bool isActive = true;   // Mặc định là true nếu chìa khóa luôn có thể nhặt

    private void Start()
    {
        if (pickupPrompt != null)
            pickupPrompt.gameObject.SetActive(false);
    }

    void Update()
    {
        if (pickupPrompt != null)
        {
            pickupPrompt.gameObject.SetActive(canPickup && isActive);
        }

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

        // === PHẦN QUAN TRỌNG: Thêm vào balo ===
        if (SimpleInventory.instance != null)
        {
            SimpleInventory.instance.NhatDo(keyName);
        }
        else
        {
            Debug.LogError("Không tìm thấy SimpleInventory!");
        }

        if (pickupPrompt != null)
            pickupPrompt.gameObject.SetActive(false);

        if (pickupEffect != null)
        {
            Instantiate(pickupEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    // Nếu bạn muốn chìa khóa xuất hiện sau khi làm nhiệm vụ nào đó
    public void ActivateKey()
    {
        isActive = true;
        gameObject.SetActive(true);
    }
}