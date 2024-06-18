using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { private set; get; }

    [SerializeField] public SceneReference sceneMenu;

    [SerializeField] public SceneReference[] scenesLevel;

    public int currentSceneIndex;
    
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

    //Starts the level when clicking on start in the starting panel
    public void StartLevel()
    {
        TimeAndCursorLock(1, false, CursorLockMode.Locked);
    }
    
    //Enables win panel
    public void WinGame()
    { 
        FindObjectOfType<UILevel>().ShowWinScreen();
        TimeAndCursorLock(0, true, CursorLockMode.None);
    }

    //Enables Loose panel
    public void LooseGame()
    {
        FindObjectOfType<UILevel>().ShowLoseScreen();
        TimeAndCursorLock(0, true, CursorLockMode.None);
    }

    //Shortcut function for freezing or unfreezing time and cursor
    public void TimeAndCursorLock(int timeScale, bool cursorLockState, CursorLockMode cursorLockMode)
    {
        Time.timeScale = timeScale;
        Cursor.visible = cursorLockState;
        Cursor.lockState = cursorLockMode;
    }

    //Reloads the current scene the player is in
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Loads the main menu
    public void LoadMenu()
    {
        SceneManager.LoadScene(sceneMenu.BuildIndex);
    }

    //Loads the level depending on the button in the level list in the main menu
    public void LoadLevel(int listIndex)
    {
        SceneManager.LoadScene(scenesLevel[listIndex].BuildIndex);
    }

    //Loads the next level of the scene level list
    public void LoadNextLevel()
    {
        var sceneToLoad = currentSceneIndex;
        
        SceneManager.LoadScene(sceneToLoad < scenesLevel.Length
            ? scenesLevel[sceneToLoad].BuildIndex
            : sceneMenu.BuildIndex);

        currentSceneIndex++;
    }
}
