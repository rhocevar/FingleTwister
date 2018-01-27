using System.Collections;
using UnityEngine;
//using Utils;

public class MainframeController : MonoBehaviour {

    [SerializeField] private float holdTimer = 3.0f;
    [SerializeField] private uint nFilesGoal = 2;

    private LayerMask draggableLayer;
    private uint filesInMainframe;
    private uint score;
    private float timeCounter;
    private bool isHoldingFiles;

    private void Awake()
    {
        draggableLayer = LayerMask.NameToLayer("Draggable");
    }

    private void Start()
    {
        filesInMainframe = 0;
        timeCounter = 0;
        score = 0;
        isHoldingFiles = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.gameObject.IsInLayer(draggableLayer))
        if(collision.gameObject.layer == draggableLayer)
        {
            filesInMainframe++;
            Debug.Log("Draggable inside. File counter = " + filesInMainframe);
            if(filesInMainframe >= nFilesGoal)
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
            filesInMainframe--;
            Debug.Log("Draggable outside. File counter = " + filesInMainframe);
            if(filesInMainframe < nFilesGoal)
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
        //Remove files from the screen
    }

}
