
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setscene : MonoBehaviour
{
    public string scene;
    public void NextScene()
    {
        SceneManager.LoadScene(scene);
    }
}