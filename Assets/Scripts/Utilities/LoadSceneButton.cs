using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
	public class LoadSceneButton : MonoBehaviour
	{
		[SerializeField]
		private string sceneName;

		public void LoadScene ()
		{
			SceneManager.LoadScene (sceneName);
		}
	}
}