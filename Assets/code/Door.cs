using UnityEngine;

public class Door : MonoBehaviour
{
    public float openAngle = -90f;
    public float speed = 2f;

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
        if (Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
        }

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