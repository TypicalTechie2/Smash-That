using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassBreak : MonoBehaviour
{
    private Rigidbody[] rigidbodies;

    void Start()
    {
        // Get all the rigidbodies of the children
        rigidbodies = GetComponentsInChildren<Rigidbody>();

        // Freeze all the rigidbodies
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball") || other.gameObject.CompareTag("Player"))
        {
            // Unparent child objects
            foreach (Transform child in transform)
            {
                child.parent = null;

                Destroy(child.gameObject, 5f);
            }

            Destroy(gameObject);

            // Unfreeze all the rigidbodies
            foreach (Rigidbody rb in rigidbodies)
            {
                rb.constraints = RigidbodyConstraints.None;
                rb.AddExplosionForce(100f, transform.position, 100f); // Adjust force and radius as needed
                Destroy(rb.gameObject, 2.5f);
            }
        }
    }
}
