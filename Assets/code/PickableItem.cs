using UnityEngine;

public class PickableItem : MonoBehaviour
{
    public bool isRed;
    public void PickUp()
    {
        gameObject.SetActive(false);
    }
}