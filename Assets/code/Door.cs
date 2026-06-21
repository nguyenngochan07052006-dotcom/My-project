using UnityEngine;

public class Door : MonoBehaviour
{
    public float openAngle = -90f;
    public float speed = 2f;

    // Khoảng cách mở cửa
    public float openDistance = 3f;

    // Kéo Player vào đây trong Inspector
    public Transform player;

    private bool isOpen = false;
    private Quaternion closedRot;
    private Quaternion openRot;

    void Start()
    {
        closedRot = transform.rotation;

        openRot = Quaternion.Euler(
            transform.eulerAngles.x,
            transform.eulerAngles.y + openAngle,
            transform.eulerAngles.z
        );
    }

    void Update()
    {
        // Kiểm tra khoảng cách
        float distance = Vector3.Distance(
            player.position,
            transform.position
        );

        // Chỉ mở khi ở gần
        if (distance <= openDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isOpen = !isOpen;
            }
        }

        // Xoay cửa
        if (isOpen)
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                openRot,
                Time.deltaTime * speed
            );
        }
        else
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                closedRot,
                Time.deltaTime * speed
            );
        }
    }
}