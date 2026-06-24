using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [Header("--- Cấu hình Đèn Pin ---")]
    [Tooltip("Kéo Object chứa ánh sáng Spotlight hoặc toàn bộ cụm đèn pin vào đây")]
    public GameObject flashlightObject;

    [Tooltip("Phím tắt để bật tắt đèn pin")]
    public KeyCode toggleKey = KeyCode.F;

    [Tooltip("Âm thanh bật/tắt đèn pin (Nếu có)")]
    public AudioClip toggleSound;

    private bool isSoundEnabled = false;
    private AudioSource audioSource;
    private bool isOn = false;

    void Start()
    {
        // Khởi tạo hệ thống âm thanh nếu bạn có bỏ file âm thanh vào
        if (toggleSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = toggleSound;
            audioSource.playOnAwake = false;
            isSoundEnabled = true;
        }

        // Mới vào game thì tắt đèn pin đi cho đúng chất kinh dị
        if (flashlightObject != null)
        {
            flashlightObject.SetActive(isOn);
        }
        else
        {
            Debug.LogWarning("Bạn chưa kéo Đèn pin vào ô Flashlight Object ở bảng Inspector kìa!");
        }
    }

    void Update()
    {
        // Kiểm tra xem người chơi có bấm nút F (hoặc nút bạn cài) không
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleFlashlight();
        }
    }

    // Hàm xử lý việc bật / tắt
    void ToggleFlashlight()
    {
        if (flashlightObject == null) return;

        // Đảo trạng thái (Nếu đang bật -> tắt, đang tắt -> bật)
        isOn = !isOn; 

        // Áp dụng trạng thái mới cho đèn pin
        flashlightObject.SetActive(isOn);

        // Phát âm thanh "Click" nếu có
        if (isSoundEnabled && audioSource != null)
        {
            audioSource.Play();
        }
    }
}