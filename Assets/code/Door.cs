using UnityEngine;

public class Door : MonoBehaviour
{
    public float openAngle = -90f;   // Mở vào trong, bản lề trái
    public float speed = 3f;
    public float interactDistance = 3f;

    private bool isOpen = false;
    private Quaternion closedRot;
    private Quaternion openRot;
    private Transform player;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");

        if (p != null)
            player = p.transform;

        closedRot = transform.rotation;

        openRot = Quaternion.Euler(
            transform.eulerAngles.x,
            transform.eulerAngles.y + openAngle,
            transform.eulerAngles.z
        );
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(player.position, transform.position);

        if (dist <= interactDistance && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
        }

        Quaternion target = isOpen ? openRot : closedRot;

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            target,
            Time.deltaTime * speed
        );
    }
}