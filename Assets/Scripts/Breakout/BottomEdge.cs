using UnityEngine;
using UnityEngine.SceneManagement;

public class BottomEdge : MonoBehaviour
{
    #region private fields



#   endregion

    void OnCollisionEnter2D()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(currentScene.buildIndex);
    }
}