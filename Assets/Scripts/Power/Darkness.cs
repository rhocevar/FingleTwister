using UnityEngine;

public class Darkness : BaseElectricObject 
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void OnPowerChanged(bool isEnabled)
    {
        IsPowerEnabled = isEnabled;
        if(isEnabled)
        {
            spriteRenderer.enabled = false;
        }
        else
        {
            spriteRenderer.enabled = true;
        }
    }

}
