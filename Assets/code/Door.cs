using UnityEngine;

public class Door : MonoBehaviour
{
    public float openAngle = -90f;
    public float speed = 2f;

    public float openDistance = 3f;

    public Transform player;

    private bool isOpen = false;
    private bool showText = false;

    private Quaternion closedRot;
    private Quaternion openRot;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

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

        float distance = Vector3.Distance(
            player.position,
            transform.position
        );

        if (distance <= openDistance)
        {
            showText = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                isOpen = !isOpen;

                // Phát âm thanh khi mở hoặc đóng
                if (audioSource != null)
                {
                    audioSource.Play();
                }
            }
        }
        else
        {
            showText = false;
        }

        Quaternion target = isOpen ? openRot : closedRot;

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            target,
            Time.deltaTime * speed
        );
    }

    void OnGUI()
    {
        if (showText)
        {
            GUIStyle style = new GUIStyle();

            style.fontSize = 24;
            style.normal.textColor = Color.white;
            style.alignment = TextAnchor.MiddleCenter;

            GUI.Label(
                new Rect(
                    Screen.width / 2 - 120,
                    Screen.height - 100,
                    240,
                    40
                ),
                "Bấm [E] để mở cửa",
                style
            );
        }
    }
}