using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFood : MonoBehaviour
{
    public string tag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == tag)
        {
            Destroy(other.gameObject);
            //TargetRunner.availablefood.RemoveAt(0);

        }
    }



}
