using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update

    public Button[] _opciones;
    public GameObject menuColiseo;


    void Start()
    {


    }


    private void addListeners()
    {
        _opciones[0].onClick.AddListener(MyOpcionUno);
        _opciones[1].onClick.AddListener(MyOpcionDos);
        _opciones[2].onClick.AddListener(MyOpcionTres);

    }



    private void Awake()
    {
        addListeners();
        menuColiseo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void MyOpcionUno()
    {
        Debug.Log("NEW GAME ON CLICK");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Nivel1Ambientado");
    }

    private void MyOpcionTres()
    {
        Debug.Log("EXIT ON CLICK");
        Application.Quit();
    }

    private void MyOpcionDos()
    {

        menuColiseo.SetActive(true);
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/coliseo2");
        Debug.Log(" MENU MAIN FALSE " + gameObject.name);
    }


    private void KeyContoller()
    {
        if (Input.GetKeyDown(KeyCode.X))
            gameObject.SetActive(false);
    }
}
