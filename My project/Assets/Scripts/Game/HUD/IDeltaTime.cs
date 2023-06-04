using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Core;

/// <summary>
/// DeltaTimeのインターフェース
/// Author : 出合翔太
/// </summary>
namespace Game
{
    public interface IDeltaTime : IFeature
    {        
        float Deltatime { get; } 
        IReactiveProperty<float> TimeScale { get; }
    }
}
