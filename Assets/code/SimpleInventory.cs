using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimpleInventory : MonoBehaviour
{
    public static SimpleInventory instance;

    public List<string> tuiDo = new List<string>();

    public GameObject baloPanel;
    public TextMeshProUGUI txtDanhSachDo;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        baloPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleBalo();
        }
    }

    // Hàm nhặt đồ - quan trọng nhất
    public void NhatDo(string tenMonDo)
    {
        tuiDo.Add(tenMonDo);
        Debug.Log($"Đã nhặt: {tenMonDo} | Tổng: {tuiDo.Count} vật phẩm");

        // Nếu balo đang mở thì cập nhật ngay lập tức
        if (baloPanel.activeSelf)
        {
            CapNhatChuBalo();
        }
    }

    void ToggleBalo()
    {
        bool trangThaiMoi = !baloPanel.activeSelf;
        baloPanel.SetActive(trangThaiMoi);

        if (trangThaiMoi)
            CapNhatChuBalo();
    }

    // Cập nhật danh sách vật phẩm
    void CapNhatChuBalo()
    {
        if (tuiDo.Count == 0)
        {
            txtDanhSachDo.text = "Balo của bạn đang trống rỗng!";
            return;
        }

        string noiDung = "BALO CỦA BẠN GỒM CÓ:\n\n";
        foreach (string monDo in tuiDo)
        {
            noiDung += $"• {monDo}\n";
        }

        txtDanhSachDo.text = noiDung;
    }
}