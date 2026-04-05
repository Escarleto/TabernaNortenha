using UnityEngine;
using DG.Tweening;

public class ChangeArea : MonoBehaviour
{
    [SerializeField] private DishHandler Plate;
    [SerializeField] private bool IsKitchen;

    private void OnMouseDown()
    {   
        if (!UIManager.Instance.HasClient) return;
        
        int Direction = IsKitchen ? 1 : -1;
        
        if (IsKitchen)
        {
            if (UIManager.Instance.CurrentDish == null) return;
            Plate.ResetDish();
            UIManager.Instance.GiveOrder();
        }
        Camera.main.transform.DOMoveY(Camera.main.transform.position.y + 10.88f * Direction, 0.5f);
    }
}
