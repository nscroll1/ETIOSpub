/***
 *  <sumary>
 *  ETIOS GAME
 *  Copyright © 2021
 *  
 *  Rodrigo Emilio Perusquia Montes  <rodrigo.perusquia@xadglobal.com>
 *  Gerardo Emilio Perusquia Montes  <gerardo.perusquia@xadglobal.com>
 * 
 *  SmallEnemy : Controller for Small Enemy interaction in the game
 *  </sumary>
 * 
 ***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class SmallEnemy : MonoBehaviour, Enemy
{
    [Tooltip("The animator of the enemy movements")]
    public Animator animator;
    [Tooltip("The Player to atack")]
    public GameObject robot;
    [Tooltip("Maximun number of hits before die")]
    public int maxHits;
    [Tooltip("Health indicator")]
    public GameObject healthIndicator;
    [Tooltip("Where to appear")]
    public GameObject spawnPlace;
    [Tooltip("The weapon to use")]
    public GameObject weapon;
    [Tooltip("Number of points to add to the player score when the enemy is dead")]
    public int points = 10;
    [Tooltip("Speed of the enemy less value is more speed")]
    public int speed = 30;
    [Tooltip("The distance between the player and the enemy")]
    public int distance = 10;
    
    private bool hit = false;
    private int hitCount = 0;
    private Component _health; //the health indicator
    private bool imDead = false;
    //private GameObject arma;

    /* AI Eenemy movement vars */
    private NavMeshAgent agent;
    private Vector3 startPosition;
    private float wanderSpeed = 0.9f;
    private float wanderRange = 100f;
    private bool chasing = false;
    private bool wall = false;
    private System.TimeSpan startdyng;


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
        _health = healthIndicator.GetComponent<SpriteRenderer>();
        SpriteRenderer _colorFill = _health.GetComponent<SpriteRenderer>();
        _colorFill.color = new Color(25, 248, 15, 255); // Green is the start color
                                                        //arma = GameObject.Find("ArmaE");
                                                        //arma.SetActive(true);
        transform.position = spawnPlace.transform.position;
        weapon.SetActive(true);

    }

    void Awake()
    {
        //arma = GameObject.Find("ArmaE");

        healthIndicator.SetActive(true);
        Debug.Log(" ARMA " + weapon);

        agent = GetComponent<NavMeshAgent>();
        agent.speed = wanderSpeed;
        if (spawnPlace != null) {
            startPosition = spawnPlace.transform.position;
            
        }
        else
        {
            startPosition = transform.position;
        }
        //InvokeRepeating("Wander", 1f, 5f);
    }

    void Wander()
    {
        //Pick a random location within wander-range of the start position and send the agent there
//        Vector3 destination = startPosition + new Vector3(Random.Range(-wanderRange, wanderRange),
  //                                                        0,
    //                                                      Random.Range(-wanderRange, wanderRange));
        Vector3 destination = startPosition + new Vector3(wanderRange,
                                                          robot.transform.position.y,
                                                          wanderRange);

        agent.destination = destination;
    }

    private bool isPlayerOnDistance(Rigidbody _toSee)
    {

        /* float targetDistance = Vector2.Distance(_toSee.transform.position, transform.position);
         Debug.Log("DISTANCIAS :" + targetDistance);
         if (targetDistance < 10f)
             return true;*/
       
        return Util.isPlayerOnDistance(transform, _toSee.transform,(float)distance);        
    }

    private bool isPlayerOnDistance(Vector3 _toSee)
    {
        return Util.isPlayerOnDistance(gameObject.transform.position, _toSee,distance);
    }

    private bool isPLayerVisible(GameObject _toSee)
    {

        Vector2 direction = _toSee.transform.position - transform.position;
        float angle = Vector2.Angle(direction, transform.up * -1);
        float targetDistance = Vector2.Distance(_toSee.transform.position, transform.position);
        if (targetDistance < 30f)
        {
            /* if (angle < m_Attack1FieldofViewAngle * 0.5f && targetDistance < m_Attack1FieldofViewRadius)
             {
                 return true;
             }*/

        }

        return false;
    }

    public void setSquareColor()
    {
        Debug.Log(" COLOR " + _health.name);

        SpriteRenderer _colorFill = _health.GetComponent<SpriteRenderer>();


        if (hitCount == (maxHits / 2))
            _colorFill.color = new Color(97.9f, 82.9f, 86.0f);
        else if (hitCount >= maxHits)
            _colorFill.color = new Color(241, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        
        EnemyMove();
        disactiveEnemy();

    }


    private void EnemyMove()
    {
         
        Rigidbody _robot = robot.GetComponentInChildren<Rigidbody>();
        PlayerController _rbpc = robot.GetComponent<PlayerController>();


        Debug.Log(" VALORES " + isPlayerOnDistance(robot.transform.position) + " " + imDead  + " " +_rbpc.getImDead());
        if (isPlayerOnDistance(robot.transform.position) && !imDead && !_rbpc.getImDead())
        {
            //transform.DOLocalMove(robot.position, 10);

            Vector3 relativePos = _robot.position - transform.position;
            weapon.SetActive(true);


            if (!wall)
            {

               
                Vector3 _dest = new Vector3(_robot.position.x, _robot.position.y, _robot.position.z);  

                //transform.DOLocalMove(_dest, (float)speed);
                transform.position = Vector3.MoveTowards(transform.position, _dest, (float)speed * Time.deltaTime);
            
                // the second argument, upwards, defaults to Vector3.up
                //Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

                if (!hit)
                    animator.Play("Base Layer.walk-aim");
            }
            Quaternion rotation = Quaternion.LookRotation(new Vector3(relativePos.x, 0, relativePos.z), Vector3.up);

            transform.rotation = rotation;
            RaycastHit info;
            if (!Physics.Linecast(transform.position, _robot.position,out info))
                wall = false;
            try
            {
                if (info.collider.tag.Equals("Player"))
                    wall = false;
            } catch (System.Exception e)
            {

            }
        //    transform.position = new Vector3(transform.position.x, _robot.position.y, transform.position.z);

           // Debug.Log(" VALOR DE WALL " + wall + " PHYSICS " + Physics.Linecast(transform.position, _robot.position) + " INFO " + info.collider.name) ;



        }
        else if (!imDead)
        {
            animator.Play("Base Layer.walk");
            weapon.SetActive(false);
            Wander();
        }
        else
        {
            transform.position = new Vector3(transform.position.x, _robot.position.y, transform.position.z);
            animator.Play("Base Layer.die");
        }
    }

    public void setImDead()
    {
        imDead = true;
         startdyng = System.DateTime.Now.TimeOfDay;
    }

    private void disactiveEnemy()
    {
        int _seconds = (System.DateTime.Now.TimeOfDay - startdyng).Seconds;

        if(_seconds > 1 && imDead)
        {
            gameObject.SetActive(false);

        }


    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("ME PEGO!!! " + collision.gameObject.name);
        /*if (collision.gameObject.name == "Rest2")
            hit = true;*/
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
        SmallEnemyWeapon _ew = GetComponent<SmallEnemyWeapon>();
        _ew.fire = false;
        weapon.SetActive(false);
        healthIndicator.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {

        Debug.Log(" COLLIDER SMALL ENEMY " + other.tag + " NOMBRE " + other.name + " NOMBRE " + gameObject.name );
        if (other.tag.Equals("Enemigo"))
        { 
            Rigidbody _rb = GetComponent<Rigidbody>();
            _rb.Sleep();
        } else if(other.tag.Equals("Wall"))
        {
            wall = true;
        }

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
