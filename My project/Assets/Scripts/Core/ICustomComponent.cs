using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

/// <summary>
/// コンポーネントを表すインターフェース
/// Monobehaviourに相当する
/// Author ：　出合翔太
/// </summary>
namespace Core
{
    public interface ICustomComponent : IDisposable
    {
        /// <summary>
        /// 集約元
        /// </summary>
        /// <value></value>
        ICustomComponentContainer Owner { get; }

        /// <summary>
        /// 破棄するもの
        /// </summary>
        CompositeDisposable Disposables{ get; }

        /// <summary>
        /// 機能を追加する
        /// </summary>
        /// <param name="owner"></param>
        void Add(ICustomComponentContainer owner);

        /// <summary>
        /// 初期化　依存解決・イベント登録を行う
        /// </summary>
        void Init();
    }
}
