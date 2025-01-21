using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    private HashSet<Rigidbody> affectedBodies = new HashSet<Rigidbody>();
    private Rigidbody componentRigidbody;

    private void Start()
    {
        componentRigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            affectedBodies.Add(other.attachedRigidbody);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            affectedBodies.Remove(other.attachedRigidbody);
        }
    }

    private void Update()
    {
        foreach (Rigidbody body in affectedBodies)
        {
            Vector3 directionToPlanet = (transform.position - body.position).normalized;

            float distance = (transform.position - body.position).magnitude;
            float strength = 3 * componentRigidbody.mass * body.mass / distance;

            body.AddForce(directionToPlanet * strength);
        }
    }
}
