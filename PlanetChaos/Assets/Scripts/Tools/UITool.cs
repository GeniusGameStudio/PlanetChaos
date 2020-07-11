using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UITool
{
	private static GameObject m_CanvasObj = null; // 場景上的2D畫布物件

	public static void ReleaseCanvas()
	{
		m_CanvasObj = null;
	}

	/// <summary>
	/// 寻找在特定Canvas下的UI界面
	/// </summary>
	/// <param name="UIName"></param>
	/// <returns></returns>
	public static GameObject FindUIGameObject(string UIName)
	{
		if (m_CanvasObj == null)
			m_CanvasObj = UnityTool.FindGameObject("Canvas");
		if (m_CanvasObj == null)
			return null;
		return UnityTool.FindChildGameObject(m_CanvasObj, UIName);
	}

	// 获得UI控件
	public static T GetUIComponent<T>(GameObject Container, string UIName) where T : UnityEngine.Component
	{
		// 找出子物件 
		GameObject ChildGameObject = UnityTool.FindChildGameObject(Container, UIName);
		if (ChildGameObject == null)
			return null;

		T tempObj = ChildGameObject.GetComponent<T>();
		if (tempObj == null)
		{
			Debug.LogWarning("组件[" + UIName + "]不是[" + typeof(T) + "]");
			return null;
		}
		return tempObj;
	}

	/// <summary>
	/// 获得按钮
	/// </summary>
	/// <param name="BtnName"></param>
	/// <returns></returns>
	public static Button GetButton(string BtnName)
	{
		// 取得Canvas
		GameObject UIRoot = GameObject.Find("Canvas");
		if (UIRoot == null)
		{
			Debug.LogWarning("场景中没有UI Canvas");
			return null;
		}

		// 找到对应的Button
		Transform[] allChildren = UIRoot.GetComponentsInChildren<Transform>();
		foreach (Transform child in allChildren)
		{
			if (child.name == BtnName)
			{
				Button tmpBtn = child.gameObject.GetComponent<Button>();
				if (tmpBtn == null)
					Debug.LogWarning("UI组件[" + BtnName + "]不是Button");
				return tmpBtn;
			}
		}

		Debug.LogWarning("UI Canvas中没有Button[" + BtnName + "]存在");
		return null;
	}

	/// <summary>
	/// 获得UI组件
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="UIName"></param>
	/// <returns></returns>
	public static T GetUIComponent<T>(string UIName) where T : UnityEngine.Component
	{
		// 获得Canvas
		GameObject UIRoot = GameObject.Find("Canvas");
		if (UIRoot == null)
		{
			Debug.LogWarning("场景中没有UI Canvas");
			return null;
		}
		return GetUIComponent<T>(UIRoot, UIName);
	}
}
