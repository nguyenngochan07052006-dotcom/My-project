using UnityEngine;
using TMPro; // Nếu dùng TextMeshPro

public class PaperInteract : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject interactHint; // Kéo InteractHint UI vào đây
    public GameObject paperPanel;  // Kéo PaperPanel UI vào đây

    private bool isInsideTrigger = false;
    private bool isReading = false;

    void Update()
    {
        // Khi người chơi đang ở trong vùng và nhấn nút E
        if (isInsideTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (!isReading)
            {
                OpenPaper();
            }
            else
            {
                ClosePaper();
            }
        }
    }

    // Phát hiện Player đi vào vùng tương tác
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInsideTrigger = true;
            if (!isReading)
            {
                interactHint.SetActive(true); // Hiện chữ "Nhấn E"
            }
        }
    }

    // Phát hiện Player đi ra khỏi vùng tương tác
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInsideTrigger = false;
            interactHint.SetActive(false); // Ẩn chữ "Nhấn E"
            ClosePaper(); // Nếu đang đọc mà đi ra xa thì tự đóng
        }
    }

    void OpenPaper()
    {
        isReading = true;
        paperPanel.SetActive(true);   // Hiện bảng đọc giấy
        interactHint.SetActive(false); // Ẩn chữ gợi ý E đi vì đang đọc rồi

        // (Tùy chọn) Dừng thời gian game hoặc khóa di chuyển của Player ở đây nếu muốn
        // Time.timeScale = 0f; 
    }

    void ClosePaper()
    {
        isReading = false;
        paperPanel.SetActive(false);  // Ẩn bảng đọc giấy

        if (isInsideTrigger)
        {
            interactHint.SetActive(true); // Hiện lại chữ gợi ý E nếu vẫn đứng gần
        }

        // (Tùy chọn) Tiếp tục game
        // Time.timeScale = 1f;
    }
}