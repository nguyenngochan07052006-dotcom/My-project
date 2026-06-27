using UnityEngine;

public class FuseBox : MonoBehaviour
{
    [Header("State")]
    public bool wiresInstalled = false;
    public bool powerIsOn = false;

    [Header("References")]
    public GameObject redWireVisual;  // Model dây đỏ ẩn sẵn trong hộp điện
    public GameObject blueWireVisual; // Model dây xanh ẩn sẵn trong hộp điện
    public Animator leverAnimator;     // Animator để chạy animation gạt cầu dao
    public GameObject houseLights;     // GameObject cha chứa toàn bộ đèn trong nhà

    private void Start()
    {
        // Ban đầu ẩn dây trong hộp và tắt hết đèn nhà đi cho tối tăm
        if(redWireVisual) redWireVisual.SetActive(false);
        if(blueWireVisual) blueWireVisual.SetActive(false);
        if(houseLights) houseLights.SetActive(false);
    }

    // Hàm này gọi khi người chơi bấm tương tác (E hoặc Click chuột) vào hộp điện
    public void InteractWithBox(PlayerInventory playerInv)
    {
        if (!wiresInstalled)
        {
            // Kiểm tra xem player có đủ 2 dây chưa
            if (playerInv.hasRedWire && playerInv.hasBlueWire)
            {
                wiresInstalled = true;
                if(redWireVisual) redWireVisual.SetActive(true);
                if(blueWireVisual) blueWireVisual.SetActive(true);
                Debug.Log("Đã lắp dây điện thành công!");
            }
            else
            {
                Debug.Log("Thiếu dây điện để sửa!");
            }
        }
        else if (!powerIsOn)
        {
            // Nếu đã lắp dây nhưng chưa gạt cầu dao
            ActivatePower();
        }
    }

    void ActivatePower()
    {
        powerIsOn = true;
        
        // 1. Chạy hiệu ứng gạt tay cầm xuống (nếu bạn có làm animation cho Main Switch)
        if (leverAnimator != null)
        {
            leverAnimator.SetTrigger("FlipSwitch");
        }

        // 2. Bật toàn bộ đèn trong nhà lên
        if (houseLights != null)
        {
            houseLights.SetActive(true);
            Debug.Log("Đèn nhà đã sáng!");
        }
    }
}