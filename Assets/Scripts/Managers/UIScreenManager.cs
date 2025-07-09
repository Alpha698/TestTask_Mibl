using UnityEngine;

public class UIScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuScreen, controlPanel, pauseScreen, gameOverScreen, winScreen;


    private void Start()
    {
        ShowMainMenuScreen(true);
    }

    public void ShowMainMenuScreen(bool show) => mainMenuScreen.SetActive(show);
    public void ShowControlPanel(bool show) => controlPanel.SetActive(show);
    public void ShowPauseScreen(bool show) => pauseScreen.SetActive(show);
    public void ShowGameOverScreen(bool show) => gameOverScreen.SetActive(show);
    public void ShowWinScreen(bool show) => winScreen.SetActive(show);


    // if we want use CanvasGroup instead of GameObject.SetActive

    //public void ShowMainMenuScreen(bool show)
    //{
    //    mainMenuScreen.SetActive(show);
    //    CanvasGroup cg = mainMenuScreen.GetComponent<CanvasGroup>();
    //    cg.alpha = show ? 1f : 0f;
    //    cg.interactable = show ? true : false;
    //    cg.blocksRaycasts = show ? true : false;
    //}


}
