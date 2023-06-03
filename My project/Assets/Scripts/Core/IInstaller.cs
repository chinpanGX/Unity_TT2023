using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 機能を注入するためのインターフェース
/// Author : 出合翔太
/// </summary>
namespace Core
{
    public interface IInstaller : IFeature
    {
        void InstallToComponentContainer( ICustomComponentContainer nonInitializedContainer );
    }
}