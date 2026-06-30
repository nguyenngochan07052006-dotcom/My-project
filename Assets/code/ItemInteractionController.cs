using UnityEngine;

public class ItemInteractionController : MonoBehaviour
{
    [Header("Cấu hình")]
    public string requiredItemName = "Crowbar"; // Tên của Xà beng trong kho đồ
    public Animator boxAnimator;               // Animator của thùng điện
    public string openAnimationTrigger = "Open"; // Tên Trigger trong Animator

    [Header("Trạng thái")]
    public bool isLocked = true;              // Mặc định thùng bị khóa

    // Hàm này sẽ được gọi từ script Raycast/PlayerInteraction của nhân vật
    public void TryInteract(string itemNameHeld)
    {
        if (isLocked)
        {
            if (itemNameHeld == requiredItemName)
            {
                OpenBox();
            }
            else
            {
                Debug.Log("Cần xà beng để mở thùng này!");
            }
        }
    }

    private void OpenBox()
    {
        isLocked = false;
        if (boxAnimator != null)
        {
            boxAnimator.SetTrigger(openAnimationTrigger);
        }
        Debug.Log("Thùng đã mở!");
    }
}