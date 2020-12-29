using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManger : MonoBehaviour
{
    private void OnDestroy()
    {
        TargetRunner.availablefood.Remove(transform.position);
        Debug.Log("i have now after eating: " + TargetRunner.availablefood.Count);
    }

    private void Start()
    {
        
        TargetRunner.availablefood.Add(transform.position);
        Debug.Log("i have now: " +TargetRunner.availablefood.Count);

    }
}
