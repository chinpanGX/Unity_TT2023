using UnityEngine;


namespace MyLogger
{
	public static class Debug
	{
	#if !(UNITY_EDITOR || DEVELOPMENT_BUILD)
		[System.Diagnostics.Conditional("DEAI_ORIGINAL_DEBUG_LOG")]
	#endif
		public static void Log(string msg)
		{
			Debug.Log(msg);
		}

	#if !(UNITY_EDITOR || DEVELOPMENT_BUILD)
		[System.Diagnostics.Conditional("DEAI_ORIGINAL_DEBUG_LOG")]
	#endif
		public static void LogWarning(string msg)
		{
			Debug.LogWarning(msg);
		}

	#if !(UNITY_EDITOR || DEVELOPMENT_BUILD)
		[System.Diagnostics.Conditional("DEAI_ORIGINAL_DEBUG_LOG")]
	#endif
		public static void LogError(string msg)
		{
			Debug.LogError(msg);
		}

	#if !(UNITY_EDITOR || DEVELOPMENT_BUILD)
		[System.Diagnostics.Conditional("DEAI_ORIGINAL_DEBUG_LOG")]
	#endif
		public static void LogInterfaceError(string interfaceName)
		{
			LogError(interfaceName + "がコンテナにありません。" + interfaceName + "の実装クラスでコンテナに追加しているか確認をしてください。");
		}
	}
}