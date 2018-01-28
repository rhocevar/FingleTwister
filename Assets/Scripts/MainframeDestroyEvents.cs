using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainframeDestroyEvents : MonoBehaviour 
{
	[Inject]
	private MainframeController mainframe;
	[SerializeField]
	private bool onReset;
	[SerializeField]
	private bool onComplete;

	private void Start ()
	{
		if (onReset)
			mainframe.OnReset += Destroy;
		if (onComplete)
			mainframe.OnComplete += Destroy;
	}

	private void Destroy ()
	{
		if (onReset)
			mainframe.OnReset -= Destroy;
		if (onComplete)
			mainframe.OnComplete -= Destroy;
		Destroy (gameObject);
	}
}
