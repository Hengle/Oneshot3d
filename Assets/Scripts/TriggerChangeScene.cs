
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerChangeScene : MonoBehaviour
{
    public string scene;
    void OnTriggerEnter(Collider Collider)
    {
        SceneManager.LoadScene(scene);
    }
}