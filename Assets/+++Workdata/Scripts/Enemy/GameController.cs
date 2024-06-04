using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public void WinGame()
    {
        Debug.Log("Win");
    }

    public void LooseGame()
    {
        SceneManager.LoadScene(0);
    }
}
