using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
	public class OpenURLButton : MonoBehaviour 
	{
		[SerializeField]
		private string url;

		public void OpenURL ()
		{
			Application.OpenURL (url);
		}
	}

}