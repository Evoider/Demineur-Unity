using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

internal class BombCounter : MonoBehaviour
{
    public int Count { get; set; }
    public string myCountTxt;
    public GameObject CountTxt;
    public GameObject GameManager;

    void Update()
    {
        myCountTxt = string.Format("{0}", Count);
        CountTxt.GetComponent<TMP_Text>().SetText(myCountTxt);

    }
    public void Init()
    {
        Count = GameManager.GetComponent<GameManager>().mineCount;
    }
}

