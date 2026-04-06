using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set; }
    [SerializeField] private ClientManager ClientManager;
    [SerializeField] private GameObject[] Receits;
    public int AttentedClients = -1;
    public int Day;
    public int Money = 0;
    private Label DayUI;
    private Label Timer;
    private Label MoneyUI;
    private Label ClientsUI;
    private int TimerSeconds = 200;
    public Dish CurrentDish;
    public bool HasClient;

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
        ClientsUI = GetComponent<UIDocument>().rootVisualElement.Q<Label>("Clients");
        Day = 1;
        StartCoroutine(StartDay());
    }

    public void UpdateMoney(int AddMoney)
    {
        Money += AddMoney;
        MoneyUI.text = $"{Money}";
        ClientsUI.text = $"{AttentedClients}";
    }

    public void GiveOrder()
    {
        ClientManager.CheckOrder(CurrentDish.DishID);
        CurrentDish = null;
    }

    public void ShowReceit(int Receit, bool Show)
    {
        Receits[Receit].SetActive(Show);
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
        SceneManager.LoadScene("EndScreen");
    }
}
