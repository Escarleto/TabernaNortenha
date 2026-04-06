using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndButtons : MonoBehaviour
{
    public void OnReplay()
    {
        SceneManager.LoadScene("Balcony");
    }

    public void OnQuit()
    {
        Application.Quit(); 
    }
}
