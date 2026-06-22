using UnityEngine;

public class DoubleDoor : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;

    public float openAngle = 90f;
    public float speed = 2f;

    bool isOpen;

    Quaternion leftClosed;
    Quaternion rightClosed;

    Quaternion leftOpen;
    Quaternion rightOpen;

    void Start()
    {
        leftClosed = leftDoor.localRotation;
        rightClosed = rightDoor.localRotation;

        leftOpen = Quaternion.Euler(0, -openAngle, 0) * leftClosed;
        rightOpen = Quaternion.Euler(0, openAngle, 0) * rightClosed;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
        }

        if (isOpen)
        {
            leftDoor.localRotation =
                Quaternion.Lerp(leftDoor.localRotation, leftOpen, Time.deltaTime * speed);

            rightDoor.localRotation =
                Quaternion.Lerp(rightDoor.localRotation, rightOpen, Time.deltaTime * speed);
        }
        else
        {
            leftDoor.localRotation =
                Quaternion.Lerp(leftDoor.localRotation, leftClosed, Time.deltaTime * speed);

            rightDoor.localRotation =
                Quaternion.Lerp(rightDoor.localRotation, rightClosed, Time.deltaTime * speed);
        }
    }
}