/***************************************************************************************************
 *  Script name :   GameManager.cs
 *  Created on  :   09.01.2016
 *  Author      :   Rafael Hocevar
 *  Purpose     :   Controls the game flow, win condition, scene transitions, framerate and 
 *                  keeps track of the number of chapters and levels.
*****************************************************************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    #region Private Methods
    protected void Awake()
    {
        //base.Awake();
        Application.targetFrameRate = 30;
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; //Subscribe for the sceneLoaded event
    }

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
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
