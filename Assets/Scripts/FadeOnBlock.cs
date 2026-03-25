using UnityEngine;

public class FadeOnBlock : MonoBehaviour
{
    public Transform target;
    public LayerMask wallLayer;
    public float fadeAlpha = 0.3f;

    private Renderer lastRenderer;
    private Color originalColor;

    void Update()
    {
        Ray ray = new Ray(transform.position, target.position - transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, wallLayer))
        {
            Renderer r = hit.collider.GetComponent<Renderer>();

            if (r != null)
            {
                if (lastRenderer != r)
                {
                    ResetLast();
                    lastRenderer = r;
                    originalColor = r.material.color;
                }

                Color c = r.material.color;
                c.a = fadeAlpha;
                r.material.color = c;
            }
        }
        else
        {
            ResetLast();
        }
    }

    void ResetLast()
    {
        if (lastRenderer != null)
        {
            lastRenderer.material.color = originalColor;
            lastRenderer = null;
        }
    }
}