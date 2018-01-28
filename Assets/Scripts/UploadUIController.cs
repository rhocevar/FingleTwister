using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UploadUIController : MonoBehaviour
{
	[SerializeField]
	private Image progressBar;
	[SerializeField]
	private ChromaticAberration chromatic;
	[SerializeField]
	private float minOffset = 0f;
	[SerializeField]
	private float maxOffset = 0.1f;
	[SerializeField]
	private Text[] progressText;
	[Inject]
	private MainframeController controller;

	private void Start ()
	{
		controller.OnComplete += Complete;
		controller.OnProgressChanged += SetProgress;
		controller.OnReset += ResetProgress;
	}

	private void SetProgress (float progress)
	{
		bool isActive = progress > 0.05f;
		ApplyToTexts ((e) => e.enabled = isActive);
		progressBar.fillAmount = progress;
		float offset = isActive? Mathf.Lerp (minOffset, maxOffset, 1-progress) : 0f;
		chromatic.SetMaxOffset (offset);
	}

    private void Complete ()
    {
		ApplyToTexts ((e) => e.text = "Complete!");
        progressBar.fillAmount = 1f;
		chromatic.SetMaxOffset (0f);
    }

	private void ResetProgress ()
	{
		//Debug.Log ("Reset");
		ApplyToTexts ((e) => {
			e.enabled = false;
			e.text = "Uploading...";
		});
        progressBar.fillAmount = 0f;
		chromatic.SetMaxOffset (0f);
	}

	private void ApplyToTexts (Action<Text> modifier)
	{
		foreach (Text text in progressText)
			modifier (text);
	}
}
