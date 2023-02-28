using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameter : MonoBehaviour
{
    public int MapSize;
     private GameObject inputObj;
    public GameManager.ModeEnum mode;
    public float volume;
    // Start is called before the first frame update
    void Start()
    {
        MapSize = 10;
        mode = GameManager.ModeEnum.Basic;
        inputObj= GameObject.Find("SelectSize");
        DontDestroyOnLoad(transform.gameObject);
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
    public void SetVolume(float input)
    {
        volume = input;
    }
}
