using System.ComponentModel;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private CharacterController playerPrefab;
    [SerializeField] private Transform playerSpawnPoint;

    [SerializeField] private UIInputService inputService;

    public override void InstallBindings()
    {
        Container.Bind<IInputService>().To<UIInputService>().AsSingle();

        CharacterController player = Container.InstantiatePrefabForComponent<CharacterController>(playerPrefab, playerSpawnPoint.position, Quaternion.identity, null);
        Container.Bind<CharacterController>().FromInstance(player).AsSingle();
    }
}
