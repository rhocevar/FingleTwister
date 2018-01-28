using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Dragging;
using Zenject;

public class FolderController : MonoBehaviour, IPointerDownHandler 
{

    [SerializeField] private GameObject filePrefab;
    [Inject]
    private IInstantiator instantiator;

    private GameObject currentPrefab;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!currentPrefab)
        {
            currentPrefab = instantiator.InstantiatePrefab (filePrefab);
            currentPrefab.transform.position = eventData.pointerCurrentRaycast.worldPosition;
            //TODO: Start dragging event already
            //eventData.Use();
            //GetComponent<BoxCollider2D>().enabled = false;

        }
    }
}
