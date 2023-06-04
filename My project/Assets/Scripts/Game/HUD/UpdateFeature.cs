using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;
using Core;


/// <summary>
/// 更新ストリーム
/// Author : 出合翔太
/// </summary>
namespace Game
{    
    public class UpdateFeature : BaseCustomComponentCshape, IUpdate, IUpdater
    {
        // 更新ストリーム
        readonly SortedDictionary<int, ISubject<Unit>> updateStreams = new SortedDictionary<int, ISubject<Unit>>();

        public override void Add(ICustomComponentContainer owner)
        {
            base.Add(owner);
            owner.AddInterface<IUpdate>(this);
            owner.AddInterface<IUpdater>(this);
        }
        
        /// <summary>
        /// Updateイベントの取得
        /// </summary>
        /// <param name="updateOrder"> 更新順序 </param>
        /// <returns> UpdateObeservable </returns>
        IObservable<Unit> IUpdate.OnUpdate(int updateOrder)
        {
            if(updateStreams.TryGetValue(updateOrder, out var update))
            {
                return update;
            }
            else
            {
                // ストリームにない場合、追加する
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