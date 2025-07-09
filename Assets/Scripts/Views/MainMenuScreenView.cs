using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenuScreenView : MonoBehaviour
{
    [Inject] private UIScreenManager screenManager;

    [SerializeField] private Button startButton;

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        screenManager.ShowMainMenuScreen(false);
        screenManager.ShowControlPanel(true);
    }
}
