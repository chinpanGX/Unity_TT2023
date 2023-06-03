using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

/// <summary>
///  IFeature プール　Queryを高速化
/// Author : 出合翔太
/// </summary>
namespace Core
{    
    public static class FeaturePool<T> where T : IFeature
    {
        /// <summary>
        /// 登録されている Feature のプール
        /// </summary>
        static List<T>[] features;

        /// <summary>
        /// ID解放用 disposable
        /// </summary>
        static HashSet<IDisposable> boundDisposables;

        /// <summary>
        /// 指定IDのFeatureがあるかどうか調べる
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsEntried(int id)
        {
            return features != null && id >= 0 && features[id] != null && features[id].Count > 0;
        }

        /// <summary>
        /// 指定IDの feature の先頭を取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T Get(int id)
        {
            return features[id][0];
        }

        public static IReadOnlyList<T> GetAll(int id)
        {
            return features[id];
        }

        /// <summary>
	    /// 指定idで機能を登録。
	    /// 第三引数 CompositeDisposable.Dispose 時に登録抹消もする。
	    /// </summary>
	    /// <param name="id"></param>
	    /// <param name="i"></param>
	    /// <param name="compositeDisposable"></param>
        public static void Entry(int id, T i, CompositeDisposable compositeDisposable)
        {
            if(id < 0 || id >= FeatureIndexGenerator.ID_MAX)
            {
                Debug.LogError($"IFeature 登録ID={id.ToString()}が不正");
                return;
            }

            if(features == null)
            {
                features = new List<T>[FeatureIndexGenerator.ID_MAX];
            }

            if(features[id] == null)
            {
                features[id] = new List<T>();
            }

            features[id].Add(i);

            // disposable にid解放を紐付ける
            if(boundDisposables == null)
            {
                boundDisposables = new HashSet<IDisposable>();            
            }
            
            if(boundDisposables.Contains(compositeDisposable) == false)
            {
                Disposable.Create(() => 
                {
                    Clear(id);
                    boundDisposables.Remove(compositeDisposable);
                }).AddTo(compositeDisposable);
            }
        }

        /// <summary>
        /// 指定IDのFeatureを削除する　
        /// </summary>
        /// <param name="id"></param>
        static void Clear(int id) 
        {
            if(features == null)
            {
                return;
            }

            if(id < 0 || id >= FeatureIndexGenerator.ID_MAX)
            {
                Debug.LogError($"IFeature 登録id={id.ToString()}が不正です");
                return;
            }

            features[id].Clear();
        }       
    }    
}
