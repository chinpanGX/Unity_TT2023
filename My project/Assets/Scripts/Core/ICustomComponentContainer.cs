using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

/// <summary>
/// コンポーネントの集合を表すインターフェース
/// GameObjectに相当する
/// ICustomComponentやIFeatureを保持するコンテナ
/// Author : 出合翔太
/// </summary>
namespace Core
{
    public interface ICustomComponentContainer : IDisposable
    {
        /// <summary>
        /// Componentを追加
        /// </summary>
        /// <param name="components"></param>
        void Add(IEnumerable<ICustomComponent> components);

        /// <summary>
        /// Componentを追加
        /// </summary>
        /// <param name="component"></param>
        void Add(ICustomComponent component);

        /// <summary>
        /// 静的に管理された
        /// </summary>
        /// <returns></returns>
        ICustomComponent[] ExtractComponent();

        /// <summary>
        /// 初期化
        /// </summary>
        void Init();

        /// <summary>
        /// 機能を取得する
        /// </summary>
        /// <param name="feature"> 戻り値 </param>
        /// <typeparam name="T"> 機能インターフェース </typeparam>
        /// <returns> bool 機能が存在すればTrue </returns>
        bool GetInterface<T>(out T feature) where T : IFeature;

        /// <summary>
        /// T型の機能をすべて列挙
        /// </summary>
        /// <param name="components"> 検索結果列挙 </param>
        /// <typeparam name="T"> 機能インターフェース </typeparam>
        /// <returns> bool 機能が存在すればTrue </returns>
        bool GetInterfaces<T>(out IEnumerable<T> components) where T : IFeature;

        /// <summary>
        /// 機能を追加
        /// </summary>
        /// <param name="item"> 追加する機能インターフェース </param>
        /// <typeparam name="T"> 機能インターフェース </typeparam>
        void AddInterface<T>(T item) where T : IFeature; 
    }
}