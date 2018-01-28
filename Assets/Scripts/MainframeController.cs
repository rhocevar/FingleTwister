using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Utils;
using Dragging;
using Power;
using Zenject;
using System;
using Audio;

public class MainframeController : BaseElectricObject
{
    public event Action<float> OnProgressChanged;
    public event Action OnComplete;
    public event Action OnReset;

    [SerializeField] private float holdTimer = 3.0f;
    [SerializeField] private uint nFilesGoal = 2;
    [Inject]
    private LevelManager levels;
    [Inject] AudioManager audioManager;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSourceProcessing;

    private LayerMask draggableLayer;
    private float timeCounter;
    private bool isHoldingFiles;
    HashSet<Draggable> mainframeSet;
    private bool isComplete;

    protected override void Awake()
    {
        base.Awake ();
        draggableLayer = LayerMask.NameToLayer("Draggable");
        mainframeSet = new HashSet<Draggable>();
        timeCounter = 0;
        isHoldingFiles = false;
        audioSource.clip = audioManager.Audios.Steady_hum;
        audioSource.volume = 1.0f;
        audioSource.Play();
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.gameObject.layer == draggableLayer && IsPowerEnabled)
        {
            mainframeSet.Add(collision.GetComponent<Draggable>());
            //Debug.Log("Draggable inside. File counter = " + mainframeSet.Count);
            if(mainframeSet.Count >= nFilesGoal)
            {
                isHoldingFiles = true;
                audioSourceProcessing.clip = audioManager.Audios.Uploading;
                audioSource.volume = 0.5f;
                audioSourceProcessing.Play();
                StartCoroutine(FileTimer());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == draggableLayer)
        {
            mainframeSet.Remove(collision.GetComponent<Draggable>());
            //Debug.Log("Draggable outside. File counter = " + mainframeSet.Count);
            if(mainframeSet.Count < nFilesGoal)
            {
                isHoldingFiles = false;
                timeCounter = 0;
                audioSourceProcessing.Stop();
                audioSource.clip = audioManager.Audios.Steady_hum;
                audioSource.volume = 1.0f;
                audioSource.Play();
            }
        }
    }

    private IEnumerator FileTimer()
    {
        while(isHoldingFiles)
        {
            yield return new WaitForEndOfFrame();

            timeCounter += Time.deltaTime;
            if (OnProgressChanged != null)
                OnProgressChanged (timeCounter / holdTimer);

            if (timeCounter >= holdTimer)
                CompleteUpload ();
        }
    }

    private void CompleteUpload()
    {
        timeCounter = 0;
        isHoldingFiles = false;
        isComplete = true;
        if (OnComplete != null)
            OnComplete ();

        //Do UI updates
        //ResetFiles();

        //Remove files from the set
        mainframeSet.Clear();

        audioSourceProcessing.Stop();
        audioSource.clip = audioManager.Audios.Steady_hum; //Change to the level complete sfx
        audioSource.Play();
    }

    private void ResetMainframe ()
    {
        if (isComplete) return;
        timeCounter = 0;
        if (OnReset != null)
            OnReset ();
        isHoldingFiles = false;
        ResetFiles();
    }

    private void ResetFiles()
    {
        foreach(Draggable d in mainframeSet)
        {
            d.ResetPosition();
        }
        mainframeSet.Clear ();
    }

    protected override void OnPowerChanged (bool isEnabled)
    {
        //Debug.Log (isEnabled);
        base.OnPowerChanged (isEnabled);
        if (!isEnabled)
            ResetMainframe(); 
    }
}
