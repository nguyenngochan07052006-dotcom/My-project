using UnityEngine;
using UnityEngine.UI; // Thư viện để làm việc với các nút bấm UI

public class MainMenuController : MonoBehaviour
{
    [Header("Kéo thả các Panel quản lý vào đây")]
    public GameObject loginPanel;      // Biến chứa màn hình đăng nhập
    public GameObject mainMenuPanel;  // Biến chứa màn hình menu chính

    // Hàm này sẽ tự động chạy ngay khi Scene Menu được load lên
    void Start()
    {
        // Đảm bảo khi mới mở game, màn hình đăng nhập luôn hiện và menu chính luôn ẩn
        if (loginPanel != null) loginPanel.SetActive(true);
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
    }

    // Hàm xử lý khi bấm nút "BẮT ĐẦU VỤ ÁN"
    public void ChuyenSangMainMenu()
    {
        if (loginPanel != null && mainMenuPanel != null)
        {
            loginPanel.SetActive(false);     // Ẩn màn hình đăng nhập đi
            mainMenuPanel.SetActive(true);   // Bật màn hình menu chính lên
        }
    }

    // Hàm xử lý khi bấm nút "RỜI KHỎI VỤ ÁN" (Thoát game)
    public void ThoatGame()
    {
        Debug.Log("Người chơi đã bấm thoát game!");
        Application.Quit(); // Lệnh tắt file game thực tế khi xuất bản (.exe)
    }
}