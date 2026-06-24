using UnityEngine;

public class SceneMergerController : MonoBehaviour
{
    [Header("--- QUẢN LÝ PANEL UI ---")]
    public GameObject loginPanel;       // Kéo LoginPanel vào đây
    public GameObject mainMenuPanel;   // Kéo MainMenuPanel vào đây

    [Header("--- QUẢN LÝ CỤM OBJECT CHÍNH ---")]
    public GameObject mainMenuCum;     // Kéo ---MAINMENU_CUM--- vào đây
    public GameObject gameplayCum;     // Kéo ---GAMEPLAY_CUM--- vào đây

    [Header("--- QUẢN LÝ CAMERA ---")]
    public Camera menuCamera;          // Kéo Main Camera của Menu vào đây

    // Hàm tự động chạy ngay khi bấm nút PLAY
    void Start()
    {
        // 1. Ép hiển thị cụm Menu và tắt cụm Màn chơi chính
        if (mainMenuCum != null) mainMenuCum.SetActive(true);
        if (gameplayCum != null) gameplayCum.SetActive(false);

        // 2. Thiết lập trạng thái hiển thị ban đầu cho các Panel UI
        if (loginPanel != null) loginPanel.SetActive(true);
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);

        // 3. Ưu tiên Camera của Menu hiển thị đè lên trên bằng code
        if (menuCamera != null)
        {
            menuCamera.gameObject.SetActive(true);
            menuCamera.depth = 10; // Số lớn để luôn hiện lên trước
        }

        // 4. Hiện con trỏ chuột ra màn hình để người chơi click bấm nút
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // CHỨC NĂNG 1: Bấm "BẮT ĐẦU VỤ ÁN" (Chuyển từ Login sang MainMenu)
    public void ChuyenSangMainMenu()
    {
        Debug.Log("==> ĐÃ CLICK NÚT BẤM THÀNH CÔNG!");

        if (loginPanel == null) Debug.LogError("LỖI: Ô Login Panel đang bị TRỐNG, không có gì để ẩn!");
        if (mainMenuPanel == null) Debug.LogError("LỖI: Ô Main Menu Panel đang bị TRỐNG, không có gì để bật!");

        if (loginPanel != null && mainMenuPanel != null)
        {
            loginPanel.SetActive(false);     // Ẩn màn hình đăng nhập
            mainMenuPanel.SetActive(true);   // Hiện menu chính

            Debug.Log("==> Đã ẩn LoginPanel và bật MainMenuPanel thành công!");
        }
    }

    // CHỨC NĂNG 2: Bấm "BẮT ĐẦU ĐIỀU TRA" (Chính thức vào Map chơi)
    public void VaoThaoLuanVaoGame()
    {
        if (mainMenuCum != null && gameplayCum != null)
        {
            mainMenuCum.SetActive(false); // Tắt toàn bộ cụm Menu, chữ đỏ, camera menu
            gameplayCum.SetActive(true);  // Kích hoạt Player và Map bệnh viện lên
            
            Debug.Log("Đã kích hoạt Gameplay thành công!");
        }
    }

    // CHỨC NĂNG 3: Bấm "THOÁT" hoặc "RỜI KHỎI VỤ ÁN"
    public void ThoatGame()
    {
        Debug.Log("Người chơi đã bấm thoát game!");
        Application.Quit(); // Tắt file game (.exe) khi Build ra ngoài
    }
}