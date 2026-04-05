using UnityEngine;
using DG.Tweening;

public class Client : MonoBehaviour
{
    [SerializeField] private Sprite[] EmotionSprites;
    private int ChosenOrder;

    private void Start()
    {
        MoveClient(1, false);
        SetEmotion(0);
    }

    public void SetEmotion(int EmotionIndex)
    {
        GetComponent<SpriteRenderer>().sprite = EmotionSprites[EmotionIndex];
    }

    public void MoveClient(int Direction, bool CanDelete)
    {
        if (CanDelete)
            transform.DOMoveX(transform.position.y - (0.1f * Direction), 0.5f).OnComplete(() => Destroy(gameObject));
        else
            transform.DOMoveX(transform.position.y + (0.1f * Direction), 0.5f).OnComplete(() => Order());
    }

    private void Order()
    {
        ChosenOrder = Random.Range(0, 4);
    }

    public void ReceiveOrder(int ReceivedOrder)
    {
        int MoneyToAdd = 0;
        if (ReceivedOrder == ChosenOrder)
        {
            MoneyToAdd = 10;
            SetEmotion(2);
        }
        else
        {
            MoneyToAdd = 2;
            SetEmotion(3);
        }

        UIManager.Instance.AttentedClients++;
        UIManager.Instance.UpdateMoney(MoneyToAdd);
    }
}

