using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

/// <summary>
/// C#バージョンの機能を保持するコンテナクラス
/// Author : 出合翔太
/// </summary>
namespace Core
{
    public class CustomComponentContainerCshape : ICustomComponentContainer
    {
        CustomComponentContainerCore ContainerCore = new CustomComponentContainerCore();

        /// <summary>
        /// 静的設定されたcomponentContainerを抽出する。 C#には存在しないので、空の配列にする
        /// </summary>
        /// <returns></returns>
        ICustomComponent[] ICustomComponentContainer.ExtractComponent() => System.Array.Empty<ICustomComponent>();

        void ICustomComponentContainer.Add(ICustomComponent component) => ContainerCore.Add(this, component);        
        void ICustomComponentContainer.Add(IEnumerable<ICustomComponent> components) => ContainerCore.Add(this, components);               
        void ICustomComponentContainer.Init() => ContainerCore.Init();
        void IDisposable.Dispose() => ContainerCore.Dispose();       
        void ICustomComponentContainer.AddInterface<T>(T feature) => ContainerCore.AddInterface(feature);
        bool ICustomComponentContainer.GetInterface<T>(out T feature) => ContainerCore.GetInterface(out feature);          
        bool ICustomComponentContainer.GetInterfaces<T>(out IEnumerable<T> components) => ContainerCore.GetInterfaces(out components);           
    }
}