using UnityEngine;
using TMPro;

public class EscapeDoorController : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject baloPanel;
    public GameObject baoVayPanel;
    public GameObject chienThangPanel;

    [Header("UI Text")]
    public TextMeshProUGUI txtThongBao;

    [Header("Cấu hình")]
    public string tenChiaKhoa = "Chìa khóa tủ";
    public string tenHungThuDung = "Trung";

    private bool playerNear = false;
    private SimpleInventory playerInventory;

    void Start()
    {
        if (baoVayPanel != null) baoVayPanel.SetActive(false);
        if (chienThangPanel != null) chienThangPanel.SetActive(false);
        if (txtThongBao != null) txtThongBao.gameObject.SetActive(false);
    }

    void Update()
    {
        // Chỉ cho phép nhấn E khi đứng gần cửa
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            KiemTraDieuKienMoCua();
        }
    }

    private void KiemTraDieuKienMoCua()
    {
        if (playerInventory == null) return;

        if (playerInventory.tuiDo.Contains(tenChiaKhoa))
        {
            // Có chìa khóa → hiện panel chọn hung thủ
            if (baoVayPanel != null)
            {
                baoVayPanel.SetActive(true);
                if (baloPanel != null) baloPanel.SetActive(false);

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
        else
        {
            // Chưa có chìa
            ShowMessage("Cửa bị khóa!\nBạn cần Chìa khóa để mở.");
        }
    }

    private void ShowMessage(string message)
    {
        if (txtThongBao != null)
        {
            txtThongBao.text = message;
            txtThongBao.gameObject.SetActive(true);
        }
    }

    public void ChonHungThu(string tenNghiPham)
    {
        if (tenNghiPham == tenHungThuDung)  // Chọn đúng
        {
            Debug.Log("=== CHỌN ĐÚNG - THẮNG GAME ===");

            if (baoVayPanel != null) baoVayPanel.SetActive(false);
            if (chienThangPanel != null) chienThangPanel.SetActive(true);

            gameObject.SetActive(false); // Ẩn cửa

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
        else  // Chọn SAI → Quay về phòng Office
        {
            Debug.Log("Chọn sai! Teleport về Office...");

            if (txtThongBao != null)
            {
                txtThongBao.text = "Sai hung thủ!\nBạn bị bắt lại và đưa về phòng Office.";
                txtThongBao.gameObject.SetActive(true);
            }

            // Ẩn panel chọn
            if (baoVayPanel != null) baoVayPanel.SetActive(false);

            // === TELEPORT VỀ PHÒNG OFFICE ===
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                // Thay đổi vị trí này thành vị trí bắt đầu của bạn trong phòng Office
                player.transform.position = new Vector3(8.34f, 2.48f, -9.35f);   // ← SỬA TỌA ĐỘ NÀY
                player.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                Debug.LogError("Không tìm thấy Player!");
            }
        }
    }

    // ================== TRIGGER ==================
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            playerInventory = other.GetComponent<SimpleInventory>();

            ShowMessage("Nhấn [E] để mở cửa chính");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            playerInventory = null;

            if (txtThongBao != null)
                txtThongBao.gameObject.SetActive(false);

            if (baoVayPanel != null)
                baoVayPanel.SetActive(false);
        }
    }
}