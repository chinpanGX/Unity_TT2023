using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using Unity.VisualScripting;

/// <summary>
/// 機能集約クラス
/// Author : 出合翔太
/// </summary>
namespace Core
{
    public class CustomComponentContainerCore
    {
        Dictionary<Type, List<IFeature>> features = new Dictionary<Type, List<IFeature>>(); // !< components
        HashSet<ICustomComponent> componentHash = new HashSet<ICustomComponent>();

        int featurePoolIndex = -1;
        int FeaturePoolIndex
        { 
            get
            {
                if(featurePoolIndex < 0)
                {
                    featurePoolIndex = FeatureIndexGenerator.Instance.NewId();
                }
                
                return featurePoolIndex;
            } 
        }

        /// <summary>
        /// 初期化済み化
        /// </summary>
        /// <value></value>
        public bool IsInitialized{ get; private set; } = false;
        
        /// <summary>
        /// Disposeされるもの
        /// </summary>
        /// <returns></returns>
        protected CompositeDisposable CompositeDisposable{ get; } = new CompositeDisposable();

        /// <summary>
        /// コンポーネントを追加
        /// </summary>
        /// <param name="Container"> 追加先のコレクション </param>
        /// <param name="components"> 追加するコンポーネントの配列の要素 </param>
        public void Add(ICustomComponentContainer Container, IEnumerable<ICustomComponent> components)
        {
            foreach(var comp in components)
            {
                Add(Container, comp);
            }
        }

        /// <summary>
        /// コンポーネントを追加
        /// </summary>
        /// <param name="Container"> 追加先のコレクション </param>
        /// <param name="component"> 追加するコンポーネント </param>
        public void Add(ICustomComponentContainer Container, ICustomComponent component)
        {
            component.Add(Container);
            componentHash.Add(component);
            component.AddTo(CompositeDisposable);
        }

        /// <summary>
        /// コンポーネントの初期化
        /// </summary>
        public void Init()
        {
            foreach (var comp in componentHash)
            {
                comp.Init();
            }

            IsInitialized = true;
        }

        /// <summary>
        /// 破棄
        /// </summary>
        public void Dispose()
        {
            // 一括でDisposeを行う
            CompositeDisposable.Clear();
            features.Clear();
            componentHash.Clear();

            if(featurePoolIndex >= 0)
            {
                FeatureIndexGenerator.Instance.ReturnId(featurePoolIndex);
                featurePoolIndex = -1;
            }
        }

        /// <summary>
        /// 機能を追加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="feature"></param>
        public void AddInterface<T>(T feature) where T : IFeature
        {
            // T型のキーが存在しないとき
            if(features.ContainsKey(typeof(T)) == false)
            {
                // T型キーに新しいリストを生成
                features[typeof(T)] = new List<IFeature>();
            }
            
            // キーがすでにあるなら、キーにくっついているListに追加する 
            features[typeof(T)].Add(feature);
            // インデックスに登録しておく
            FeaturePool<T>.Entry(FeaturePoolIndex, feature, CompositeDisposable);
        }

        /// <summary>
        /// インターフェースを取得
        /// なければ、Assert
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetInterface<T>() where T : IFeature
        {
            T feature = default(T); // 0埋め
            // 機能を取得を試みる
            bool ok = GetInterface<T>(out feature);
            
            // 機能がなかったとき
            if(ok  == false)
            {
                Debug.LogAssertion($"GetInterface 要求された機能:{typeof(T).Name}を持っていない");
            }

            return feature;
        }

        /// <summary>
        /// T型のFeatureの最初に一致したものを返す
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="feature"></param>
        /// <returns></returns>
        public bool GetInterface<T>(out T feature) where T : IFeature
        {
            // 指定された機能をインデックスから検索
            if(FeaturePool<T>.IsEntried(featurePoolIndex) == true)
            {
                // あれば、参照先のfeatureに代入し、Trueを返す
                feature = FeaturePool<T>.Get(featurePoolIndex);
                return true;
            }
            else
            {
                // なければ、0埋めしFalseを返す
                feature = default; // 0埋め
                return false;
            }
        }

        /// <summary>
        /// T型Featureをすべて列挙
        /// </summary>
        /// <typeparam name="T"> IFeature継承クラス </typeparam>
        /// <param name="ret"> Component検索結果 </param>
        /// <returns>true = 1つでも存在する</returns>
        public bool GetInterfaces<T>(out IEnumerable<T> ret) where T : IFeature
        {
            // 指定された機能をインデックスから検索
            if (FeaturePool<T>.IsEntried(featurePoolIndex) == true)
            {
                // あれば、参照先にReadOnlyListとして代入し、Trueを返す
                ret = FeaturePool<T>.GetAll(featurePoolIndex);
                return true;
            }
            else 
            {                
                ret = new T[0];
                return false; 
            }
        }
    }
}