using UnityEngine;
using DG.Tweening;

public class ChangeArea : MonoBehaviour
{
    [SerializeField] private bool ToKitchen = true;

    private void OnMouseDown()
    {
        if (ToKitchen)
            Camera.main.transform.DOMoveY(Camera.main.transform.position.y - 10.88f, 0.5f);
        else
            Camera.main.transform.DOMoveY(Camera.main.transform.position.y + 10.88f, 0.5f);
    }
}
