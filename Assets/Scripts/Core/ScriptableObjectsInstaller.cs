using Gameplay.Levels;
using UnityEngine;
using Utils;
using Zenject;

[CreateAssetMenu(fileName = "ScriptableObjectsInstaller", menuName = "Installers/ScriptableObjectsInstaller")]
public class ScriptableObjectsInstaller : ScriptableObjectInstaller<ScriptableObjectsInstaller>
{
    [SerializeField] private StringPrefabPairContainer _prefabContainer;
    [SerializeField] private LevelsContainer _levelsContainer;
    public override void InstallBindings()
    {
        Container.BindInstance<StringPrefabPairContainer>(_prefabContainer);
        Container.BindInstance<LevelsContainer>(_levelsContainer);

    }
}