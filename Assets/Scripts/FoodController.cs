using UnityEngine;

public class FoodController : MonoBehaviour
{
    public enum FoodType { Bread, Filling, Topping, Sauce }
    public FoodType Type;
    [SerializeField] private bool IsVegetarian;

    private DishHandler CurrentDish;
    private DishHandler LastDish;

    private Animator Anim;
    private Camera Cam;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
        Cam = Camera.main;
    }

    private void Update()
    {
        Drag();

        if (Input.GetMouseButtonUp(0))
            TryPlace();
    }

    private void Drag()
    {
        if (!Input.GetMouseButton(0)) return;

        Vector3 Pos = Cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(Pos.x, Pos.y, 0);
    }

    private void TryPlace()
    {
        if (CurrentDish == null)
        {
            Destroy(gameObject);
            return;
        }

        bool Success = CurrentDish.BuildDish(this);

        if (Success)
        {
            if (!IsVegetarian)
                LastDish.IsVegetarianDish = false;
            transform.position = CurrentDish.transform.position;
            Anim.enabled = false;
            enabled = false;
        }
        else
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D Col)
    {
        if (!Col.CompareTag("Dish")) return;

        if (Col.TryGetComponent(out DishHandler Dish))
        {
            CurrentDish = Dish;
            LastDish = Dish;
        }
    }

    private void OnTriggerExit2D(Collider2D Col)
    {
        if (!Col.CompareTag("Dish")) return;

        if (CurrentDish != null)
            CurrentDish = null;
    }
}