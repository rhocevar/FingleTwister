using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Power;

public class BaseElectricObject : MonoBehaviour
{
    [Inject]
    protected PowerController powerSystem;
    protected bool IsPowerEnabled { get; private set; }

    protected virtual void Awake ()
    {
        powerSystem.OnPowerChanged += OnPowerChanged;
    }

    protected virtual void OnPowerChanged (bool isEnabled)
    {
        IsPowerEnabled = isEnabled;
    }

    protected virtual void OnDestroy()
    {
        powerSystem.OnPowerChanged -= OnPowerChanged;
    }
}