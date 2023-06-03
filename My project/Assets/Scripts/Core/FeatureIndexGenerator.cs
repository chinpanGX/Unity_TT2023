using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// FeaturePool 用の ComponentCollection ごとのシリアル番号を生成する
/// Author : 出合翔太
/// </summary>
namespace Core
{
    public class FeatureIndexGenerator 
    {
        public static readonly int ID_MAX = 1024;
        static FeatureIndexGenerator instance = null;
        Queue<int> freeIds;

        /// <summary>
        /// シングルトンインスタンス
        /// </summary>
        /// <value></value>
        public static FeatureIndexGenerator Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new FeatureIndexGenerator();
                }
                return instance;
            }
        }

        /// <summary>
        /// 未使用のIdを新規取得する
        /// </summary>
        /// <returns></returns>
        public int NewId()
        {
            // idがないので、0を返す
            if(freeIds.Count == 0)
            {
                Debug.Log($"Feature id がなくなった");
                return 0;
            }
            return freeIds.Dequeue();
        }

        /// <summary>
        /// 不要になったIdを未使用に戻す
        /// </summary>
        /// <param name="id"></param>        
        public void ReturnId(int id)
        {
            // id がキューに含まれている
            if(freeIds.Contains(id) == true)
            {
                Debug.Log($"Feature ID:{id.ToString()}を解放する");
                return;
            }
            freeIds.Enqueue(id);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private FeatureIndexGenerator()
        {
            freeIds = new Queue<int>();
            for (int i = 0; i < ID_MAX; i++)
            {
                freeIds.Enqueue(i);
            }
        }
    }
}
