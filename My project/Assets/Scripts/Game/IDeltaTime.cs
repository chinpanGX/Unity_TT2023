using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Core;

/// <summary>
/// DeltaTimeのインターフェース
/// </summary>
namespace Game
{
    public interface IDeltaTime : IFeature
    {        
        float Deltatime { get; } 
        IReactiveProperty<float> TimeScale { get; }
    }
}
