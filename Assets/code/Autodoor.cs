using UnityEngine;

public class AutoDoubleDoor : MonoBehaviour
{
    [Header("Door Settings")]
    public Transform leftDoor;      // Cánh cửa bên trái
    public Transform rightDoor;     // Cánh cửa bên phải
    public float openAngle = 90f;   // Góc mở cửa
    public float openSpeed = 3f;    // Tốc độ mở cửa

    private Quaternion leftClosedRot;
    private Quaternion rightClosedRot;
    private Quaternion leftOpenRot;
    private Quaternion rightOpenRot;

    private bool playerNear = false;

    void Start()
    {
        // Lưu góc ban đầu
        leftClosedRot = leftDoor.localRotation;
        rightClosedRot = rightDoor.localRotation;

        // Tính góc mở ra (trái mở ra ngoài, phải mở ra ngược hướng)
        leftOpenRot = Quaternion.Euler(leftDoor.localEulerAngles + new Vector3(0, -openAngle, 0));
        rightOpenRot = Quaternion.Euler(rightDoor.localEulerAngles + new Vector3(0, openAngle, 0));
    }

    void Update()
    {
        HandleDoorRotation();
    }

    private void HandleDoorRotation()
    {
        if (playerNear)
        {
            leftDoor.localRotation = Quaternion.Slerp(leftDoor.localRotation, leftOpenRot, Time.deltaTime * openSpeed);
            rightDoor.localRotation = Quaternion.Slerp(rightDoor.localRotation, rightOpenRot, Time.deltaTime * openSpeed);
        }
        else
        {
            leftDoor.localRotation = Quaternion.Slerp(leftDoor.localRotation, leftClosedRot, Time.deltaTime * openSpeed);
            rightDoor.localRotation = Quaternion.Slerp(rightDoor.localRotation, rightClosedRot, Time.deltaTime * openSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNear = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNear = false;
    }
}
