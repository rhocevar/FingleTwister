using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager
{
	private string[] levels;
	private int current;
    private string startScene;

    public LevelManager (string[] levels, string startScene)
	{
		this.levels = levels;
		current = 0;
		this.startScene = startScene;
	}

	public void LoadNextLevel ()
	{
		if (current == levels.Length)
			Restart ();
		else
		{
			SceneManager.LoadScene (levels[current]);
			current++;
		}
	}

    private void Restart()
    {
		current = 0;
		SceneManager.LoadScene (startScene);
    }
}
