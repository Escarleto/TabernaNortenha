using UnityEngine;
using DG.Tweening;

public class Client : MonoBehaviour
{
    [SerializeField] private Sprite[] EmotionSprites;
    private int ChosenOrder;
    [SerializeField] private GameObject OrderSpeech;
    private int CurrentOrder;

    private void Start()
    {
        MoveClient(false);
        SetEmotion(0);
        UIManager.Instance.HasClient = true;
        OrderSpeech.SetActive(false);
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
            transform.DOMoveX(transform.position.y + 0.1f, 0.5f).SetDelay(Random.Range(0.5f, 1.5f)).OnComplete(() => Order());
    }

    private void Order()
    {
        ChosenOrder = Random.Range(1, 4);
        CurrentOrder = ChosenOrder;
        SetEmotion(1);
        OrderSpeech.SetActive(true);
        UIManager.Instance.ShowReceit(ChosenOrder -1, true);

        switch (ChosenOrder)
        {
            case 1:
                ActivateOrderSpeechChild("Order1");
                return;
            case 2:
                ActivateOrderSpeechChild("Order2");
                return;
            case 3:
                ActivateOrderSpeechChild("Order3");
                return;
        }
    }

    private void ActivateOrderSpeechChild(string childName)
    {
        if (OrderSpeech == null)
            return;

        bool Found = false;
        for (int i = 0; i < OrderSpeech.transform.childCount; i++)
        {
            GameObject child = OrderSpeech.transform.GetChild(i).gameObject;
            bool shouldActivate = child.name == childName;
            child.SetActive(shouldActivate);
            if (shouldActivate)
                Found = true;
        }

        if (!Found)
            Debug.LogWarning($"OrderSpeech child not Found: {childName}");
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
        
        OrderSpeech.SetActive(false);
        UIManager.Instance.ShowReceit(CurrentOrder - 1, false);
        CurrentOrder = 0;
        UIManager.Instance.AttentedClients++;
        UIManager.Instance.HasClient = false;
        UIManager.Instance.UpdateMoney(MoneyToAdd);
    }
}

