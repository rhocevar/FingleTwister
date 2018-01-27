using UnityEngine;

/// <summary>
/// When a class derives from this they will be a singleton don't derive from the derived class 
/// </summary>
/// <typeparam name="T">The generic value have to be the derived class</typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
	//Exposed to the inspector whether or not it should be destroyed on a scene load
	[SerializeField]
	private bool dontDestroyOnload = false;

	//Instance for singleton
	private static T instance;

	//Getting the instance for other classes
	public static T Instance { get { return instance; } }

	/// <summary>
	/// Unity awake method can be overriden but remember to base it
	/// </summary>
	protected virtual void Awake()
	{
		if (instance)
		{
			//Destroying this if there already exists an instance
			Destroy(gameObject);
		}
		else
		{
			//Setting the instance
			instance = (T)this;

			if (dontDestroyOnload)
			{
				//Don't destroy on load if enabled
				DontDestroyOnLoad(this);
			}
		}
	}

	/// <summary>
	/// Unity OnDestroy method can be overriden but remember to base it
	/// </summary>
	protected virtual void OnDestroy()
	{
		if (instance == this)
		{
			//Sets instance to null when the instance is destroyed 
			instance = null;
		}
	}
}
