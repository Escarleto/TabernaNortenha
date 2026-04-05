using UnityEngine;
using DG.Tweening;

public class Client : MonoBehaviour
{
    [SerializeField] private Sprite[] EmotionSprites;
    private int ChosenOrder;

    private void Start()
    {
        MoveClient(false);
        SetEmotion(0);
        UIManager.Instance.HasClient = true;
    }

    public void SetEmotion(int EmotionIndex)
    {
        GetComponent<SpriteRenderer>().sprite = EmotionSprites[EmotionIndex];
    }

    public void MoveClient(bool CanDelete)
    {
        if (CanDelete)
            transform.DOMoveX(transform.position.y - 20f, 0.5f).OnComplete(() => Destroy(gameObject));
        else
            transform.DOMoveX(transform.position.y + 0.1f, 0.5f).OnComplete(() => Order());
    }

    private void Order()
    {
        ChosenOrder = Random.Range(1, 4);
        Debug.Log(ChosenOrder);
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
        UIManager.Instance.HasClient = false;
        UIManager.Instance.UpdateMoney(MoneyToAdd);
    }
}

