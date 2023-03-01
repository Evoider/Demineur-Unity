using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetParameter : MonoBehaviour
{
    Parameter parameter;
    GameObject inputObj;

    // Start is called before the first frame update
    void Start()
    {
        inputObj = GameObject.Find("SelectSize");
        parameter = FindObjectOfType<Parameter>();
        FindObjectOfType<TMP_Dropdown>().value = (int)parameter.mode;
    }
    public void SetMapSize(string input)
    {
        if (input.Length != 0)
            parameter.MapSize = int.Parse(input);
    }

    public void SetMode(int input)
    {

        switch (input)
        {
            case 0:
                inputObj.SetActive(true);
                parameter.MapSize = 10;
                parameter.mode = GameManager.ModeEnum.Basic;
                break;
            case 1:
                inputObj.SetActive(true);
                parameter.MapSize = 10;
                parameter.mode = GameManager.ModeEnum.Rotate;
                break;
            case 2:
                parameter.mode = GameManager.ModeEnum.Infinite;
                parameter.MapSize = 3;
                inputObj.SetActive(false);
                break;
            default:
                break;
        }
    }
}
