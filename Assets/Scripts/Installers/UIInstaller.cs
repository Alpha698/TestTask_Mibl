using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private UIScreenManager screenManager;

    public override void InstallBindings()
    {
        Container.Bind<UIScreenManager>().FromInstance(screenManager).AsSingle();
    }
}