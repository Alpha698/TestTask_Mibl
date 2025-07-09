using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;

public class PauseScreenView : MonoBehaviour
{
    [Inject] private UIScreenManager screenManager;
    [Inject] private GameManager gameManager;

    [SerializeField] private TextMeshProUGUI levelLabel;

    [SerializeField] private Button closeButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        closeButton.onClick.AddListener(ContinueGame);
        continueButton.onClick.AddListener(ContinueGame);
        quitButton.onClick.AddListener(ExitToMenu);
    }

    private void ContinueGame()
    {
        screenManager.ShowPauseScreen(false);
        Time.timeScale = 1f;
    }

    public void ExitToMenu()
    {
        screenManager.ShowMainMenuScreen(true);
        screenManager.ShowControlPanel(false);
        screenManager.ShowPauseScreen(false);

        gameManager.Installer.UnloadLevel(); // Destroy level
        Time.timeScale = 1f;
    }




}
