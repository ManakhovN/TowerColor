using System.Collections;
using UnityEngine;

public class LevelBase : ScriptableObject, ILevelConfig
{
    [SerializeField]
    protected float _bigR;
    [SerializeField]
    protected float _smallR;
    [SerializeField]
    protected float _height;

    public float SmallR => _smallR;
    public float BigR => _bigR;
    public float Height => _height;
    public virtual IEnumerable GetPositions()
    {
        yield return null;
    }
}
