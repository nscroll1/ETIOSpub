/***
 *  ETIOS GAME
 *  Copyright © 2021
 *  
 *  Rodrigo Emilio Perusquia Montes  <rodrigo.perusquia@xadglobal.com>
 *  Gerardo Emilio Perusquia Montes  <gerardo.perusquia@xadglobal.com>
 * 
 *  PlayerController : Controller for player interaction in the game
 *  
 ***/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public abstract class Command
    {
        public abstract void Execute();
    }

    public class JumpFunction : Command
    {
        public override void Execute()
        {
            Jump();
        }
    }

    public class TelekinesisFunction : Command
    {
        public override void Execute()
        {
            Telekinesis();
        }
    }


    public static void Telekinesis()
    {

    }

    public static void Jump()
    {

    }
     
    public static void DoMove()
    {
        Command keySpace = new JumpFunction();
        Command keyX = new TelekinesisFunction();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            keySpace.Execute();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            keyX.Execute();
        }
    }


    public CharacterController characterController;
    public float speed = 3;


    public Animator animator;

    // camera and rotation
    public Transform cameraHolder;
    public float mouseSensitivity = 2f;
    public float upLimit = -50;
    public float downLimit = 50;
    public float RotationSpeed = 5;
    public Rigidbody robot;  // Objeto del robot 
    public Camera camara;
    public GameObject spamwLocation;
    public float maxHits = 100;
    public GameObject menu;
    public Texture2D pointing;
    

    // gravity
    private float gravity = 9.87f;
    private float verticalSpeed = 0;
    private CharacterController player;
    private Boolean go_north = true;
    private Boolean go_south = false;
    private Boolean go_east = false;
    private Boolean go_west = false;
    private Boolean fadeIn = false;
    private Boolean fadeOut = false;
    private Boolean isJumping = false;
    private Boolean isRunning = false;
    private float turnSpeed = 5f;
    private float scuttleSpeed = 15f;
    private Color _mcolor;
    private float _factor; // acercamiento o alejamiento de la camara
    private GUI interfaz;
    private GameObject arma;
    private bool imDead = false;
    private float hitCount=0;
    private int points = 0;  // the score acumulator
    private bool paused = false;
    private float xmove, zmove;

    public int Points { get => points; set => points = value; }



    private void actHealthPercent()
    {
        GameObject _healthGO = GameObject.Find("HealthLevelText");
        Text _healthText = _healthGO.GetComponent<Text>();

        float _per;

        if (hitCount > 0)
        {
            _per = 100f - (hitCount / maxHits) * 100;
            Debug.Log(" HEALTH PERCENT " + _per.ToString() + " hits " + hitCount);
        }
        else
        {
           _per = 100f;
        }
         

        _healthText.text = _per.ToString() + "%";
    }

    private void setCardinal(String _pCardinal)
    {
        if (_pCardinal == "north")
        {
            go_north = true;
            go_south = go_east = go_west = false;
        }
        else if (_pCardinal == "south")
        {
            go_south = true;
            go_north = go_west = go_east = false;
        }
        else if (_pCardinal == "east")
        {
            go_east = true;
            go_west = go_north = go_south = false;
        }
        else if (_pCardinal == "west")
        {
            go_west = true;
            go_east = go_south = go_north = false;

        }
    }

    private void PlayerAction()
    {
        //TextMeshPro _ltext = GameObject.Find("Bienvenida").GetComponent<TextMeshPro>();


        if (!imDead)
        {
            Move();
            keyMove();
            Rotate();
            actHealthPercent();
            updateGUIPointsLevel();
        }
        else
        {
            animator.Play("Base Layer.die");
            //UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Primero");
            menu.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        Debug.Log(" FADE OUT " + fadeOut);
      /*  if (fadeOut)
        {
            fade(_ltext.color);
        }*/
        //Instantiate(robot);  //
    }

    void Update()
    {
        PlayerAction();

    }


    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.C) && IsGrounded())
        {
            animator.SetBool("isJumping", true);
            animator.Play("Base Layer.jump");
            //if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.45f) 
            jump();
        }

    }


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Points = 0;
        robot.detectCollisions = true;
        xmove = robot.transform.position.x;
        zmove = robot.transform.position.z;


    }



    private void jump()
    {
       
        if (!isJumping)
        {
            //robot.useGravity = true;
            //robot.isKinematic = false;
            //robot.transform.Translate(new Vector3(0,10 * Time.fixedDeltaTime, 0), Space.World);
            //robot.velocity = new Vector3(0, 10 * Time.fixedDeltaTime, 0);
            //robot.GetComponent<Rigidbody>().AddForce(Vector3.up * 10,ForceMode.Impulse);
           
            
            //robot.AddForce(robot.transform.up * 20f);
            robot.transform.DOLocalJump(new Vector3(robot.transform.localPosition.x,
                                                    robot.transform.localPosition.y * Time.deltaTime,
                                                    robot.transform.localPosition.z),2f, 1, 0.8f);

            

            isJumping = true;
        }
        else
        {
            //robot.useGravity    = true;
            //robot.isKinematic   = true;
            isJumping = false;
        }
    }
   
    public void Rotate()
    {
        float horizontalRotation = Input.GetAxis("Mouse X");
        float verticalRotation = Input.GetAxis("Mouse Y");




        /*robot.transform.Rotate((Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime),
                               (Input.GetAxis("Mouse Y") * RotationSpeed * Time.deltaTime),
                                0, Space.World);
        robot.transform.Rotate( new Vector3(Input.GetAxis("Mouse X"), 0, 0) * 
                                Time.deltaTime * speed);
       */
        if (Input.GetMouseButton(0)) {

            GameObject.Find("ETIOS").GetComponent<Animator>().enabled = false;
            GameObject _spine = GameObject.Find("mixamorig:Spine");
            float angle = Mathf.Atan2(verticalRotation, horizontalRotation) * Mathf.Rad2Deg;
            _spine.transform.Rotate(-(verticalRotation * mouseSensitivity), horizontalRotation * mouseSensitivity,0);
           
        }
        else
        {
            transform.Rotate(0, horizontalRotation * mouseSensitivity, 0);
        }
        //cameraHolder.Rotate(-verticalRotation * mouseSensitivity, 0, 0);
        //robot.transform.Rotate(0, horizontalRotation * mouseSensitivity, 0);

        //Vector3 currentRotation = cameraHolder.localEulerAngles;
        //Vector3 currentRotation = robot.transform.localEulerAngles;
        //if (currentRotation.x > 180) currentRotation.x -= 360;
        //currentRotation.x = Mathf.Clamp(currentRotation.x, upLimit, downLimit);
        //cameraHolder.localRotation = Quaternion.Euler(currentRotation);
        //robot.transform.localRotation = Quaternion.Euler(currentRotation); */
    }

    private void Move()
    {

        arma.SetActive(false);
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");
        float actSpeed = speed;

        animator.SetBool("isWalking", verticalMove != 0 || horizontalMove != 0);
        animator.SetBool("isJumping", isJumping);
        Debug.Log("JUMP AGAIN!!! " + isJumping);
        GameObject.Find("ETIOS").GetComponent<Animator>().enabled = true;

        if ((verticalMove != 0 || horizontalMove != 0) && Input.GetKey(KeyCode.LeftShift)
                                                       && !Input.GetKey(KeyCode.V))
        {
            animator.SetBool("isRunning", true);
            animator.Play("Base Layer.run");
            if (!isRunning)
            {

                isRunning = true;
            }
            actSpeed = speed + 4;
        }
        else if ((verticalMove != 0 || horizontalMove != 0) && Input.GetKey(KeyCode.V))
        {
            PlayerWeapon _pw = GetComponent<PlayerWeapon>();
            Debug.Log(" ARMA [" + _pw.getWeaponInUse().name + "]");
            animator.SetBool("isWalking", true);
            if (_pw.getWeaponInUse().tag.Equals("BigArma"))
            {
                animator.Play("Base Layer.walk-aim-big");
            } else if(_pw.getWeaponInUse().tag.Equals("Arma"))
            {
                animator.Play("Base Layer.walk-aim");
            }
            actSpeed = speed;
            isRunning = false;
            arma.SetActive(true);
        }
        else if (Input.GetKey(KeyCode.V))
        {
            PlayerWeapon _pw = GetComponent<PlayerWeapon>();

            animator.SetBool("isAiming", true);
            if (_pw.getWeaponInUse().tag.Equals("Arma"))
            {
                animator.Play("Base Layer.aim");
            }
            else if(_pw.getWeaponInUse().tag.Equals("BigArma"))
            {
                animator.Play("Base Layer.idle-aim-big");
            }
            arma.SetActive(true);
        }
        else if (Input.GetKey(KeyCode.H))
        {
            GameObject textCanvas = GameObject.Find("TextCanvas");
            GameObject textBienvenida = GameObject.Find("Bienvenida");
            textCanvas.SetActive(false);
            textBienvenida.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            if (!paused)
            {
                menu.SetActive(true);
                Time.timeScale = 0;
                paused = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            } else
            {
                menu.SetActive(false);
                Time.timeScale = 1;
                paused = false;
                Cursor.visible = false;
            }
        }
        else if (verticalMove != 0 || horizontalMove != 0)
        {
                
            animator.SetBool("isWalking", true);
            animator.Play("Base Layer.walk");
            actSpeed = speed;
            isRunning = false;
        }
        else if(!Input.GetKeyDown(KeyCode.C) && IsGrounded())
            animator.Play("Base Layer.idle");

        if (Input.GetKeyDown(KeyCode.B))
        {
          
            Cursor.visible = !Cursor.visible;
            if(pointing!=null)
                Cursor.SetCursor(pointing,Vector2.zero,CursorMode.Auto);
            Cursor.lockState = CursorLockMode.None;
        }
        if (characterController.isGrounded)
        {
            verticalSpeed = 0;
            //isJumping = false;
        }
        else verticalSpeed -= gravity * Time.deltaTime;
        Vector3 gravityMove = new Vector3(0, verticalSpeed, 0);

        //if ( !Input.GetKey(KeyCode.V)) // if jump or fire the weapon does not move
        //{
        Vector3 move = transform.forward * verticalMove + transform.right * horizontalMove;
        characterController.Move(actSpeed * Time.deltaTime * move + gravityMove * Time.deltaTime);


        //characterController.Move(actSpeed * Time.deltaTime * move * Time.deltaTime);

        //} 

        Debug.Log(" Resultado del or [" + (verticalMove != 0 || horizontalMove != 0) + "] !!!");
    }


    private void rotateToMousePointer(GameObject target)
    {
        //Get the Screen positions of the object
        Vector2 positionOnScreen = camara.WorldToViewportPoint(target.transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)camara.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        //float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        float angle = Mathf.Atan2(positionOnScreen.y - mouseOnScreen.y, positionOnScreen.x - mouseOnScreen.x) * Mathf.Rad2Deg;

        //Ta Daaa
        target.transform.rotation = Quaternion.Euler(new Vector3(angle, angle, 0f));
    }


    private void updateGUIPointsLevel()
    {
        GameObject _gui = GameObject.Find("PointsText"); // Dock front of the GUI
        Text _textValue = _gui.GetComponent<Text>();

        _textValue.text = points.ToString();


    }


    private bool IsGrounded()
    {
        
        Collider colider = GameObject.Find(robot.name).GetComponent<MeshCollider>();

        RaycastHit info;

        //Debug.DrawRay(colider.bounds.center, Vector3.down * colider.bounds.extents.y);

        if (Physics.Raycast(colider.bounds.center, Vector3.down,out info)) return true;
        Debug.Log(" DESPUES SALTO " + info.collider);

       
       
        return false;
    }
    private void keyMove()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) ||
            Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
        {
            fadeOut = true;
            if (Input.GetKeyDown(KeyCode.LeftArrow) && go_north)
            {
                setCardinal("east");
                robot.transform.Rotate(90 * Vector3.down, Space.World);

            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && go_south)
            {
                setCardinal("east");
                robot.transform.Rotate(90 * Vector3.up, Space.World);
                //robot.transform.Rotate(90 * Vector3.down, Space.World);

            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && go_west)
            {
                setCardinal("east");
                robot.transform.Rotate(180 * Vector3.down, Space.World);

            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && go_east)
            {
                setCardinal("west");
                //robot.transform.Rotate(180 * Vector3.down, Space.World);
                robot.transform.Rotate(180 * Vector3.down, Space.World);

            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && go_north)
            {
                setCardinal("west");
                robot.transform.Rotate(90 * Vector3.up, Space.World);

            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && go_south)
            {
                setCardinal("west");
                robot.transform.Rotate(90 * Vector3.down, Space.World);
                //robot.transform.Rotate(90 * Vector3.up, Space.World);

            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && go_south)
            {
                setCardinal("north");
                robot.transform.Rotate(180 * Vector3.up, Space.World);

            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && go_east)
            {
                setCardinal("north");
                robot.transform.Rotate(90 * Vector3.up, Space.World);

            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && go_west)
            {
                setCardinal("north");
                robot.transform.Rotate(90 * Vector3.down, Space.World);

            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && go_north)
            {
                setCardinal("south");
                robot.transform.Rotate(180 * Vector3.up, Space.World);

            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && go_east)
            {
                setCardinal("south");
                robot.transform.Rotate(90 * Vector3.down, Space.World);
                //robot.transform.Rotate(90 * Vector3.up, Space.World);

            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && go_west)
            {
                setCardinal("south");
                robot.transform.Rotate(90 * Vector3.up, Space.World);
                //robot.transform.Rotate(90 * Vector3.down, Space.World);

            }
            else if (Input.GetKey(KeyCode.Z))
            {
                Debug.Log("PLUS!!!" + _factor);
                _factor += 5;
                camara.fieldOfView = _factor;
                

            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                Debug.Log("PLUS!!!" + _factor);
                _factor -= 5;
                camara.fieldOfView = _factor; 
            }
        }

    }

    public void Start()
    {
        //robot = GameObject.Find("robotblender.001");
        //robot  = GameObject.Find("Robot").GetComponent<Rigidbody>();
        //camara = GameObject.Find("Camera").GetComponent<Camera>();
        _factor = camara.transform.position.y;
        //interfaz = GameObject.Find("GUI").GetComponent<GUI>();
        //player = GameObject.Find("Player").GetComponent<CharacterController>();
        player = characterController;
        robot.freezeRotation = false;
        arma = GetComponent<PlayerWeapon>().getWeaponInUse();
        transform.position = spamwLocation.transform.position;



    }


    private void fade(Color _pFrom)
    {
        // la idea es que haga un made sobre los letreros cuando te muevas
        float fadeAmount = _mcolor.a - (10 * Time.deltaTime);

        _pFrom = new Color(_mcolor.r, _mcolor.g, _mcolor.b, 20);
        _mcolor = _pFrom;

        if (_pFrom.a < 0)
        {
            fadeOut = false;
        }
        Debug.Log("FADE OUT");
    }


    public void addHit()
    {
        hitCount++;
    }

    public void addHit(int _hitsToAdd)
    {
        hitCount += _hitsToAdd;
    }

    public void setImDead()
    {
        imDead = true;
    }

    public bool getImDead()
    {
        return imDead;
    }

    public float getHits()
    {
        return hitCount;
    }

    public float getMaxHits()
    {
        return maxHits;
    }


    public void hideWeapon()
    {
       arma.SetActive(false);
    }

    private void addHealth(int _healthToAdd)
    {
          
        if(hitCount>0)
        {
            if(hitCount-_healthToAdd<=0)
            {
                hitCount = 0;
            }
            else
                hitCount -= _healthToAdd;
        }
    }

    private void addBigBarrelBullets(int _value)
    {
        PlayerWeapon _pw = GetComponent<PlayerWeapon>();
        PlayerWeapon.MyWeapon _pwmy = _pw.GetMyWeapon("BigArma");
        BigBarrelWeapon _pwgo = _pwmy.weapon.GetComponent<BigBarrelWeapon>();

        /*
        if (_pwgo.mags + _value > _pwgo.maxMags) 
        {
            _pwgo.mags = _pwgo.maxMags;
            //_pwgo.setNumberOfBullets(_pwgo.bulletsPerMag*_value);
           
        }
        else
        {
            _pwmy.mags+=_value;
            _pwgo.mags+=_value;
        }*/
        _pwgo.setNumberOfBullets(_pwgo.bulletsPerMag * _value);

    }

    private void addDoubleBarrelBullets(int _value)
    {
        PlayerWeapon _pw = GetComponent<PlayerWeapon>();
        PlayerWeapon.MyWeapon _pwmy = _pw.GetMyWeapon("Arma");
        DoubleBarrelWeapon _pwob = _pwmy.weapon.GetComponent<DoubleBarrelWeapon>();

       /* if (_pwob.mags + _value >= _pwob.maxMags)
        {
            _pwob.mags = _pwob.maxMags;
        } else
        {
            _pwmy.mags++;
            _pwob.mags++;

        }*/
        _pwob.setNumberOfBullets(_pwob.bulletsPerMag * _value);


    }


    public void addItem(String _name,int _value) {

        if (_name.Equals("Health"))
        {
            addHealth(_value);
        } else if(_name.Equals("BigBarrelBullets"))
        {
            addBigBarrelBullets(_value);
        } else if (_name.Equals("DoubleBarrelBullets"))
        {
            addDoubleBarrelBullets(_value);
        }

    }

}
