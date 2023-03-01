using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Parameter : MonoBehaviour
{
    private static Parameter instance;

    
    public float Volume = 0.5f;
    public int MapSize;
     private GameObject inputObj;
    public GameManager.ModeEnum mode;
    // Start is called before the first frame update
    void Start()
    {
        MapSize = 10;
        mode = GameManager.ModeEnum.Basic;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }else Destroy(gameObject);
    }

    
    
}
