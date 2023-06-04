using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Core;

/// <summary>
/// ゲームの判定を行うインターフェース
/// Author : 出合翔太
/// </summary>
namespace Manager
{
    public interface IGameJudge : IFeature
    {
        // 残り時間
        IReadOnlyReactiveProperty<float> RemainTime { get; }
    }
}
