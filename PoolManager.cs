using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ObjectPool Manager Make by KENEW 
  Http://github.com/KENEW */

[System.Serializable]
public class ObjectInfo
{
	public string poolName;

	public GameObject prefab;
	public int poolCount;
	public Transform parentTrans;

	[HideInInspector]
	public Queue<GameObject> objQueue = new Queue<GameObject>();
}

public class PoolManager : MonoBehaviour
{
	public ObjectInfo[] objectInfo;

	private void Awake()
	{
		QueueInit();
	}
	private void QueueInit()
	{
		for(int i = 0; i < objectInfo.GetLength(0); i++)
		{
			InsertQueue(objectInfo[i]);
		}
	}
	private void InsertQueue(ObjectInfo t_objectInfo)
	{
		for(int i = 0; i < t_objectInfo.poolCount; i++)
		{
			GameObject t_pool = Instantiate(t_objectInfo.prefab, transform.position, Quaternion.identity);
			t_pool.SetActive(false);

			t_objectInfo.objQueue.Enqueue(t_pool);

			if (t_objectInfo.parentTrans != null)
			{
				t_pool.transform.SetParent(t_objectInfo.parentTrans);
			}
			else
			{
				t_pool.transform.SetParent(this.transform);
			}
		}
	}
	public GameObject ObjectDequeue(string t_poolName)
	{
		for (int i = 0; i < objectInfo.GetLength(0); i++)
		{
			if (t_poolName == objectInfo[i].poolName)
			{
				Debug.Log(objectInfo[i].poolName);

				var t_obj = objectInfo[i].objQueue.Dequeue();
				t_obj.SetActive(true);
				return t_obj;
			}
		}

		Debug.Log("오브젝트를 찾을 수 없습니다.");
		return null;
	}
	public void ObjectEnqueue(string t_poolName, GameObject t_object)
	{
		for(int i = 0; i < objectInfo.GetLength(0); i++)
		{
			if(t_poolName == objectInfo[i].poolName)
			{
				objectInfo[i].objQueue.Enqueue(t_object);
				t_object.SetActive(false);
				return;
			}
		}

		Debug.Log("오브젝트를 찾을 수 없습니다.");
	}
	public void ObjectEnqueue(string t_poolName, GameObject t_object, float t_time)
	{
		StartCoroutine(ObjectEnqueueCo(t_poolName, t_object, t_time));
	}
	private IEnumerator ObjectEnqueueCo(string t_poolName, GameObject t_object, float t_time)
	{
		yield return new WaitForSeconds(t_time);
		ObjectEnqueue(t_poolName, t_object);
	}
}
