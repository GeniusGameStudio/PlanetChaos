using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnityTool
{
	public static void Attach(GameObject ParentObj, GameObject ChildObj, Vector3 Pos)
	{
		ChildObj.transform.parent = ParentObj.transform;
		ChildObj.transform.localPosition = Pos;
	}

	public static void AttachToRefPos(GameObject ParentObj, GameObject ChildObj, string RefPointName, Vector3 Pos)
	{
		// 搜索
		bool bFinded = false;
		Transform[] allChildren = ParentObj.transform.GetComponentsInChildren<Transform>();
		Transform RefTransform = null;
		foreach (Transform child in allChildren)
		{
			if (child.name == RefPointName)
			{
				if (bFinded == true)
				{
					Debug.LogWarning("物体[" + ParentObj.transform.name + "]内有两个以上的参考点[" + RefPointName + "]");
					continue;
				}
				bFinded = true;
				RefTransform = child;
			}
		}

		//  是否找到
		if (bFinded == false)
		{
			Debug.LogWarning("物体[" + ParentObj.transform.name + "]内没有参考点[" + RefPointName + "]");
			Attach(ParentObj, ChildObj, Pos);
			return;
		}

		ChildObj.transform.parent = RefTransform;
		ChildObj.transform.localPosition = Pos;
		ChildObj.transform.localScale = Vector3.one;
		ChildObj.transform.localRotation = Quaternion.Euler(Vector3.zero);
	}


	/// <summary>
	/// 找到场景上的物体
	/// </summary>
	/// <param name="GameObjectName"></param>
	/// <returns></returns>
	public static GameObject FindGameObject(string GameObjectName)
	{
		// 找到对应的GameObject
		GameObject pTmpGameObj = GameObject.Find(GameObjectName);
		if (pTmpGameObj == null)
		{
			Debug.LogWarning("场景中找不到GameObject[" + GameObjectName + "]物体");
			return null;
		}
		return pTmpGameObj;
	}

	/// <summary>
	/// 取得子物体
	/// </summary>
	/// <param name="Container"></param>
	/// <param name="gameobjectName"></param>
	/// <returns></returns>
	public static GameObject FindChildGameObject(GameObject Container, string gameobjectName)
	{
		if (Container == null)
		{
			Debug.LogError("NGUICustomTools.GetChild : Container =null");
			return null;
		}

		Transform pGameObjectTF = null; //= Container.transform.FindChild(gameobjectName);											


		// 是不是Container本身
		if (Container.name == gameobjectName)
			pGameObjectTF = Container.transform;
		else
		{
			// 找出所有子物体				
			Transform[] allChildren = Container.transform.GetComponentsInChildren<Transform>();
			foreach (Transform child in allChildren)
			{
				if (child.name == gameobjectName)
				{
					if (pGameObjectTF == null)
						pGameObjectTF = child;
					else
						Debug.LogWarning("Container[" + Container.name + "]下找出重复的物体名称[" + gameobjectName + "]");
				}
			}
		}

		// 都没有找到
		if (pGameObjectTF == null)
		{
			Debug.LogError("物体[" + Container.name + "]找不到子物体[" + gameobjectName + "]");
			return null;
		}

		return pGameObjectTF.gameObject;
	}
}
