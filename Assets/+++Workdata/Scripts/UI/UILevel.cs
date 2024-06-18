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

    public void ShowPauseScreen()
    {
        if(!panelStart.interactable)
            return;
        
        if (!panelPause.interactable)
        {
            GameController.Instance.TimeAndCursorLock(0, true, CursorLockMode.None);
            panelPause.ShowCanvasGroup();
        }
        else
        {
            GameController.Instance.TimeAndCursorLock(1, false, CursorLockMode.Locked);
            panelPause.HideCanvasGroup();
        }
    }
    
    public void ShowWinScreen()
    {
        panelWin.ShowCanvasGroup();
    }

    public void ShowLoseScreen()
    {
        panelLoose.ShowCanvasGroup();
    }
}

public static class ExtensionMethods
{
    public static void HideCanvasGroup(this CanvasGroup myCanvasGroup)
    {
        myCanvasGroup.alpha = 0;
        myCanvasGroup.interactable = false;
        myCanvasGroup.blocksRaycasts = false;
    }
    
    public static void ShowCanvasGroup(this CanvasGroup myCanvasGroup)
    {
        myCanvasGroup.alpha = 1;
        myCanvasGroup.interactable = true;
        myCanvasGroup.blocksRaycasts = true;
    }
}
