using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// MonobehaviourクラスバージョンのComponent抽象クラス
/// Author : 出合翔太
/// </summary>
namespace Core
{
    public class BaseCustomComponentBehaviour : MonoBehaviour, ICustomComponent
    {
        /// <summary>
        /// 集約元のコンテナクラス
        /// </summary>
        public ICustomComponentContainer Owner { get; private set; }

        /// <summary>
        /// 破棄対象
        /// </summary>
        public CompositeDisposable Disposables { get; } = new CompositeDisposable();

        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="owner"> 集約元のコンテナ </param>
        public virtual void Add(ICustomComponentContainer owner)
        {
            Owner = owner;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public virtual void Init()
        {

        }

        /// <summary>
        /// 破棄
        /// </summary>
        public virtual void Dispose()
        {
            Disposables.Clear();
        }
    }
}