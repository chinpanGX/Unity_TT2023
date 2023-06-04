using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// ゲームの判定実装
/// Author : 出合翔太
/// </summary>
namespace Manager
{
    public class GameJudge : Core.BaseCustomComponentBehaviour, IGameJudge
    {
        [SerializeField] private float gameTime = 30f;
    
        /// <summary>
        /// IReadOnlyReactiveProperty 
        /// 値の読み取り & 購読を提供するインターフェース
        /// </summary>
        public IReadOnlyReactiveProperty<float> RemainTime => remainingGameTime;

        /// <summary>
        /// IReactiveProperty
        /// Subject<T> 機能が同梱された変数
        /// 値の変動をOnNextでメッセージの発行を行う
        /// </summary>
        /// <typeparam name="float"></typeparam>
        /// <returns></returns>
        private readonly IReactiveProperty<float> remainingGameTime = new ReactiveProperty<float>(0f);

        

        public override void Add(Core.ICustomComponentContainer owner)
        {
            base.Add(owner);
            
            owner.AddInterface<IGameJudge>(this);             
        }

        public override void Init()
        {
            base.Init();
        
            // IDeltaTimeがない場合、Logを残して、Returnする
            if(Owner.GetInterface<Game.IDeltaTime>(out Game.IDeltaTime deltatime) == false)
            {                                      
                MyLogger.Debug.LogInterfaceError("IDeltaTime");             
                return;                
            }
            
            // 残り時間の初期化
            remainingGameTime.Value = gameTime;


            if(Owner.GetInterface(out Core.IUpdate Update) == true)
            {
                Update.OnUpdate((int)UPDATE_ORDER.JUDGE).Subscribe(_ => remainingGameTime.Value = Mathf.Max(0f, remainingGameTime.Value - deltatime.Deltatime)).AddTo(this);
            }
        }

    }
}