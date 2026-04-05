using System.Collections;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    [SerializeField] private Client[] ClientPrefabs;
    private Client CurrentClient;

    private void Start()
    {
        SpawnClient();
    }

    private void SpawnClient()
    {
        if (ClientPrefabs == null || ClientPrefabs.Length == 0) return;
        if (CurrentClient != null) return;

        int RandomIndex = Random.Range(0, ClientPrefabs.Length);
        CurrentClient = Instantiate(ClientPrefabs[RandomIndex], transform.position, Quaternion.identity).GetComponent<Client>();
    }

    private IEnumerator SpawnClientWithDelay()
    {
        float Delay = Random.Range(1f, 5f);
        
        yield return new WaitForSeconds(1f);
        CurrentClient.MoveClient(true);
        CurrentClient = null;
        
        yield return new WaitForSeconds(Delay);
        SpawnClient();
    }

    public void CheckOrder(int DishID)
    {
        if (CurrentClient == null) return;
        CurrentClient.ReceiveOrder(DishID);
        StartCoroutine(SpawnClientWithDelay());
    }
}