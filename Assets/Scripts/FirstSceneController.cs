using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class FirstSceneController : MonoBehaviour
{

    private VideoPlayer _player;
    private GameObject _menuMain;
    private GameObject _menuColiseo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {

        _player = gameObject.GetComponent<VideoPlayer>();
        _menuMain = GameObject.Find("MenuMain");
        Vector3 _pos = _menuMain.GetComponent<RectTransform>().position;
        _pos.x = -500;
        Debug.Log(" VALORES DE RECT " + _pos);

        _menuColiseo = GameObject.Find("MenuColiseo");
        _menuMain.SetActive(false);
        if(_menuColiseo!=null)
            _menuColiseo.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(_player.isPlaying)
        {
            GameObject _text = GameObject.Find("Loading");
            Destroy(_text);
            if(_menuMain!=null)
              _menuMain.SetActive(true);
        }
    }
}
