using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;
using Core;
using UnityEditor;


/// <summary>
/// �X�V���䏈�����s��
/// Author : �o���đ�
/// </summary>
namespace Game
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateFeature : BaseCustomComponentCshape, IUpdateObservable, IUpdater
    {
        // �X�V�X�g���[��
        readonly SortedDictionary<int, ISubject<Unit>> updateStreams = new SortedDictionary<int, ISubject<Unit>>();

        public override void Add(ICustomComponentContainer owner)
        {
            base.Add(owner);
            owner.AddInterface<IUpdateObservable>(this);
            owner.AddInterface<IUpdater>(this);
        }
        
        /// <summary>
        /// Update�C�x���g�̎擾
        /// </summary>
        /// <param name="updateOrder"> �X�V�� </param>
        /// <returns> �X�V����Obeservable </returns>
        IObservable<Unit> IUpdateObservable.OnUpdate(int updateOrder)
        {
            if(updateStreams.TryGetValue(updateOrder, out var update))
            {
                return update;
            }
            else
            {
                // �C�x���g���X�g���[���ɂȂ��ꍇ�A�ǉ�����
                Subject<Unit> newSteam = new Subject<Unit>();
                updateStreams.Add(updateOrder, newSteam);
                return newSteam;
            }
        }

        /// <summary>
        /// Update�C�x���g�̔��s
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