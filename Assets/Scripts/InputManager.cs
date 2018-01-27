/***************************************************************************************************
 *  Script name :   InputManager.cs
 *  Created on  :   05.01.2017
 *  Author      :   Rafael Hocevar
 *  Purpose     :   Provides an easy and modularized way to get any input detected by the selected platform.
 *                  Every time an input is detected for a given device, an event “OnInputDetected” is called, 
 *                  so that any subscribed listeners could get the horizontal and vertical input.
*****************************************************************************************************/

using UnityEngine;

public class InputManager : Singleton<InputManager>
{
	#region Inspector
	[SerializeField] private float pressRate = 0.01f;
    #endregion

    #region Attributes
    private bool isPointerDown = false;
    private float nextPress = 0.0f;
    private int horizontalAxis = 0;
    private int verticalAxis = 0;
    #endregion

    #region Properties
    /// The horizontal input detected. Ranges from -1 (left) to 1 (right).
	public int HorizontalAxis { get { return horizontalAxis; } }
    /// The vertical input detected. Ranges from -1 (down) to 1 (up).
    public int VerticalAxis { get { return verticalAxis; } }
	#endregion

	#region Private Methods

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            GameManager.Instance.ExitGame();
        }

        GetMobileInput();
    }

	private void ClearInput()
	{
		//initialTouch = new Touch();
		//hasSwiped = false;
		//swipeDir = Directions.None;
		horizontalAxis = 0;
		verticalAxis = 0;
		//hasTapped = false;
		isPointerDown = false;
	}
    #endregion

    #region Public Methods

    #endregion

    //Mobile Variables -------------------------
    private float diagonal;
    Touch initialTouch = new Touch();
    float distance = 0;
    //--------------------------------------

	//Captures Swipe and Hold inputs + Tap in all 4 directions to move 1 unit
	private void GetMobileInput()
	{
		foreach (Touch t in Input.touches)
		{
			if (t.phase == TouchPhase.Began)
			{
				initialTouch = t;

			}
			else if (t.phase == TouchPhase.Moved)
			{
				
			}
			//If the player swipes and keep touching the screen in the same position
			else if (t.phase == TouchPhase.Stationary)
			{
                
			}
			else if (t.phase == TouchPhase.Ended)
			{
				//Debug.Log("Touch ended.");
				ClearInput();
			}
		}
	}
}
