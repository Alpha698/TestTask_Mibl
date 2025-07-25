﻿using UnityEngine;
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

    int maxbulletCount = 55;
    int currentbulletCount;

    private void Start()
    {
        pauseButton.onClick.AddListener(OnPauseButtonClicked);
        shootButton.onClick.AddListener(OnShoot);
        jumpButton.onClick.AddListener(OnJump);


        currentbulletCount = maxbulletCount;
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

    public void OnShoot() 
    {

        if (currentbulletCount != 0)
        {
            currentbulletCount--;
            bulletLabel.text = currentbulletCount + "/" + maxbulletCount;
            input.Shoot();
        }
        else
        {
            bulletLabel.text = currentbulletCount + "/" + maxbulletCount;
        }
    }

}
