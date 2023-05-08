using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
