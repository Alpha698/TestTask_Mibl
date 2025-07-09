using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;

public class ControlPanelView : MonoBehaviour
{
    [Inject] private IInputService input;
    [Inject] private UIScreenManager screenManager;

    [SerializeField] private TextMeshProUGUI bulletLabel;

    [SerializeField] private Button pauseButton;
    [SerializeField] private Button shootButton;
    [SerializeField] private Button jumpButton;

    private void Start()
    {
        pauseButton.onClick.AddListener(OnPauseButtonClicked);
        shootButton.onClick.AddListener(OnShoot);
        jumpButton.onClick.AddListener(OnJump);

    }

    private void OnPauseButtonClicked()
    {
        screenManager.ShowPauseScreen(true);
        Time.timeScale = 0f; 
    }

    public void OnLeftDown() => input.SetLeft(true);
    public void OnLeftUp() => input.SetLeft(false);

    public void OnRightDown() => input.SetRight(true);
    public void OnRightUp() => input.SetRight(false);

    public void OnJump() => input.Jump();

    public void OnShoot() => input.Shoot();

}
