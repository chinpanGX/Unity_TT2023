using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;
using Core;
using UnityEditor;


/// <summary>
/// 更新制御処理を行う
/// Author : 出合翔太
/// </summary>
namespace Game
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateFeature : BaseCustomComponentCshape, IUpdateObservable, IUpdater
    {
        // 更新ストリーム
        readonly SortedDictionary<int, ISubject<Unit>> updateStreams = new SortedDictionary<int, ISubject<Unit>>();

        public override void Add(ICustomComponentContainer owner)
        {
            base.Add(owner);
            owner.AddInterface<IUpdateObservable>(this);
            owner.AddInterface<IUpdater>(this);
        }
        
        /// <summary>
        /// Updateイベントの取得
        /// </summary>
        /// <param name="updateOrder"> 更新順 </param>
        /// <returns> 更新するObeservable </returns>
        IObservable<Unit> IUpdateObservable.OnUpdate(int updateOrder)
        {
            if(updateStreams.TryGetValue(updateOrder, out var update))
            {
                return update;
            }
            else
            {
                // イベントがストリームにない場合、追加する
                Subject<Unit> newSteam = new Subject<Unit>();
                updateStreams.Add(updateOrder, newSteam);
                return newSteam;
            }
        }

        /// <summary>
        /// Updateイベントの発行
        /// </summary>
        void IUpdater.UpdateStreams()
        {
            foreach(var updateStream in updateStreams)
            {
                var update = updateStream.Value;
                update.OnNext(Unit.Default);
            }
        }
    }
}