using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private GameObject FoodPrefab;
    private Collider2D SpawnArea;
    private Camera Cam;

    private void Awake()
    {
        SpawnArea = GetComponent<Collider2D>();
        Cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && IsMouseOver())
            Spawn();
    }

    private bool IsMouseOver()
    {
        Vector3 Pos = Cam.ScreenToWorldPoint(Input.mousePosition);
        return SpawnArea.OverlapPoint(Pos);
    }

    private void Spawn()
    {
        Vector3 Pos = Cam.ScreenToWorldPoint(Input.mousePosition);
        Pos.z = 0;

        Instantiate(FoodPrefab, Pos, Quaternion.identity);
    }
}