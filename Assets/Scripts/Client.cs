using System.Collections;
using UnityEngine;

public class Client : MonoBehaviour
{
    [SerializeField] private Sprite[] ClientsSprites;

    private void Start()
    {
        NewClient();
    }

    private void NewClient()
    {
        Sprite NewClient = ClientsSprites[Random.Range(0, ClientsSprites.Length)];
        GetComponent<SpriteRenderer>().sprite = NewClient;
    }

    private void HideClient()
    {

    }
}
