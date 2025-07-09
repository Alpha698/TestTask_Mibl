using System.ComponentModel;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private LevelConfig levelConfig;

    [SerializeField] private UIInputService inputService;

    private GameObject levelInstance;

    public override void InstallBindings()
    {
        Container.Bind<IInputService>().To<UIInputService>().AsSingle();

        Container.Bind<GameManager>().AsSingle().OnInstantiated<GameManager>((ctx, gm) =>
        {
            gm.Installer = this;
        });

    }
    public void LoadLevel()
    {

        levelInstance = Container.InstantiatePrefab(levelConfig.levelPrefab);

        var levelController = levelInstance.GetComponent<LevelController>();

        var player = levelController.SpawnPlayer();
        Container.Bind<CharacterController>().FromInstance(player).AsTransient();
        levelController.SetPlayer(player);


        Container.Bind<LevelController>().FromInstance(levelController).AsTransient();
        levelController.SpawnEnemies();

    }

    public void UnloadLevel()
    {
        if (levelInstance != null)
            Destroy(levelInstance);
    }

}
