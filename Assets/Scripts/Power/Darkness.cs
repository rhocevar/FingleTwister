using UnityEngine;

public class Darkness : BaseElectricObject 
{
    private SpriteRenderer spriteRenderer;

    protected override void Awake()
    {
        base.Awake ();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void OnPowerChanged(bool isEnabled)
    {
        base.OnPowerChanged (isEnabled);
        spriteRenderer.enabled = !isEnabled;
    }

}
