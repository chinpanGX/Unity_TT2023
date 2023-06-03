using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Mnobehaviourバージョンの機能を保持するコンテナクラス
/// Author : 出合翔太
/// </summary>
namespace Core
{
    public class CustomComponentContainerBehaviour : MonoBehaviour, ICustomComponentContainer
    {
        private CustomComponentContainerCore ContainerCore = new CustomComponentContainerCore();        
      
        /// <summary>
        /// Unity Start<see cref="CustomComponentContainerFactory"/>経由されていない場合Containerの初期化を行う
        /// ex) UnitySceneに配置されていないもの
        /// </summary>        
        void Start()
        {
            if(ContainerCore.IsInitialized == false)
            {
               this.GetInitComponentContainer(null, null);
            }
        }

        /// <summary>
        /// 静的定義されたComponentContainerを抽出する
        /// MonobehaviourなのでPrefabsにAddComponent済みのICusutomComponent実装しているクラスから持ってくる
        /// </summary>
        /// <typeparam name="ICustomComponent"></typeparam>
        /// <returns></returns>
        ICustomComponent[] ICustomComponentContainer.ExtractComponent() => gameObject.GetComponents<ICustomComponent>();

        void ICustomComponentContainer.Add(ICustomComponent component) => ContainerCore.Add(this, component);
        void ICustomComponentContainer.Add(IEnumerable<ICustomComponent> components) => ContainerCore.Add(this, components);        
        void ICustomComponentContainer.Init() => ContainerCore.Init();
        void IDisposable.Dispose() => ContainerCore.Dispose();               
        void ICustomComponentContainer.AddInterface<T>(T feature) => ContainerCore.AddInterface(feature);        
        bool ICustomComponentContainer.GetInterface<T>(out T feature) => ContainerCore.GetInterface(out feature);      
        bool ICustomComponentContainer.GetInterfaces<T>(out IEnumerable<T> components)  => ContainerCore.GetInterfaces(out components);
    }
}