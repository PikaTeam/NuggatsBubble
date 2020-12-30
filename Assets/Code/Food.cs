using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public string tag;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == tag)
        {
            Debug.Log("it is food");
            if (Input.GetKey(KeyCode.F))
            {
                Debug.Log("yammy");
                Destroy(gameObject);
                TargetRunner.yourscrore = TargetRunner.yourscrore + 10;
                //TargetRunner.availablefood.Remove(other.transform.position);

            }
        }
    }
}
