using UnityEngine;

public class FoodController : MonoBehaviour
{
    public enum FoodType { Bread, Filling, Topping, Sauce }
    [SerializeField] private FoodType Type; 
    [SerializeField] private bool IsVegetarian;
    private DishHandler Dish;
    private Animator Animator;

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Debug.Log("Dish: " + Dish + " | Resultado: " + Dish?.BuildDish(Type));
        if (Input.GetMouseButtonUp(0))
        {
            if (Dish != null && Dish.BuildDish(Type))
            {
                transform.position = Dish.gameObject.transform.position;
                Animator.enabled = false;
                enabled = false;
            }
            else
                Destroy(gameObject);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(MousePos.x, MousePos.y, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dish"))
            Dish = collision.TryGetComponent<DishHandler>(out var dish) ? dish : null;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Dish") && Dish != null) 
            Dish = null; 
    }
}