using Gameplay;
using Gameplay.Field;
using Gameplay.Input;
using Zenject;

namespace Core
{
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<FieldObjectsController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<FieldObjectsSpawner>().AsSingle();
            Container.BindFactory<UnityEngine.Object, TowerElementMonoBehaviour, TowerElementMonoBehaviour.Factory>()
                .FromFactory<PrefabFactory<TowerElementMonoBehaviour>>();
            Container.BindInterfacesAndSelfTo<InputController>().AsSingle().NonLazy();
            Container.Bind<LevelManager>().AsSingle().NonLazy();
        }
    }
}