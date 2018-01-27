using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Utils;
using Dragging;

public class MainframeController : MonoBehaviour {

    [SerializeField] private float holdTimer = 3.0f;
    [SerializeField] private uint nFilesGoal = 2;

    private HashSet<Draggable> mainframeSet;

    private LayerMask draggableLayer;
    private uint score;
    private float timeCounter;
    private bool isHoldingFiles;

    private void Awake()
    {
        draggableLayer = LayerMask.NameToLayer("Draggable");
        mainframeSet = new HashSet<Draggable>();
    }

    private void Start()
    {
        timeCounter = 0;
        score = 0;
        isHoldingFiles = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.gameObject.IsInLayer(draggableLayer))
        if(collision.gameObject.layer == draggableLayer)
        {
            mainframeSet.Add(collision.GetComponent<Draggable>());
            Debug.Log("Draggable inside. File counter = " + mainframeSet.Count);
            if(mainframeSet.Count >= nFilesGoal)
            {
                isHoldingFiles = true;
                StartCoroutine(FileTimer());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.gameObject.IsInLayer(draggableLayer))
        if (collision.gameObject.layer == draggableLayer)
        {
            mainframeSet.Remove(collision.GetComponent<Draggable>());
            Debug.Log("Draggable outside. File counter = " + mainframeSet.Count);
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
            //TODO: replace this with a progress bar
            Debug.Log("Timer running = " + timeCounter);

            if (timeCounter >= holdTimer)
            {
                UpdateScore();
            }
        }
    }

    private void UpdateScore()
    {
        score++;
        timeCounter = 0;
        isHoldingFiles = false;

        Debug.Log("Score = " + score);

        //Do UI updates

        //Remove files from the screen, or reuse them putting them back in the folder)
        foreach(Draggable d in mainframeSet)
        {
            d.transform.position = Vector3.zero;
        }

        //Remove files from the set
        mainframeSet.Clear();
    }

}
