using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Core;

/// <summary>
/// DeltaTime
/// </summary>
namespace Game
{
    public class DeltaTime : BaseCustomComponentCshape, IDeltaTime
    {
        // DeltaTime
        float IDeltaTime.Deltatime => Time.deltaTime * timeScale.Value;

        // TimeScale 初期値:1f
        IReactiveProperty<float> IDeltaTime.TimeScale => timeScale;

        private IReactiveProperty<float> timeScale => new ReactiveProperty<float>(1f);

        public override void Add(ICustomComponentContainer owner)
        {
            base.Add(owner);
            owner.AddInterface<IDeltaTime>(this);
        }
    }
}