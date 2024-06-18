using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { private set; get; }

    [SerializeField] public SceneReference sceneMenu;

    [SerializeField] public SceneReference[] scenesLevel;

    private int currentSceneIndex;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        
        DontDestroyOnLoad(this);
    }

    public void StartLevel()
    {
        TimeAndCursorLock(1, false, CursorLockMode.Locked);
    }
    
    public void WinGame()
    { 
        FindObjectOfType<UILevel>().ShowWinScreen();
        TimeAndCursorLock(0, true, CursorLockMode.None);
    }

    public void LooseGame()
    {
        FindObjectOfType<UILevel>().ShowLoseScreen();
        TimeAndCursorLock(0, true, CursorLockMode.None);
    }

    public void TimeAndCursorLock(int timeScale, bool cursorLockState, CursorLockMode cursorLockMode)
    {
        Time.timeScale = timeScale;
        Cursor.visible = cursorLockState;
        Cursor.lockState = cursorLockMode;
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(sceneMenu.BuildIndex);
    }

    public void LoadLevel(int listIndex)
    {
        SceneManager.LoadScene(scenesLevel[listIndex].BuildIndex);
    }

    public void LoadNextLevel()
    {
        var sceneToLoad = currentSceneIndex;
        
        SceneManager.LoadScene(sceneToLoad < scenesLevel.Length
            ? scenesLevel[sceneToLoad].BuildIndex
            : sceneMenu.BuildIndex);
    }
}
