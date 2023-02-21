using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartParam : MonoBehaviour
{
    public string NumMines { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        NumMines = "10";
        DontDestroyOnLoad(transform.gameObject);
    }

    public void NumberOfMines(string input)
    {
        NumMines = input;
    }
}
