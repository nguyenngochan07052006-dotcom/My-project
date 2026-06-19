using UnityEngine;

public class AutoSingleDoorHinge : MonoBehaviour
{
    [Header("Cài đặt Cửa")]
    public Transform doorMesh;           // Kéo object "Door" vào
    public float openAngle = 90f;        // 90 hoặc -90
    public float openSpeed = 4f;
    public float closeDelay = 2f;

    [Header("Collider")]
    public Collider doorCollider;        // Kéo Mesh Collider của Door vào

    private Quaternion closedRotation;
    private Quaternion openedRotation;
    private bool playerNear = false;
    private float closeTimer = 0f;

    void Start()
    {
        closedRotation = doorMesh.localRotation;
        openedRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);

        if (doorCollider == null)
            doorCollider = doorMesh.GetComponent<Collider>();
    }

    void Update()
    {
        Quaternion targetRot = playerNear ? openedRotation : closedRotation;

        doorMesh.localRotation = Quaternion.Slerp(
            doorMesh.localRotation,
            targetRot,
            openSpeed * Time.deltaTime
        );

        // Tắt collider khi cửa mở để tránh đẩy player
        if (doorCollider != null)
        {
            bool almostOpen = Quaternion.Angle(doorMesh.localRotation, openedRotation) < 10f;
            doorCollider.enabled = !almostOpen;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            closeTimer = closeDelay;
        }
    }
}