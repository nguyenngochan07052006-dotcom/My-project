using UnityEngine;

public class AutoSlidingDoor : MonoBehaviour
{
    [Header("Doors")]
    public Transform leftDoor;
    public Transform rightDoor;

    [Header("Settings")]
    public float slideDistance = 2f;
    public float slideSpeed = 3f;
    public float closeDelay = 1.5f;     // Delay trước khi đóng

    private Vector3 leftClosed, rightClosed;
    private Vector3 leftOpen, rightOpen;
    private bool playerInside = false;
    private float closeTimer = 0f;
    private Collider[] doorColliders;

    void Start()
    {
        leftClosed = leftDoor.localPosition;
        rightClosed = rightDoor.localPosition;

        leftOpen = leftClosed + Vector3.left * slideDistance;
        rightOpen = rightClosed + Vector3.right * slideDistance;

        doorColliders = GetComponentsInChildren<Collider>(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            closeTimer = 0f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            closeTimer = closeDelay;   // Bắt đầu đếm delay đóng cửa
        }
    }

    void Update()
    {
        // Mở cửa
        if (playerInside)
        {
            leftDoor.localPosition = Vector3.MoveTowards(leftDoor.localPosition, leftOpen, slideSpeed * Time.deltaTime);
            rightDoor.localPosition = Vector3.MoveTowards(rightDoor.localPosition, rightOpen, slideSpeed * Time.deltaTime);
            closeTimer = 0f;
        }
        // Đóng cửa sau delay
        else
        {
            if (closeTimer > 0)
            {
                closeTimer -= Time.deltaTime;
            }
            else
            {
                leftDoor.localPosition = Vector3.MoveTowards(leftDoor.localPosition, leftClosed, slideSpeed * Time.deltaTime);
                rightDoor.localPosition = Vector3.MoveTowards(rightDoor.localPosition, rightClosed, slideSpeed * Time.deltaTime);
            }
        }

        // Tắt collider khi cửa mở hoàn toàn
        bool fullyOpen = Vector3.Distance(leftDoor.localPosition, leftOpen) < 0.05f;
        foreach (var col in doorColliders)
        {
            if (col != null && !col.isTrigger)
                col.enabled = !fullyOpen;
        }
    }
}