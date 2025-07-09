using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WinScreenView : MonoBehaviour
{
    [Inject] private UIScreenManager screenManager;
    [Inject] private GameManager gameManager;

    [SerializeField] private Button nextButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        nextButton.onClick.AddListener(ReplayGame);
        quitButton.onClick.AddListener(ExitToMenu);
    }

    private void ReplayGame()
    {
        screenManager.ShowWinScreen(false);

        gameManager.Installer.UnloadLevel(); // Destroy level
        gameManager.Installer.LoadLevel(); // Instantiate level
        Time.timeScale = 1f;
    }

    public void ExitToMenu()
    {
        screenManager.ShowMainMenuScreen(true);
        screenManager.ShowControlPanel(false);
        screenManager.ShowWinScreen(false);

        gameManager.Installer.UnloadLevel(); // Destroy level
        Time.timeScale = 1f;
    }


}
