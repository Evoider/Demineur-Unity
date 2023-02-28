using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameter : MonoBehaviour
{
    public string MapSize;
    public int Mode;
    public float Volume = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        MapSize = "10";
        Mode = 0;
        DontDestroyOnLoad(transform.gameObject);
    }

    public void SetMapSize(string input)
    {
        if (input.Length != 0)
            MapSize = input;
    }

    public void SetMode(int input)
    {
        Mode = input;
    }
}
