using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	public void SetProgress (float progress)
	{
		ApplyToTexts ((e) => e.enabled = progress > 0f);
		progressBar.fillAmount = progress;
		chromatic.SetMaxOffset (Mathf.Lerp (minOffset, maxOffset, 1-progress));
	}

    public void Complete ()
    {
		ApplyToTexts ((e) => e.text = "Complete!");
        progressBar.fillAmount = 1f;
		chromatic.SetMaxOffset (0f);
    }

	private void ApplyToTexts (Action<Text> modifier)
	{
		foreach (Text text in progressText)
			modifier (text);
	}
}
