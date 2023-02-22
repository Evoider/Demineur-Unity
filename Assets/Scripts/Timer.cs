using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine.UI;
using TMPro;
using System.IO.Compression;

namespace Assets.Scripts
{
    internal class Timer : MonoBehaviour
    {
        public string timeText;
        public float myTimer;
        public GameObject TimerTxt;

        private void Start()
        {
            
        }

        void Update()
        {
            myTimer += Time.deltaTime;
            DisplayTime(myTimer);
            TimerTxt.GetComponent<TMP_Text>().SetText(timeText);
        }

        void DisplayTime(float timeToDisplay)
        {
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            timeText = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
