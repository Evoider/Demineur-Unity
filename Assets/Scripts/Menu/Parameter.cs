using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Parameter : MonoBehaviour
{
    private static Parameter instance;

    
    public int Mode;
    public float Volume = 0.5f;
    public int MapSize;
     private GameObject inputObj;
    public GameManager.ModeEnum mode;
    // Start is called before the first frame update
    void Start()
    {
        MapSize = 10;
        mode = GameManager.ModeEnum.Basic;
        inputObj= GameObject.Find("SelectSize");

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }else Destroy(gameObject);
    }

    public void SetMapSize(string input)
    {
        if (input.Length != 0)
            MapSize = int.Parse(input);
    }

    public void SetMode(int input)
    {
        switch (input)
        {
            case 0:
                inputObj.SetActive(true);
                mode = GameManager.ModeEnum.Basic;
                break;
            case 1:
                inputObj.SetActive(true);
                mode = GameManager.ModeEnum.Rotate;
                break;
            case 2:
                mode = GameManager.ModeEnum.Infinite;
                MapSize = 3;
                inputObj.SetActive(false);
                break;
            default:
                break;
        }
    }
    
}
