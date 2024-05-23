using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassBreak : MonoBehaviour
{
    public GameObject fracturedGlassPrefab;
    public float explosionForce = 50f;
    public float explosionRadius = 5f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            BreakGlass();
        }
    }

    void BreakGlass()
    {
        // Instantiate the fractured glass at the same position and rotation as the original glass
        GameObject fracturedGlass = Instantiate(fracturedGlassPrefab, transform.position, transform.rotation);
        // Apply explosion force to each piece of the fractured glass
        foreach (Transform piece in fracturedGlass.transform)
        {
            Rigidbody rb = piece.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
        // Destroy the original glass object
        Destroy(gameObject);
    }
}
