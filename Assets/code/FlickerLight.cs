using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    private Light myLight;

    void Start()
    {
        myLight = GetComponent<Light>();
    }

    void Update()
    {
        myLight.enabled = Mathf.Sin(Time.time * 10f) > 0;
    }
}