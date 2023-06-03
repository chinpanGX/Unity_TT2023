using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

/// <summary>
/// コンポーネントが破棄されたときに自動で購読解除する拡張メソッド
/// Author : 出合翔太
/// </summary>
namespace Core
{
    public static  class CustomComponetExtension
    {
        /// <summary>
        /// ICustomComponentにAddTo
        /// </summary>
        /// <param name="disposable"> 紐付ける元 </param>
        /// <param name="component"> 紐付け先 </param>
        /// <returns></returns>
        public static IDisposable AddTo(this IDisposable disposable, ICustomComponent component)
        {
            return disposable.AddTo(component);
        }
    }
}
