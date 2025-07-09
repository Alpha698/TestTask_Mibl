using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PauseScreenView : MonoBehaviour
{
    [Inject] private UIScreenManager screenManager;

    [SerializeField] private Button closeButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        closeButton.onClick.AddListener(ResumeGame);
    }

    private void ResumeGame()
    {
        screenManager.ShowPauseScreen(false);
        Time.timeScale = 1f;
    }


}
