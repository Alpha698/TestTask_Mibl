using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;

public class GameOverScreenView : MonoBehaviour
{
    [Inject] private UIScreenManager screenManager;
    [Inject] private GameManager gameManager;

    [SerializeField] private TextMeshProUGUI levelLabel;

    [SerializeField] private Button closeButton;
    [SerializeField] private Button replayButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        closeButton.onClick.AddListener(ExitToMenu);
        replayButton.onClick.AddListener(ReplayGame);
        quitButton.onClick.AddListener(ExitToMenu);
    }

    private void ReplayGame()
    {
        screenManager.ShowGameOverScreen(false);

        gameManager.Installer.UnloadLevel(); // Destroy level
        gameManager.Installer.LoadLevel(); // Instantiate level
        Time.timeScale = 1f;
    }

    public void ExitToMenu()
    {
        screenManager.ShowMainMenuScreen(true);
        screenManager.ShowControlPanel(false);
        screenManager.ShowGameOverScreen(false);

        gameManager.Installer.UnloadLevel(); // Destroy level
        Time.timeScale = 1f;
    }

}
