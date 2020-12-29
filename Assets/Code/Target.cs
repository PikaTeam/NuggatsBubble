using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] float sphereRadius = 0.3f;
    [SerializeField] Color sphereColor = Color.red;
    private void OnDrawGizmos()
    {
        Gizmos.color = sphereColor;
        Gizmos.DrawSphere(transform.position, sphereRadius);
    }
}
