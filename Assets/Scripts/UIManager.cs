using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set; }
    public int AttentedClients = -1;
    public int Day;
    public int Money = 0;
    private Label DayUI;
    private Label Timer;
    private Label MoneyUI;
    private int TimerSeconds = 200;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        DayUI = GetComponent<UIDocument>().rootVisualElement.Q<Label>("Day");
        Timer = GetComponent<UIDocument>().rootVisualElement.Q<Label>("Timer");
        Timer.text = TimerSeconds.ToString();
        MoneyUI = GetComponent<UIDocument>().rootVisualElement.Q<Label>("Money");
        Day = 1;
        StartCoroutine(StartDay());
    }

    public void UpdateMoney(int AddMoney)
    {
        Money += AddMoney;
        MoneyUI.text = Money.ToString();
    }

    private IEnumerator StartDay()
    {
        DayUI.text = Day.ToString();
        while (TimerSeconds > 0)
        {
            Timer.text = TimerSeconds.ToString();
            yield return new WaitForSeconds(1);
            TimerSeconds--;
        }
    }
}
