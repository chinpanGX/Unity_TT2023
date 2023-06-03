using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

/// <summary>
/// CustomComponentContainerのFactoryメソッド
/// Author ： 出合翔太
/// </summary>
namespace Core
{
    public static class CustomComponentContainerFactory
    {
        /// <summary>
        /// ComponentContainer生成（Unityに依存しない、C#クラス版）
        /// </summary>
        /// <param name="components">注入コンポーネント</param>
        /// <param name="onRegister">機能登録処理</param>
        /// <returns></returns>
        public static ICustomComponentContainer CreateComponentContainer(IEnumerable<ICustomComponent> components, System.Action<ICustomComponentContainer> onAdd)
        {
            ICustomComponentContainer Container = new CustomComponentContainerCshape();
            return Container.GetInitComponentContainer(components, onAdd);
        }

        /// <summary>
        /// ComponentContainer生成 （GameObjectからInstantiate）
        /// </summary>
        /// <param name="basePrefab">生成用プレハブ</param>
        /// <param name="components">注入コンポーネント </param>
        /// <param name="onRegister">機能登録時処理</param>
        /// <returns> 生成したComponentContainer </returns>
        public static ICustomComponentContainer CreateComponentContainer( GameObject basePrefab, IEnumerable<ICustomComponent> components = null, System.Action<ICustomComponentContainer> onAdd = null)
        {
            GameObject ret = basePrefab == null ? new GameObject() : GameObject.Instantiate( basePrefab );
            return CreateComponentContainerWithoutInstantiate(ret, components, onAdd);
        }

        /// <summary>
        /// componentContainer生成(Instance化されたGameObject上にComponentContainerを構築)
        /// </summary>
        /// <param name="instanceObject">生成されたオブジェクト</param>
        /// <param name="components">注入コンポーネント</param>
        /// <param name="onRegister">機能登録時処理</param>
        /// <returns>生成したComponentContainer</returns>
        public static ICustomComponentContainer CreateComponentContainerWithoutInstantiate(GameObject instanceObject, 
                                                                                                IEnumerable<ICustomComponent> components = null, 
                                                                                                Action<ICustomComponentContainer> onAdd = null )
        {
            components = components == null ? Enumerable.Empty<ICustomComponent>() : components;
            var Container = CreateComponentContainerWithGameobject(instanceObject, components, onAdd);
            return Container;
        }   

        /// <summary>
	    /// 生成されたばかりのComponentContainerに対し、Component関連の初期化を行う
	    /// </summary>
	    /// <param name="nonInitializedContainer">未初期化Container</param>
	    /// <param name="components">注入コンポーネント</param>
	    /// <param name="onRegister">機能登録処理</param>
	    /// <returns></returns>
        public static ICustomComponentContainer GetInitComponentContainer(this ICustomComponentContainer nonInitContainer, 
                                                                            IEnumerable<ICustomComponent> components, 
                                                                            Action<ICustomComponentContainer> onAdd)   
        {
            components =  components == null ? Enumerable.Empty<ICustomComponent>() : components;
            ICustomComponent[] compMonobehaivour = nonInitContainer.ExtractComponent();

            if(onAdd != null)
            {
                onAdd.Invoke(nonInitContainer);
            }

            // 注入されたICustomComponentContainerと静的定義のComponentwをまとめて追加、初期化
            nonInitContainer.AddAndInit(compMonobehaivour.Concat(components));
            return nonInitContainer;
        }             

        # region private_menber_function

        /// <summary>
        /// ComponentContainerの生成
        /// </summary>
        /// <param name="gameObject"> 生成元のプレファブ </param>
        /// <param name="components"> 注入するコンポーネント </param>
        /// <param name="onAdd"> 機能追加処理 </param>
        /// <returns> ICustomComponentContainerインターフェース </returns>
        private static ICustomComponentContainer CreateComponentContainerWithGameobject(GameObject gameObject, 
                                                                                            IEnumerable<ICustomComponent> components, 
                                                                                            Action<ICustomComponentContainer> onAdd)        
        {             
            ICustomComponentContainer Container = gameObject.GetComponent<CustomComponentContainerBehaviour>();
            if (Container == null)
            {
                Container = gameObject.AddComponent<CustomComponentContainerBehaviour>();
            }
            return Container.GetInitComponentContainer(components, onAdd);
        }

        /// <summary>
        /// 機能追加と初期化
        /// </summary>
        /// <param name="Container"></param>
        /// <param name="components"></param>
        private static void AddAndInit(this ICustomComponentContainer Container, IEnumerable<ICustomComponent> components)
        {
            Container.Add(components);
            Container.Init();
        }
        
        # endregion 
    }
}