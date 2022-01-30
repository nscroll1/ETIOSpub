using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class BigEnemy : MonoBehaviour, Enemy
{
    [Tooltip("Animator controller of the object")]
    public Animator animator;
    [Tooltip("Player game object")]  
    public GameObject robot;
    [Tooltip("Max number of hits before die")]
    public int maxHits;
    [Tooltip("Health graphic indicator")]
    public GameObject healthIndicator;
    [Tooltip("The place to apper")]
    public GameObject spawnPlace;
    [Tooltip("Point to add to the player score when this enemy is dead")]
    public int points = 50;
    [Tooltip("The distance in wich the player is  vissible")]
    public float distance = 10f;


    private bool hit = false;
    private int hitCount = 0;
    private Component _health; //the health indicator
    private bool imDead = false;
    private bool wall = false;


    private NavMeshAgent agent;
    private Vector3 startPosition;
    private float wanderSpeed = 0.9f;
    private float wanderRange = 100f;

    public bool Wall { get => wall; set => wall = value; }

    public void addHit()
    {
        hitCount++;
    }

    public int getHits()
    {
        return hitCount;
    }

    public bool isDead()
    {
        return imDead;
    }
    // Start is called before the first frame update
    void Start()
    {
        //_health = healthIndicator.GetComponent<SpriteRenderer>();
        //SpriteRenderer _colorFill = _health.GetComponent<SpriteRenderer>();
        //_colorFill.color = new Color(25, 248, 15, 255); // Green is the start color
    }

    void Wander()
    {
        //Pick a random location within wander-range of the start position and send the agent there
        //Vector3 destination = startPosition + new Vector3(Random.Range(-wanderRange, wanderRange),
        //                                                       0,
        //                                                      Random.Range(-wanderRange, wanderRange));
        Vector3 destination = startPosition + new Vector3(wanderRange,
                                                          0,
                                                          wanderRange);
        
        agent.destination = destination;
    }
    void Awake()
    {
        if (spawnPlace != null)
            transform.position = spawnPlace.transform.position;
        agent = GetComponent<NavMeshAgent>();
        if (spawnPlace != null)
        {
            startPosition = spawnPlace.transform.position;
        }
        else
        {
            startPosition = transform.position;
        }


    }

    private bool isPlayerOnDistance(Vector3 _toSee)
    {
        return Util.isPlayerOnDistance(gameObject.transform.position, _toSee, distance);
    }

    private bool isPlayerOnSmallDistance(Vector3 _toSee)
    {
        return Util.isPlayerOnDistance(gameObject.transform.position, _toSee, 4);
    }

    private bool isPlayerOnMidDistance(Vector3 _toSee)
    {
        return Util.isPlayerOnDistance(gameObject.transform.position, _toSee, 8);
    }

    private bool isPlayerBetween(Vector3 _toSee,float _distance1, float _distance2)
    {
        return Util.isPlayerBetween(gameObject.transform.position, _toSee, _distance1, _distance2);
    }


    public void setSquareColor()
    {
        //Debug.Log(" COLOR " + _health.name);

       /* SpriteRenderer _colorFill = _health.GetComponent<SpriteRenderer>();


        if (hitCount == (maxHits / 2))
            _colorFill.color = new Color(97.9f, 82.9f, 86.0f);
        else if (hitCount >= maxHits)
            _colorFill.color = new Color(241, 0, 0);
       */
    }

    // Update is called once per frame
    void Update()
    {

        Rigidbody _robot = robot.GetComponentInChildren<Rigidbody>();
        PlayerController _rbpc = robot.GetComponent<PlayerController>();

        if (isPlayerOnDistance(robot.transform.position) & !imDead && !_rbpc.getImDead())
        {
            //transform.DOLocalMove(robot.position, 10);

            Vector3 relativePos = _robot.position - transform.position;
            bool isPlayerNear = isPlayerBetween(robot.transform.position, 1, 5);
            bool isPlayerMid = isPlayerBetween(robot.transform.position, 6, 12);


            if(isPlayerMid && !isPlayerNear)
            {
                animator.Play("Base Layer.aim");
            }
            else if (isPlayerNear)
            {
                animator.Play("Base Layer.puch");
            }
            else if (!Wall)
            {
                Vector3 _dest = new Vector3(_robot.position.x, _robot.position.y, _robot.position.z);
                //transform.DOLocalMove(new Vector3(_robot.position.x, _robot.position.y, _robot.position.z), 10);
                transform.position = Vector3.MoveTowards(transform.position, _dest, 2 * Time.deltaTime);

            }
            // the second argument, upwards, defaults to Vector3.up
            //Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            Quaternion rotation = Quaternion.LookRotation(new Vector3(relativePos.x, 0, relativePos.z), Vector3.up);

            transform.rotation = rotation;

            if (!Physics.Linecast(transform.position, _robot.position))
                wall = false;

            if (!hit && !Wall && !isPlayerNear && !isPlayerMid)
                animator.Play("Base Layer.walk");
            else if (!isPlayerNear && !isPlayerMid)
                animator.Play("Base Layer.idle");

        }
        else if (imDead)
        {
            animator.Play("Base Layer.die");
        }
        else
        {
            //animator.Play("Base Layer.idle");
            animator.Play("Base Layer.walk");
            Wander();
        }
    } 

    public void setImDead()
    {
        imDead = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("ME PEGO!!!! COLL " + collision.gameObject.name);
        if (collision.gameObject.name == "Rest2")
            hit = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ME PEGO!!!! TRI" + other.gameObject.name);
        if (other.gameObject.name == "Rest2")
            hit = true;

    }



    private void OnDestroy()
    {
        hit = true;
    }

    public int getMaxHits()
    {
        return maxHits;
    }

    public void hideWeapon()
    {

    }

    public Animator getAnimator()
    {
        return animator;
    }


    public int getValue()
    {
        return points;
    }
}
