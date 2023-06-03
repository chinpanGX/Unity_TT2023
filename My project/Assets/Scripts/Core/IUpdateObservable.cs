using System;
using UniRx;

namespace Core
{
    /// <summary>
    /// 更新イベント購読するインターフェース
    /// Author : 出合翔太
    /// </summary>
     public interface IUpdateObservable : IFeature
     {
        /// <summary>
        /// イベント購読
        /// </summary>
        /// <param name="updateOrder"> 更新順番 </param>
        /// <returns></returns>
        IObservable<uint> OnUpdate(int updateOrder);
     }
}