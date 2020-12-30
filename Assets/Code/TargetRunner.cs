using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]

public class TargetRunner : MonoBehaviour
{
    enum ModeSwitching { None, FoodLanding, FoodEating, TargetCapture ,Coward };
    [SerializeField] private ModeSwitching TargetRunnerMode;

    [Tooltip("Minimum time to wait at target between running to the next target")]
    [SerializeField] private float minWaitAtTarget = 7f;

    [Tooltip("Maximum time to wait at target between running to the next target")]
    [SerializeField] private float maxWaitAtTarget = 15f;


    [Tooltip("A game object whose children have a Target component. Each child represents a target.")]
    [SerializeField] private Transform targetFolder = null;
    private Target[] allTargets = null;

    //all types of food that growlith can put on the ground
    [SerializeField] public GameObject [] foodis;


    //the last food positon + array with all the available food on the ground now
    public Vector3 currentfood ;
    public static List<Vector3> availablefood = new List<Vector3>();

    //the points recorders.
    public static int yourscrore = 0;
    public static int rodisscrore = 0;


    //we make just one curutirtion 
    public bool curstart = false;


    [Header("For debugging")]
    [SerializeField] private Target currentTarget;
    [SerializeField] private float timeToWaitAtTarget = 0;
    private GameObject currentFoodie;

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private float rotationSpeed = 5f;
    private float timeToChangeDirection;
    private Vector3 CurrentTarge;



    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        allTargets = targetFolder.GetComponentsInChildren<Target>(false); // get components in active children only
        Debug.Log("Found " + allTargets.Length + " targets.");
        SelectNewTarget();
    }

    public void GoldThrower()
    {
        currentFoodie = foodis[Random.Range(0, foodis.Length)];
        Instantiate(currentFoodie, transform.position, new Quaternion());
        //availablefood.Add(transform.position);
      

    }




    private void SelectNewTarget()
    {
        currentTarget = allTargets[Random.Range(0, allTargets.Length - 1)];
        //Debug.Log("New target: " + currentTarget.name);
        navMeshAgent.SetDestination(currentTarget.transform.position);
        //if (animator) animator.SetBool("Run", true);
        timeToWaitAtTarget = Random.Range(minWaitAtTarget, maxWaitAtTarget);
    }


    private void SelectNewFoodTarget()
    {
        
        if (availablefood.Count>0)
        {
            currentfood = availablefood[0];

            Debug.Log("New target is ~~~~~~: " + currentfood); 
            navMeshAgent.SetDestination(currentfood);

            //if (animator) animator.SetBool("Run", true);
            timeToWaitAtTarget = Random.Range(minWaitAtTarget, maxWaitAtTarget);
        }
        

    }

    private void SelectNewDest ()
    {
        float maxdis = 12;
        float mindis = 2;
        float x = UnityEngine.Random.Range(-maxdis+ mindis, maxdis- mindis) + mindis;
        float z = UnityEngine.Random.Range(-maxdis+ mindis, maxdis- mindis) + mindis;

         

        Vector3 dir = new Vector3(x, 0, z);
        NavMeshHit hit;
        bool close=false;
        /*while (close != true)
        {
            close=NavMesh.SamplePosition(dir+ transform.position, out hit, 3, NavMesh.AllAreas);
            if (close==true)
            {
                Debug.Log("im in new dest");
                navMeshAgent.SetDestination(hit.position);
                Debug.Log("im our from the new dest");
            }
            
        }*/
        navMeshAgent.SetDestination(dir+transform.position);


    }

    public IEnumerator Foodput()
    {
        while (true)
        {
            if (navMeshAgent.hasPath)
            {
                FaceDestination();

            }
            else
            {
                timeToWaitAtTarget -= Time.deltaTime;
                if (timeToWaitAtTarget <= 0)
                {
                    SelectNewDest();
                    //Debug.Log("i going throw someting");
                    GoldThrower();
                    //Debug.Log("bla bla bla");
                }

            }
            yield return new WaitForSeconds(4);
        }
    }


    private void Update()
    {
        if (TargetRunnerMode != ModeSwitching.None)
        {
            switch (TargetRunnerMode)
            {
                //the food landing one, go's on all over the map and landing
                //the food.
                case ModeSwitching.FoodLanding:
                    if (curstart == true)
                    {
                        break;
                    }

                    StartCoroutine(Foodput());
                    curstart = true;
                    break;

                //the food eating one, go's after the food that the landing on put 
                //and eat it before the player.
                case ModeSwitching.FoodEating:
                    if (navMeshAgent.hasPath)
                    {
                        FaceDestination();
                    }
                    else
                    {  
                        timeToWaitAtTarget -= Time.deltaTime;
                        if (timeToWaitAtTarget <= 0)
                        {
                            if (!availablefood.Contains(currentfood))
                            {
                                SelectNewFoodTarget();
                            }
                        }
                            
                    }


                    break;


                //the target capture one are going after the player
                //and try to capture him.
                case ModeSwitching.TargetCapture:
                    if (navMeshAgent.hasPath)
                    {
                        FaceDestination();
                    }
                    else
                    {   // we are at the target
                        //if (animator) animator.SetBool("Run", false);
                        timeToWaitAtTarget -= Time.deltaTime;
                        if (timeToWaitAtTarget <= 0)
                            SelectNewTarget();
                    }

                    break;
            }
        }

   }

    private void FaceDestination()
    {
        Vector3 directionToDestination = (navMeshAgent.destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToDestination.x, 0, directionToDestination.z));
        //transform.rotation = lookRotation; // Immediate rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed); // Gradual rotation
    }

    /*private Vector3 TargegetRandomDir ()
    {
        
        navMeshAgent.SetDestination(currentTarget.transform.position);
    }*/



}
