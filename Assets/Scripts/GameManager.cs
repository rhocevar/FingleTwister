/***************************************************************************************************
 *  Script name :   GameManager.cs
 *  Created on  :   09.01.2016
 *  Author      :   Rafael Hocevar
 *  Purpose     :   Controls the game flow, win condition, scene transitions, framerate and 
 *                  keeps track of the number of chapters and levels.
*****************************************************************************************************/

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Utils;

/// <summary>
/// Controls the game flow, win condition, scene transitions, framerate and keeps track of the number of chapters and levels.
/// </summary>
public class GameManager : Singleton<GameManager>
{
    #region Inspector
    /// The total number of chapter in the game.
    [SerializeField] private const int nChapters = 1;
    /// The total number o level for each chapter.
    [SerializeField] private const int nLevelsPerChapter = 3;
    #endregion

    #region Attributes
    private FPSDisplay fpsDisplay;
    private Dictionary<int, Level[]> levels;
	/// The framerate will be fixed at the specified target fps.
    [SerializeField] private int targetFramerate = 60;
    [SerializeField] private int captureFramerate = 40;
    #endregion

    #region Properties
    /// Reference to the display the FPS counter on the screen. Currently used by the InputManager.
    public FPSDisplay FPSDisplay { get { return fpsDisplay; } }
    /// Dictionary of levels: Chapter -> Levels array
    public Dictionary<int, Level[]> Levels { get { return levels; } set { levels = value; } }
    #endregion

    #region Private Methods
    protected override void Awake()
    {
        base.Awake();
        #if UNITY_IOS
            Application.targetFrameRate = 30;
        #else
            Application.targetFrameRate = targetFramerate;
            Time.captureFramerate = captureFramerate; //The higher the slower
        #endif
        fpsDisplay = GetComponent<FPSDisplay>();
    }

    private void Start()
    {
        InitializeLevelsDictionary();
        SceneManager.sceneLoaded += OnSceneLoaded; //Subscribe for the sceneLoaded event
    }
    /// Initializes the dictionary structure to hold the levels and chapters.
    private void InitializeLevelsDictionary()
    {
        levels = new Dictionary<int, Level[]>();

        for (int ch = 1; ch <= nChapters; ch++)
        {
            Level[] lvls = new Level[nLevelsPerChapter];
            for (int lev = 0; lev < nLevelsPerChapter; lev++)
            {
                lvls[lev] = new Level(lev + 1, ch);
            }
            levels.Add(ch, lvls);
        }
        //Unlock first level
        levels[1][0].IsLocked = false;
    }

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == (int)Scenes.Menu)
        {
            UIManager.Instance.ShowInGameUI(false);
            UIManager.Instance.ShowMainMenu(true);
        }
        else
        {
            UIManager.Instance.ShowInGameUI(true);
            UIManager.Instance.ShowMainMenu(false);
        }
    }
	#endregion

	#region Public Methods
    /// <summary>
    /// Loads a scene based on the scene name passed as the parameter.
    /// </summary>
    /// <param name="s"> The name of the scene. </param>
    public void LoadScene(string s)
    {
        SceneManager.LoadScene(s);
    }
	/// <summary>
	/// Loads a scene based on the scene name passed as the parameter.
	/// </summary>
	/// <param name="i"> The name of the scene. </param>
	public void LoadScene(int i)
    {
        SceneManager.LoadScene(i);
    }
    /// <summary>
    /// Exits the game.
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion
}
