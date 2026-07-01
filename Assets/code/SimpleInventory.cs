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
        if (baloPanel != null)
            baloPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleBalo();
        }
    }

    // Hàm nhặt đồ
    public void NhatDo(string tenMonDo)
    {
        tuiDo.Add(tenMonDo);
        Debug.Log("Đã nhặt: " + tenMonDo);

        if (baloPanel != null && baloPanel.activeSelf)
            CapNhatChuBalo();
    }

    void ToggleBalo()
    {
        if (baloPanel == null) return;

        bool trangThaiMoi = !baloPanel.activeSelf;
        baloPanel.SetActive(trangThaiMoi);

        if (trangThaiMoi)
            CapNhatChuBalo();
    }

    void CapNhatChuBalo()
    {
        if (txtDanhSachDo == null) return;

        if (tuiDo.Count == 0)
        {
            txtDanhSachDo.text = "Balo của bạn đang trống rỗng!";
            return;
        }

        string noiDung = "BALO CỦA BẠN:\n\n";
        foreach (string monDo in tuiDo)
        {
            noiDung += "• " + monDo + "\n";
        }

        txtDanhSachDo.text = noiDung;
    }
}