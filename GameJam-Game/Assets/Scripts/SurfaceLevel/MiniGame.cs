using System;
using System.Collections;
using System.Collections.Generic;
using Nidavellir.Input;
using UnityEngine;

namespace Nidavellir
{
    public class MiniGame : MonoBehaviour
    {
        public GameObject processBar; // Grows with every successful click, shrinks with every unsuccessful click
        public GameObject allowedArea; // if "thing" is above it after click it is a successful click and vice versa

        public GameObject
            thing; // object is moved to right according to the length of a click and to the left according to the time between clicks

        private float processBarScale = 0;
        private float processBarPosition = 0;

        private float thingPosition = 0;

        public float movementSpeed = 0.5f;
        public float processSpeed = 0.1f;
        public float allowedAreaPercentage = 0.5f; // TODO: Individualize

        public InputProcessor m_inputProcessor;

        // TODO: Implement outer barriers for "thing"

        void Start()
        {
            // process bar init
            processBar.transform.localScale = new Vector3(0, 0.5f, 1);
            processBar.transform.localPosition = new Vector3(0, 0, 0);

            allowedArea.transform.localScale = new Vector3(allowedAreaPercentage, 0.5f, 0);

            m_inputProcessor.OnClickCancelled += InputProcessorOnOnClickCancelled;
        }

        private void InputProcessorOnOnClickCancelled(object sender, System.EventArgs e)
        {
            if (thing.transform.localPosition.x < allowedArea.transform.localScale.x / 2 &&
                thing.transform.localPosition.x > -allowedArea.transform.localScale.x / 2)
            {
                processBarScale += processSpeed;
            }
            else if(processBarScale >= 0)
            {
                processBarScale -= processSpeed;
            }

            processBarPosition = processBarScale / 2;
            processBar.transform.localScale = new Vector3(processBarScale, 0.5f, 1);
            processBar.transform.localPosition = new Vector3(processBarPosition, 0, 0);
        }

        // TODO: Implement Win-condition
        // TODO: Implement loose condition
        // TODO: Implement percentage-tester ^^
        void Update()
        {
            if (m_inputProcessor.ClickInProgress)
            {
                thingPosition += movementSpeed * Time.deltaTime;
            }
            else
            {
                thingPosition -= movementSpeed * Time.deltaTime;
            }

            thing.transform.localPosition = new Vector3(thingPosition, -0.25f, 0);
        }
    }
}