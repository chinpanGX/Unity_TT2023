using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Component機能を注入する
/// Author : 出合翔太
/// </summary>
namespace Core
{
    [DefaultExecutionOrder(-1000)]
    public abstract class CustomComponentInstaller : BaseCustomComponentBehaviour, IInstaller
    {        
        public virtual void Awake()
        {
            // 現在のシーン内のCustomComponentContainerを集める
            var nonInitcustomComponentContainer =  gameObject.scene.GetRootGameObjects().Select(e => e.GetComponent<ICustomComponentContainer>()).Where(e => e != null);

            // 事前にコンテナに追加する
            foreach(var comp in nonInitcustomComponentContainer)
            {
                InstallToComponentContainer(comp);
            }
        }

        public override void Add(ICustomComponentContainer owner)
        {
            base.Add(owner);
            owner.AddInterface<IInstaller>(this);
        }

        public void InstallToComponentContainer(ICustomComponentContainer nonInitCustomComponentContainer)
        {
            InstallFeatures(nonInitCustomComponentContainer);
        }

        protected abstract void InstallFeatures(ICustomComponentContainer target);       
    }
}
