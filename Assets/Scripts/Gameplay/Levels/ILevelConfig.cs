using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelConfig
{
    IEnumerable GetPositions();
    float SmallR { get; }
    float BigR { get; }
    float Height { get; }
}