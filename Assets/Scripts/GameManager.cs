using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Zenject;

public class GameManager: IInitializable
{
	private MainframeController mainframe;
	private float loadSceneTime;
    private LevelManager levelManager;
    private ICoroutineExecuter coroutines;

    public GameManager (LevelManager levelManager, MainframeController mainframe, ICoroutineExecuter coroutines, float loadSceneTime)
    {
		this.mainframe = mainframe;
		this.loadSceneTime = loadSceneTime;
		this.levelManager = levelManager;
		this.coroutines = coroutines;
    }

    public void Initialize()
    {
		mainframe.OnComplete += LoadNextLevel;
    }

    private void LoadNextLevel()
    {
		coroutines.StartCoroutine (LoadLevelRoutine ());
    }

	private IEnumerator LoadLevelRoutine ()
	{
		yield return new WaitForSeconds (loadSceneTime);
		levelManager.LoadNextLevel ();
	}
}
