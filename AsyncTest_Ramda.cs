using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class AsyncTest_Ramda : MonoBehaviour
{
	private void Start()
	{
		Debug.Log("Run() Invoked in Start()");
		Run(10);
		Debug.Log("Run() returns");
	}

	private void Update()
	{
		Debug.Log("Update()");
	}

	async void Run(int count)
	{
		int result = 0;

		await Task.Run(() =>
		{
			for (int i = 0; i < count; i++)
			{
				Debug.Log(i);
				result += i;
				Thread.Sleep(1000);
			}
		});

		Debug.Log("Result : " + result);
	}

}
