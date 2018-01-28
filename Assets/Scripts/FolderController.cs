using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Dragging;
using Zenject;
using Power;

public class FolderController : BaseElectricObject//, IPointerDownHandler
{
    private void Awake()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    protected override void OnPowerChanged(bool isEnabled)
    {
        IsPowerEnabled = isEnabled;
        if (isEnabled)
        {
            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
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
