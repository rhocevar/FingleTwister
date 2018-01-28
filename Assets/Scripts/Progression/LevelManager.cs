using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager
{
	private string[] levels;
	private int current;

	public LevelManager (string[] levels)
	{
		this.levels = levels;
		current = 0;
	}

	public void LoadNextLevel ()
	{
		SceneManager.LoadScene (levels[current]);
		current++;
	}
}
