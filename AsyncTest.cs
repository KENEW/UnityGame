//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.Threading;
//using System.Threading.Tasks;

//public class AsyncTest : MonoBehaviour
//{
//	private void Start()
//	{
//		Debug.Log("Run() Invoked in Start()");
//		Run(10);
//		Debug.Log("Run() returns");
//	}

//	async void Run(int count)
//	{
//		var task = Task.Run(() => CountAsync(10));

//		int result = await task;

//		Debug.Log("Result : " + result);
//	}
//	int CountAsync(int count)
//	{
//		int result = 0;

//		for(int i = 0; i < count; i++)
//		{
//			Debug.Log(i);
//			result += i;
//			Thread.Sleep(1000);
//		}

//		return result;
//	}
//}
