using UnityEngine;

[ExecuteInEditMode]
public class ChromaticAberration : MonoBehaviour
{
    [SerializeField]
    private Material material;
    [SerializeField]
    [Range (0, 0.5f)]
    private float maxOffset;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }

    private void Update ()
    {
        if (!Application.isPlaying) return;
        material.SetFloat ("_RedX", Random.Range (-maxOffset, maxOffset));
        material.SetFloat ("_RedY", Random.Range (-maxOffset, maxOffset));
        material.SetFloat ("_BlueX", Random.Range (-maxOffset, maxOffset));
        material.SetFloat ("_BlueY", Random.Range (-maxOffset, maxOffset));
        material.SetFloat ("_GreenX", Random.Range (-maxOffset, maxOffset));
        material.SetFloat ("_GreenY", Random.Range (-maxOffset, maxOffset));
    }

    public void SetMaxOffset (float maxOffset)
    {
        this.maxOffset = maxOffset;
    }
}