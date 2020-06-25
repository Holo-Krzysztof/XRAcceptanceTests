﻿using System.Collections;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PerformanceTest_MeasureScopeTests
{
	[UnityTest, Performance]
	public IEnumerator PerformanceTest_MeasureScopeTest()
	{
		AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync("Assets/Scenes/PerformanceTest/PerformanceTest.unity", LoadSceneMode.Single);

		while (!asyncLoadLevel.isDone)
		{
				yield return null;
		}

		// Allow for initial Scene Load settle time for more stable measurement
		yield return new WaitForSecondsRealtime(1);

		// This simply measures the single time to instantiate 5000 cubes
		// You can replace the contents of this scope with your own functions to measure
		using(Measure.Scope())
		{
			var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			for (var i = 0; i < 5000; i++)
			{
				UnityEngine.Object.Instantiate(cube);
			}
		}
	}
}