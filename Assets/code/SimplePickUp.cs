using UnityEngine;

public class SimplePickUp : MonoBehaviour
{
    public string tenVatPham = "Chìa khóa tủ";   // Đổi tên vật phẩm ở đây

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SimpleInventory.instance.NhatDo(tenVatPham);
            Destroy(gameObject);   // Xóa vật phẩm sau khi nhặt
        }
    }
}