using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class AsyncTest_Coroutine : MonoBehaviour
{
	private void Start()
	{
		StartCoroutine(Run(10));
	}

	IEnumerator Run(int count)
	{
		int result = 0;
		bool isDone = false;

		(new Thread(() =>
		{
			for (int i = 0; i < count; i++)
			{
				Debug.Log(i);
				result += i;
				Thread.Sleep(1000);
			}

			isDone = true;
		}))
		.Start();

		while (!isDone)
			yield return null;

		Debug.Log("Result : " + result);
	}
}
