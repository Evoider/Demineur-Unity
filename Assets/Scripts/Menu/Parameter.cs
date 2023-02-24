using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameter : MonoBehaviour
{
    public string MapSize { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        MapSize = "10";
        DontDestroyOnLoad(transform.gameObject);
    }

    public void SetMapSize(string input)
    {
        if (input.Length != 0)
            MapSize = input;
    }
}
