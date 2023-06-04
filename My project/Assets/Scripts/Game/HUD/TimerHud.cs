using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;
using Core;

/// <summary>
/// タイマーのHUD
/// Author : 出合翔太
/// </summary>
namespace Game
{
    public class TimerHud : BaseCustomComponentBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;

        public override void Init()
        {
            base.Init();

            
        }
    }
}