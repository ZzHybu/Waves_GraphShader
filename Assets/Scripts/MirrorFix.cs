using UnityEngine;

[ExecuteAlways]
public class MirrorFix : MonoBehaviour
{
    public RenderTexture customReflection;
    private Camera mirrorCam;

    void Update()
    {
        // Sucht die Kamera, die das Mirror-Skript heimlich generiert hat
        if (mirrorCam == null)
        {
            mirrorCam = GetComponent<Camera>();
        }

        // Zwingt die Kamera, ihr Bild auf unsere Textur zu rendern
        if (mirrorCam != null && customReflection != null)
        {
            mirrorCam.targetTexture = customReflection;
        }
    }
}
