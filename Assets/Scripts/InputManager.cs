/***************************************************************************************************
 *  Script name :   InputManager.cs
 *  Created on  :   05.01.2017
 *  Author      :   Rafael Hocevar
 *  Purpose     :   Provides an easy and modularized way to get any input detected by the selected platform.
 *                  Every time an input is detected for a given device, an event “OnInputDetected” is called, 
 *                  so that any subscribed listeners could get the horizontal and vertical input.
*****************************************************************************************************/

using UnityEngine;
using Utils;

/// <summary>
/// provides an easy and modularized way to get any input detected by the selected platform. 
/// Every time an input is detected for a given device, an event “OnInputDetected” is called, 
/// so that any subscribed listeners could get the horizontal and vertical input.
/// </summary>
public class InputManager : Singleton<InputManager>
{
	#region Inspector
    /// The panel game object that contains the button to be pressed on mobile devices.
	//[SerializeField] private GameObject inputPanel;
    /// The height percentage of the screen covered by the input panel. The default value is 0.4 (40%).
    //[SerializeField] private float inputPanelHeight = 0.4f;
	/// The frequency in which the InputManager detects new inputs. The default value is 1 every 0.01s. 
	[SerializeField] private float pressRate = 0.01f;
    #endregion

    #region Attributes
    //private int width = Screen.width;
    //private int height = Screen.height;
    private bool isControlsHintEnabled = false;
    private bool isPointerDown = false;
    private float nextPress = 0.0f;
    private int horizontalAxis = 0;
    private int verticalAxis = 0;
    private EventArgs evArgs = new EventArgs();
    #endregion

    #region Properties
    /// The horizontal input detected. Ranges from -1 (left) to 1 (right).
	public int HorizontalAxis { get { return horizontalAxis; } }
    /// The vertical input detected. Ranges from -1 (down) to 1 (up).
    public int VerticalAxis { get { return verticalAxis; } }

    public bool IsControlsHintEnabled { get { return isControlsHintEnabled; } set { isControlsHintEnabled = value; } }
	#endregion

	#region Private Methods
	//private void Start()
	//{
		//RectTransform ip = inputPanel.GetComponent<RectTransform>();
		//ip.sizeDelta = new Vector2(ip.sizeDelta.x, height * inputPanelHeight);
		//diagonal = Mathf.Sqrt(Mathf.Pow(width, 2) + Mathf.Pow(height, 2));
		//swipeDist = (int)(diagonal * swipeLengthPercentage);
	//}

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            GameManager.Instance.ExitGame();
        }
        #if UNITY_IOS
            //GetMobileInput();
        #else
            GetKeyboardInput();
        #endif
        if (Time.time > nextPress)
        {
            if (Input.GetKey(KeyCode.Tab))
            {
                GameManager.Instance.FPSDisplay.ShowFPS();
            }
            if ((horizontalAxis != 0 || verticalAxis != 0))
            {
                evArgs.intArg1 = horizontalAxis; evArgs.intArg2 = verticalAxis;
                #if UNITY_IOS
                if(isPointerDown)
                {
                    EventManager.Instance.TriggerEvent(EventHashes.InputDetected, evArgs);
                }
                #else
                    EventManager.Instance.TriggerEvent(EventHashes.InputDetected, evArgs);
				#endif
				
            }
            nextPress = Time.time + pressRate;
        }
    }

    private void GetKeyboardInput()
    {
        horizontalAxis = (int)(Input.GetAxisRaw("Horizontal"));
        verticalAxis = (int)(Input.GetAxisRaw("Vertical"));
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
    /// <summary>
    /// Event that triggers when the player has pressed a button in the inputPanel.
    /// </summary>
    /// <param name="dir">Integer value that indicates the direction pressed: Left, Right, Up or Down.</param>
    public void OnPointerDown(int dir)
    {
        switch (dir)
        {
            case 0: //Left
                horizontalAxis = -1;
                verticalAxis = 0;
                break;
            case 1: //Right
                horizontalAxis = 1;
                verticalAxis = 0;
                break;
            case 2: //Up
                horizontalAxis = 0;
                verticalAxis = 1;
                break;
            case 3: //Down
                horizontalAxis = 0;
                verticalAxis = -1;
                break;
        }
        isPointerDown = true;
    }
    /// <summary>
    /// Event called when the user release the button.
    /// </summary>
    public void OnPointerUp()
    {
        ClearInput();
        //if(isControlsHintEnabled)
        //{
        //    UIManager.Instance.StartControlsHintFade(1);
        //    isControlsHintEnabled = false;
        //}
    }

    #endregion

    //PREVIOUS INPUT METHODS (SWIPE)
	/*
	 *     
	 * //[SerializeField] private float swipeLengthPercentage = 0.1f;
    //[SerializeField] private int widthPartition = 4;
    //[SerializeField] private int heightPartition = 6;
     *  //Mobile Variables -------------------------
    //private float diagonal;
    //private int swipeDist;
    // Touch initialTouch = new Touch();
    //float distance = 0;
    //bool hasSwiped = false;
    //Directions swipeDir = Directions.None;
    //bool hasTapped = false;
    //--------------------------------------

	//Captures Swipe and Hold inputs + Tap in all 4 directions to move 1 unit
	private void GetMobileInput()
	{
		foreach (Touch t in Input.touches)
		{
			if (t.phase == TouchPhase.Began)
			{
				initialTouch = t;
				swipeDir = Directions.None;
			}
			else if (t.phase == TouchPhase.Moved)
			{
				CheckNewSwipe(t);
			}
			//If the player swipes and keep touching the screen in the same position
			else if (hasSwiped && t.phase == TouchPhase.Stationary)
			{
				switch (swipeDir)
				{
					case Directions.Left:
						//Debug.Log("Holding left.");
						horizontalAxis = -1;
						verticalAxis = 0;
						break;
					case Directions.Right:
						//Debug.Log("Holding Right.");
						horizontalAxis = 1;
						verticalAxis = 0;
						break;
					case Directions.Down:
						//Debug.Log("Holding Down.");
						horizontalAxis = 0;
						verticalAxis = -1;
						break;
					case Directions.Up:
						//Debug.Log("Holding Up.");
						horizontalAxis = 0;
						verticalAxis = 1;
						break;
				}
			}
			else if (t.phase == TouchPhase.Ended && !hasSwiped)
			{
				//Tap
				TapMove(t); //Check touch position to decide the movement direction
			}
			else if (t.phase == TouchPhase.Ended && hasSwiped)
			{
				//Debug.Log("Touch ended.");
				ClearInput();
			}
		}
	}
    private void TapMove(Touch t)
    {
        Directions dir = CheckTapPosition(t);

        switch (dir)
        {
            case Directions.Left:
                horizontalAxis = -1;
                verticalAxis = 0;
                break;
            case Directions.Right:
                horizontalAxis = 1;
                verticalAxis = 0;
                break;
            case Directions.Down:
                horizontalAxis = 0;
                verticalAxis = -1;
                break;
            case Directions.Up:
                horizontalAxis = 0;
                verticalAxis = 1;
                break;
        }
        hasTapped = true;
    }

    private Directions CheckTapPosition(Touch t)
    {
        //Debug.Log("Touch position = (" + t.position.x + "," + t.position.y + ")");
        int wPartition = width / widthPartition;
        int hPartition = height / heightPartition;

        if (t.position.x < wPartition)
        {
            return Directions.Left;
        }
        else if (t.position.x > width - wPartition)
        {
            return Directions.Right;
        }
        else
        {
            if (t.position.y >= hPartition)
            {
                return Directions.Up;
            }
            return Directions.Down;
        }
    }

        private void CheckNewSwipe(Touch t)
    {
        float deltaX = initialTouch.position.x - t.position.x;
        float deltaY = initialTouch.position.y - t.position.y;
        distance = Mathf.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
        bool swipedSideways = Mathf.Abs(deltaX) > Mathf.Abs(deltaY);

        if (distance >= swipeDist)
        {
            if (swipedSideways && deltaX > 0)
            {
                //Debug.Log("Swiped left.");
                horizontalAxis = -1;
                verticalAxis = 0;
                swipeDir = Directions.Left;
            }
            else if (swipedSideways && deltaX <= 0)
            {
                //Debug.Log("Swiped right.");
                horizontalAxis = 1;
                verticalAxis = 0;
                swipeDir = Directions.Right;
            }
            else if (!swipedSideways && deltaY > 0)
            {
                //Debug.Log("Swiped down.");
                horizontalAxis = 0;
                verticalAxis = -1;
                swipeDir = Directions.Down;
            }
            else if (!swipedSideways && deltaY <= 0)
            {
                //Debug.Log("Swiped up.");
                horizontalAxis = 0;
                verticalAxis = 1;
                swipeDir = Directions.Up;
            }
            initialTouch.position = t.position;
            hasSwiped = true;
        }
    }
    */

	/* Captures swipe only events
	void GetMobileInputSwipe()
	{
		foreach(Touch t in Input.touches)
		{
			if(t.phase == TouchPhase.Began)
			{
				initialTouch = t;
			}
			else if (t.phase == TouchPhase.Moved && !hasSwiped)
			{
				float deltaX = initialTouch.position.x - t.position.x;
				float deltaY = initialTouch.position.y - t.position.y;
				distance = Mathf.Sqrt ((deltaX * deltaX) + (deltaY * deltaY));
				bool swipedSideways = Mathf.Abs (deltaX) > Mathf.Abs (deltaY);

				if(distance > swipeDist)
				{
					if(swipedSideways && deltaX > 0)
					{
						//Swiped left
						//Debug.Log("Swiped left.");
						horizontalAxis = -1;
						verticalAxis = 0;
					}
					else if(swipedSideways && deltaX <= 0)
					{
						//Swiped right
						//Debug.Log("Swiped right.");
						horizontalAxis = 1;
						verticalAxis = 0;
					}
					else if(!swipedSideways && deltaY > 0)
					{
						//Swiped down
						//Debug.Log("Swiped down.");
						horizontalAxis = 0;
						verticalAxis = -1;
					}
					else if(!swipedSideways && deltaY <= 0)
					{
						//Swiped up
						//Debug.Log("Swiped up.");
						horizontalAxis = 0;
						verticalAxis = 1;
					}
					hasSwiped = true;
				}

			}
			else if(t.phase == TouchPhase.Ended && hasSwiped)
			{
				initialTouch = new Touch ();
				hasSwiped = false;
			}
			else if(t.phase == TouchPhase.Ended && !hasSwiped)
			{
				initialTouch = new Touch ();
				//Tap
				//Debug.Log("Tapped.");
				horizontalAxis = 0;
				verticalAxis = 1;
			}
		}
	}
	*/
}
