using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Utils;
using Dragging;
using Power;
using Zenject;
using System;

public class MainframeController : BaseElectricObject
{
    public event Action<float> OnProgressChanged;
    public event Action OnComplete;
    public event Action OnReset;

    [SerializeField] private float holdTimer = 3.0f;
    [SerializeField] private uint nFilesGoal = 2;


    private LayerMask draggableLayer;
    private float timeCounter;
    private bool isHoldingFiles;
    HashSet<Draggable> mainframeSet;
    private bool isComplete;

    private void Awake()
    {
        draggableLayer = LayerMask.NameToLayer("Draggable");
        mainframeSet = new HashSet<Draggable>();
    }

    protected override void Start ()
    {
        base.Start ();
        timeCounter = 0;
        isHoldingFiles = false;
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
        ResetFiles();

        //Remove files from the set
        mainframeSet.Clear();
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
        mainframeSet.Clear ();
    }

    protected override void OnPowerChanged (bool isEnabled)
    {
        base.OnPowerChanged (isEnabled);
        if (!isEnabled)
            ResetMainframe(); 
    }
}
