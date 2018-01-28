using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Dragging;
using Zenject;
using Power;

public class FolderController : BaseElectricObject//, IPointerDownHandler
{

    protected override void OnPowerChanged(bool isEnabled)
    {
        base.OnPowerChanged (isEnabled);
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(isEnabled);
        }
    }


    //[SerializeField] private GameObject filePrefab;
    //[Inject] private IInstantiator instantiator;
    //private GameObject currentPrefab;

    //public void OnPointerDown(PointerEventData eventData)
    //{


    //Not gonna spawn files anymore, the whole folder is gonna move
    //if(!currentPrefab && IsPowerEnabled)
    //{
    //    currentPrefab = instantiator.InstantiatePrefab (filePrefab);
    //    currentPrefab.transform.position = eventData.pointerCurrentRaycast.worldPosition;
    //    //TODO: Start dragging event already
    //    //eventData.Use();
    //    //GetComponent<BoxCollider2D>().enabled = false;

    //}
    //}
}
