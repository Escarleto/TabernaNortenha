using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private GameObject FoodPrefab;
    private GameObject CurrentFood;
    private Collider2D SpawnerCollider;

    private void Start()
    {
        SpawnerCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && IsMouseOverSpawner())
            SpawnFood();

        if (CurrentFood != null)
            FollowMouse();

        if (Input.GetMouseButtonUp(0) && CurrentFood != null)
            DestroyFood();
    }

    private bool IsMouseOverSpawner()
    {
        Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return SpawnerCollider.OverlapPoint(MousePos);
    }

    private void SpawnFood()
    {
        if (CurrentFood != null) return;

        Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CurrentFood = Instantiate(FoodPrefab, MousePos, Quaternion.identity);
    }

    private void FollowMouse()
    {
        Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CurrentFood.transform.position = MousePos;
    }

    private void DestroyFood()
    {
        Destroy(CurrentFood);
        CurrentFood = null;
    }
}