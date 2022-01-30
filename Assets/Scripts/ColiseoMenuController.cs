using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColiseoMenuController : MonoBehaviour
{
    public Button[] _opciones;
    public GameObject mainMenu;

    private int opcion = 0;

    void Start()
    {
        

    }

    private void Awake()
    { 
        
        addListeners();
       
    }


    private void addListeners()
    {
        _opciones[0].onClick.AddListener(MyOpcionUno);
        _opciones[1].onClick.AddListener(MyOpcionDos);
        _opciones[2].onClick.AddListener(MyOpcionTres);
        _opciones[3].onClick.AddListener(MyOpcionCuatro);


    }

    // Update is called once per frame
    void Update()
    {
        switch (opcion)
        {
            case 0: mainMenu.SetActive(false); break;
            case 3: mainMenu.SetActive(true);
                    gameObject.SetActive(false);
                    break;

        }

    }

    public void MyOpcionUno()
    {
        Debug.Log(" OPCION UNO ");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/coliseo2");
    }


    public void MyOpcionDos()
    {
        Debug.Log(" OPCION DOS ");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Coliseo3");
    }

    public void MyOpcionCuatro()
    {
        Debug.Log(" OPCION DOS ");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Coliseo4");
    }

    public void MyOpcionTres()
    {
        Debug.Log(" OPCION TRES ");
        opcion = 3;
       // UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Primero");
    }
}
