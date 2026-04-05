using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeArea : MonoBehaviour
{
    [SerializeField] private UnityEditor.SceneAsset ToScene;
    private string SceneName;

    private void Start()
    {
        SceneName = ToScene.name;
    }

    private void OnMouseDown()
    {
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }
}
