using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookX : MonoBehaviour
{
    [SerializeField] private float _speedRotation = 1f;
     
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float _mouseX = Input.GetAxis("Mouse X");
        //Debug.Log("mouse x = " + _mouseX);
        Vector3 rotation = transform.localEulerAngles;
        rotation.y += _mouseX * _speedRotation;
        transform.localEulerAngles = rotation;
    }
}
