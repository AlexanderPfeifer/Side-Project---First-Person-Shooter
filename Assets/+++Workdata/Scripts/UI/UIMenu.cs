using UnityEngine;
using Eflatun.SceneReference;
using TMPro;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    [SerializeField] private Transform parentButtons;
    [SerializeField] private GameObject prefabButtonLevel;

    //adds every level of the game inside a scrollbar
    private void Start()
    {
        var i = 0;
        
        foreach (var level in GameController.Instance.scenesLevel)
        {
            var button = Instantiate(prefabButtonLevel, parentButtons).GetComponent<Button>();
            button.GetComponentInChildren<TextMeshProUGUI>().text = level.Name;
            var safeIndex = i;
            button.onClick.AddListener(() => GameController.Instance.LoadLevel(safeIndex));
            i++;
        }
    }

    //Starts first level 
    public void Play()
    {
        GameController.Instance.LoadNextLevel();
    }
}
