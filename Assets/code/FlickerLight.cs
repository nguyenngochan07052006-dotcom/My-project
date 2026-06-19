using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    Light myLight;

    void Start()
    {
        myLight = GetComponent<Light>();
    }

    void Update()
    {
        myLight.enabled = Mathf.Sin(Time.time * 10) > 0;
    }
}