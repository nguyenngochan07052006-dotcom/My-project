using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SafeWithPassword : MonoBehaviour
{
    [Header("Mật khẩu")]
    public string correctPassword = "2006";

    [Header("Cài đặt")]
    public float interactionDistance = 3f;

    [Header("References")]
    public Transform player;
    public GameObject door;
    public GameObject keyInside;

    [Header("UI")]
    public GameObject passwordPanel;
    public TMP_InputField passwordInput;
    public Button btnXacNhan;
    public Button btnHuy;
    public TextMeshProUGUI promptText;    // "Nhấn [E] để mở tủ"

    [Header("Âm thanh")]
    public AudioClip openSound;
    public AudioClip wrongSound;

    private AudioSource audioSource;
    private bool isOpen = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();

        if (keyInside != null)
            keyInside.SetActive(false);

        if (btnXacNhan != null) btnXacNhan.onClick.AddListener(CheckPassword);
        if (btnHuy != null) btnHuy.onClick.AddListener(CloseUI);
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);
        bool nearSafe = distance <= interactionDistance;

        // Prompt chỉ hiện khi gần két sắt, chưa mở UI và chưa mở tủ
        if (promptText != null)
        {
            promptText.gameObject.SetActive(nearSafe && !passwordPanel.activeSelf && !isOpen);
        }

        // Nhấn E để mở bảng mật khẩu
        if (nearSafe && Input.GetKeyDown(KeyCode.E) && !isOpen && !passwordPanel.activeSelf)
        {
            OpenUI();
        }
    }

    private void OpenUI()
    {
        if (passwordPanel != null)
        {
            passwordPanel.SetActive(true);

            if (passwordInput != null)
            {
                passwordInput.text = "";
                passwordInput.Select();
                passwordInput.ActivateInputField();
            }
        }
    }

    public void CloseUI()
    {
        if (passwordPanel != null)
            passwordPanel.SetActive(false);
    }

    public void CheckPassword()
    {
        if (passwordInput == null) return;

        string input = passwordInput.text.Trim();

        if (input == correctPassword)
        {
            isOpen = true;
            Debug.Log("✅ Mật khẩu đúng! Cửa mở - Chìa khóa xuất hiện.");

            if (audioSource && openSound)
                audioSource.PlayOneShot(openSound);

            if (door != null)
                door.SetActive(false);

            if (keyInside != null)
            {
                keyInside.SetActive(true);
                KeyPickup keyScript = keyInside.GetComponent<KeyPickup>();
                if (keyScript != null)
                    keyScript.ActivateKey();
            }

            CloseUI();
        }
        else
        {
            Debug.Log("❌ Sai mật khẩu!");
            if (audioSource && wrongSound)
                audioSource.PlayOneShot(wrongSound);

            passwordInput.text = "";
        }
    }
}