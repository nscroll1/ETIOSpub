using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Tooltip("Max enemies to appear")]
    public int MaximEnemys = 3; //max number of enemies
    [Tooltip("GameObject of the enemy")]
    public GameObject enemy;
    [Tooltip("The distance from the player to the base")]
    public float distance = 30f;
    //public GameObject spawnPlace;
    //
    // Start is called before the first frame update

    private List<GameObject> _enemies = new List<GameObject>();
    private int ticks = 0;
    private int currentEnemyCreated = 0;
    public int timeToWait = 10;  // time to wait between the creation of echa enemy
    private System.TimeSpan now = System.DateTime.Now.TimeOfDay;

    void Start() { 

   // transform.position = new Vector3(59.280f, 1.38918f, 99.439f);
  
  
        Vector3 newPosition = transform.position; 
        newPosition.y = 10;
        //transform.position = newPosition; 
       /* 
        for (int index = 0; index < MaximEnemys; index++)
        {
           // if (ticks >= timeToWait)
            //{
                _enemies.Add(Instantiate(enemy));
                ticks = 0;
           /* }
            else
                ticks++;
          
        }*/

        //Debug.Log(" ITEM MAX " + _enemies[1]);

    }

    public void Awake()
    {
        currentEnemyCreated = 0;
        for(int i=0;i<MaximEnemys;i++)
        {
            //_enemies[i] = new GameObject();
            _enemies.Add(Instantiate(enemy));
            _enemies[i].SetActive(false);
            _enemies[i].gameObject.transform.position = new Vector3(gameObject.transform.position.x-25,
                                                                    gameObject.transform.position.y-10,
                                                                    gameObject.transform.position.z-25);
        }
    }

    public bool isAnyPalActive()
    {
        int activePals = 0;

        for(int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i].activeSelf)
            {
                //return true;
                activePals++;
                if (activePals >= 3) return true;
            }
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {

        System.TimeSpan _seconds = System.DateTime.Now.TimeOfDay;
        int _actsec = (_seconds - now).Seconds;
        //GameObject _player = GameObject.Find("PlayerC");
        GameObject _player = GameObject.FindGameObjectWithTag("Player");
        bool _playerNear = Util.isPlayerOnDistance(gameObject.transform.position, _player.transform.position,distance);
                                            

        if (_actsec >= timeToWait && currentEnemyCreated < MaximEnemys && !_playerNear  && !isAnyPalActive())
        { 
           _enemies[currentEnemyCreated++].SetActive(true);
            _enemies[currentEnemyCreated++].gameObject.transform.position = transform.position; 
            now = System.DateTime.Now.TimeOfDay;
            Debug.Log(" ENEMIGO ACTUAL " + currentEnemyCreated);
            
        }
        Debug.Log(" FRAME SECONDS " + _seconds + " NOW " + _actsec);

        //if (currentEnemyCreated > 2) 
            //passGarbageCollector();

    }


    private void passGarbageCollector()
    {
        GameObject _enemy;
        Enemy _enemyController;

        for(int index = 0; index < MaximEnemys; index++)
        {
            
            _enemy = (GameObject)_enemies[index];
            _enemyController = (Enemy)_enemy.GetComponent("Enemy");
            Debug.Log(" ENEMIGO MUERTO?? " + _enemyController.isDead());
            if (_enemyController.isDead())
            {
                //Destroy(_enemy);
                //_enemies.RemoveAt(index);
                Destroy(_enemies[index]);

            }
        }
    }

    private void move()
    {
        // move the enemy on player direction when he see the player
       
    }


    private void OnAnimatorMove()
    {
       
    }
}
