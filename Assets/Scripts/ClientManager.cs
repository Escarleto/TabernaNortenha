using UnityEngine;

public class ClientManager : MonoBehaviour
{
    [SerializeField] private Client[] ClientPrefabs;

    private void Start()
    {
        SpawnClient();
    }

    private void SpawnClient()
    {
        int RandomIndex = Random.Range(0, ClientPrefabs.Length);
        Instantiate(ClientPrefabs[RandomIndex], transform.position, Quaternion.identity);
    }
}
