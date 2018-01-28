using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Menu
{
	public class StartGameButton : MonoBehaviour 
	{
		[Inject]
		private LevelManager levels;

		public void LoadLevel ()
		{
			levels.LoadNextLevel ();
		}
	}
}