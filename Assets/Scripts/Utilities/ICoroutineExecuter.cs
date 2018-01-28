using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
	public interface ICoroutineExecuter 
	{
		Coroutine StartCoroutine (IEnumerator coroutine);
		void StopCoroutine (IEnumerator coroutine);
	}

}