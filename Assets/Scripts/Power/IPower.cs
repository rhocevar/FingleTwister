using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Power;

public abstract class IPower : MonoBehaviour
{
    [Inject] protected PowerController PowerController { get; }
    protected bool IsPowerEnabled { get; set; }
    protected abstract void OnPowerChanged(bool isEnabled);
}