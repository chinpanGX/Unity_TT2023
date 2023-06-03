using System;
using UniRx;

namespace Core
{
    /// <summary>
    /// Updateイベント開始するインターフェース
    /// Author : 出合翔太
    /// </summary>
    public interface IUpdater : IFeature
    {
        void UpdateStreams();
    }
}