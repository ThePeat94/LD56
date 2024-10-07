using System;
using System.Collections;
using System.Collections.Generic;
using Nidavellir.Scriptables;
using TMPro;
using UnityEngine;

namespace Nidavellir
{
    public class GameEndManager : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI score;
        [SerializeField] private TextMeshProUGUI highScore;
        [SerializeField] private Resource resource;
        
        // Start is called before the first frame update
        void Start()
        {
            this.highScore.text = ((int)Math.Floor(PlayerPrefs.GetFloat("HighScore", 0))).ToString();
            this.score.text = ((int)this.resource.ResourceController.CurrentValue).ToString();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
