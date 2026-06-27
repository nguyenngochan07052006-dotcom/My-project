using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Cài đặt cửa")]
    public float openAngle = -90f;        // Góc mở cửa
    public float speed = 3f;              // Tốc độ mở cửa
    public float openDistance = 3f;       // Khoảng cách tương tác
    public Transform player;              // Kéo Player vào đây

    [Header("Âm thanh")]
    public AudioClip openSound;
    public AudioClip closeSound;

    private bool isOpen = false;
    private bool showText = false;
    private Quaternion closedRot;
    private Quaternion openRot;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

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

        float distance = Vector3.Distance(player.position, transform.position);

        // Kiểm tra khoảng cách
        if (distance <= openDistance)
        {
            showText = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                isOpen = !isOpen;

                // Phát âm thanh
                if (audioSource != null)
                {
                    audioSource.clip = isOpen ? openSound : closeSound;
                    audioSource.Play();
                }
            }
        }
        else
        {
            showText = false;
        }

        // Mở / Đóng cửa mượt mà
        Quaternion targetRot = isOpen ? openRot : closedRot;
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRot,
            Time.deltaTime * speed
        );
    }

    void OnGUI()
    {
        if (showText)
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.fontSize = 28;
            style.normal.textColor = Color.white;
            style.alignment = TextAnchor.MiddleCenter;
            style.fontStyle = FontStyle.Bold;

            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height - 120, 300, 50),
                      "Nhấn [E] để " + (isOpen ? "đóng" : "mở") + " cửa", style);
        }
    }
}