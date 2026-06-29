using UnityEngine;
using TMPro; // Nếu bạn dùng TextMeshPro cho chữ "Bấm E"

public class PlayerInteraction : MonoBehaviour
{
    [Header("Settings")]
    public float interactionDistance = 3f;
    public Camera playerCamera; // Kéo Main Camera vào đây
    public TextMeshProUGUI interactionText; // Kéo UI Text vào đây

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            // Kiểm tra nếu nhìn vào vật thể có Tag "FuseBox"
            if (hit.collider.CompareTag("FuseBox"))
            {
                if (interactionText) interactionText.text = "Bấm E để sửa hộp điện";
                if (interactionText) interactionText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Lấy script FuseBox từ vật thể đang nhìn
                    FuseBox box = hit.collider.GetComponent<FuseBox>();
                    // Lấy Inventory từ chính nhân vật này
                    PlayerInventory inv = GetComponent<PlayerInventory>();
                    
                    if (box != null && inv != null)
                    {
                        box.InteractWithBox(inv);
                    }
                }
            }
            else
            {
                // Nếu nhìn chỗ khác thì ẩn chữ
                if (interactionText) interactionText.gameObject.SetActive(false);
            }
        }
        else
        {
            if (interactionText) interactionText.gameObject.SetActive(false);
        }
    }
}