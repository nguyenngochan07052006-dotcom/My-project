using UnityEngine;

public class AutoSlidingDoor : MonoBehaviour
{
    [Header("Cửa Trái - Phải")]
    public Transform leftDoor;
    public Transform rightDoor;

    [Header("Cài đặt")]
    public float slideDistance = 2.2f;     // Khoảng cách trượt sang 2 bên
    public float slideSpeed = 4f;          // Tốc độ trượt
    public float closeDelay = 2f;          // Delay trước khi đóng

    private Vector3 leftClosedPos;
    private Vector3 rightClosedPos;
    private Vector3 leftOpenPos;
    private Vector3 rightOpenPos;

    private bool playerNear = false;
    private float closeTimer = 0f;

    void Start()
    {
        // Lưu vị trí ban đầu
        leftClosedPos = leftDoor.localPosition;
        rightClosedPos = rightDoor.localPosition;

        // Vị trí khi mở (trượt sang 2 bên)
        leftOpenPos = leftClosedPos + Vector3.left * slideDistance;
        rightOpenPos = rightClosedPos + Vector3.right * slideDistance;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            closeTimer = 0;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            closeTimer = closeDelay;
        }
    }

    void Update()
    {
        if (playerNear) // Mở cửa
        {
            leftDoor.localPosition = Vector3.MoveTowards(leftDoor.localPosition, leftOpenPos, slideSpeed * Time.deltaTime);
            rightDoor.localPosition = Vector3.MoveTowards(rightDoor.localPosition, rightOpenPos, slideSpeed * Time.deltaTime);
        }
        else // Đóng cửa sau delay
        {
            if (closeTimer > 0)
            {
                closeTimer -= Time.deltaTime;
            }
            else
            {
                leftDoor.localPosition = Vector3.MoveTowards(leftDoor.localPosition, leftClosedPos, slideSpeed * Time.deltaTime);
                rightDoor.localPosition = Vector3.MoveTowards(rightDoor.localPosition, rightClosedPos, slideSpeed * Time.deltaTime);
            }
        }
    }
}