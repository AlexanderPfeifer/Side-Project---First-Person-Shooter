using UnityEngine;
using UnityEngine.UI;

public class UILevel : MonoBehaviour
{
    [Header("PanelStart")]
    [SerializeField] private CanvasGroup panelStart;
    [SerializeField] private Button buttonStartLevel;

    [Header("PanelWin")]
    [SerializeField] private CanvasGroup panelWin;
    [SerializeField] private Button buttonNextLevel;
    [SerializeField] private Button buttonBackToMainMenu;
    [SerializeField] private Button buttonAgain;

    [Header("PanelLoose")]
    [SerializeField] private CanvasGroup panelLoose;
    [SerializeField] private Button buttonAgain1;
    [SerializeField] private Button buttonBackToMainMenu1;
    
    [Header("PanelPause")]
    [SerializeField] private CanvasGroup panelPause;
    [SerializeField] private Button buttonContinue;
    [SerializeField] private Button buttonBackToMainMenu2;
    
    //Adds all methods according to the buttons
    void Start()
    {
        Time.timeScale = 0;
        panelStart.ShowCanvasGroup();
        
        panelWin.HideCanvasGroup();
        panelLoose.HideCanvasGroup();
        panelPause.HideCanvasGroup();
        
        buttonStartLevel.onClick.AddListener(() =>
        {
            panelStart.HideCanvasGroup();
            GameController.Instance.StartLevel();
        });
        
        buttonAgain.onClick.AddListener(GameController.Instance.ReloadLevel);
        buttonAgain1.onClick.AddListener(GameController.Instance.ReloadLevel);
        buttonBackToMainMenu.onClick.AddListener(GameController.Instance.LoadMenu);
        buttonBackToMainMenu1.onClick.AddListener(GameController.Instance.LoadMenu);
        buttonNextLevel.onClick.AddListener(GameController.Instance.LoadNextLevel);
        buttonContinue.onClick.AddListener(ShowPauseScreen);
        buttonBackToMainMenu2.onClick.AddListener(GameController.Instance.LoadMenu);
    }

    //Show pause screen and when pause screen is active, then pause screen deactivates again
    public void ShowPauseScreen()
    {
        if(!panelStart.interactable)
            return;
        
        if (!panelPause.interactable)
        {
            GameController.Instance.TimeAndCursorLock(0, true, CursorLockMode.None);
            Debug.Log("hallo");
            panelPause.ShowCanvasGroup();
        }
        else
        {
            GameController.Instance.TimeAndCursorLock(1, false, CursorLockMode.Locked);
            panelPause.HideCanvasGroup();
        }
    }
    
    //shows the win screen
    public void ShowWinScreen()
    {
        panelWin.ShowCanvasGroup();
    }

    //Shows the loose screen
    public void ShowLoseScreen()
    {
        panelLoose.ShowCanvasGroup();
    }
}

public static class ExtensionMethods
{
    //Extension for hiding a canvas
    public static void HideCanvasGroup(this CanvasGroup myCanvasGroup)
    {
        myCanvasGroup.alpha = 0;
        myCanvasGroup.interactable = false;
        myCanvasGroup.blocksRaycasts = false;
    }
    
    //Extension for showing a canvas
    public static void ShowCanvasGroup(this CanvasGroup myCanvasGroup)
    {
        myCanvasGroup.alpha = 1;
        myCanvasGroup.interactable = true;
        myCanvasGroup.blocksRaycasts = true;
    }
}
