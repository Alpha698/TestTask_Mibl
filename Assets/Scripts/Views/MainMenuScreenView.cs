using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenuScreenView : MonoBehaviour
{
    [Inject] private UIScreenManager screenManager;
    [Inject] private GameManager gameManager;

    [SerializeField] private Button startButton;

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {

        screenManager.ShowMainMenuScreen(false);
        screenManager.ShowControlPanel(true);

        gameManager.Installer.LoadLevel(); // Instantiate level
    }

}
