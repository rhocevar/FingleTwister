using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Dragging;

public class FolderController : MonoBehaviour, IPointerDownHandler {

    [SerializeField] private GameObject filePrefab;

    private GameObject currentPrefab;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!currentPrefab)
        {
            currentPrefab = Instantiate(filePrefab, transform.position, transform.rotation);
            //TODO: Start dragging event already
            //eventData.Use();
            //GetComponent<BoxCollider2D>().enabled = false;

        }
    }
}
