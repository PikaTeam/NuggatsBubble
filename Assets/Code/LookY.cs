using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookY : MonoBehaviour
{
    [SerializeField] private float _speedRotation = 1f;
    [SerializeField] float limitangle = 45;
    Vector3 oldrout;
    float sum;

    void Update()
    {
        float _mouseY = Input.GetAxis("Mouse Y");
        int up =0;
        
        Debug.Log("mouse y = " + _mouseY);
        if (_mouseY>0)
        {
            up = 1;
        }
        else if (_mouseY<0)
        {
            up = 0;
        }

        Vector3 rotation = transform.localEulerAngles;
        rotation.x -= _mouseY * _speedRotation;
        
        if (Mathf.Abs(sum) < limitangle || (up==1 && sum<0) || (up==0 && sum>0))
        {
            transform.localEulerAngles = rotation;
            sum = sum + _mouseY*_speedRotation;

        }
        
    }
}
