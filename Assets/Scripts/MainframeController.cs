using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class MainframeController : MonoBehaviour {

    private LayerMask draggableLayer;

    private void Awake()
    {
        draggableLayer = LayerMask.NameToLayer("Draggable");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.IsInLayer(draggableLayer))
        {
            Debug.Log("Draggable inside");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.IsInLayer(draggableLayer))
        {
            Debug.Log("Draggable outside");
        }
    }

}
