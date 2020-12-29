using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkPoka : MonoBehaviour
{
    public string tag;
    public bool hit;
    public GameObject Mass;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == tag)
        {
            //Debug.Log("it is an expolosan");
            if (Input.GetKey(KeyCode.E))
            {
                Instantiate(Mass,transform.position,new Quaternion());
                Debug.Log("i found you");

            }
        }
    }





}
