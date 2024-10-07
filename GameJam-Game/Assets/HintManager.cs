using System.Collections;
using Nidavellir.Input;
using TMPro;
using UnityEngine;

namespace Nidavellir
{
    public class HintManager : MonoBehaviour
    {
        private TMP_Text _hintText;
        public InputProcessor m_inputProcessor;

        public GameObject catSpawner;
        public GatherFood gatherFood;

        // Start is called before the first frame update
        void Start()
        {
            catSpawner.SetActive(false);
            StartCoroutine(HintManagement());
        }

        IEnumerator HintManagement()
        {
            _hintText = GetComponentInChildren<TMP_Text>();
            _hintText.text = "Use [WASD] or [Arrow keys] to move.";

            var next = false;
            while (!next)
            {
                yield return new WaitForSeconds(1);
                if (m_inputProcessor.Movement != Vector2.zero)
                {
                    next = true;
                }

                yield return new WaitForSeconds(1);
            }

            _hintText.text = "Use [Shift] to dash.";
            next = false;
            while (!next)
            {
                yield return new WaitForSeconds(1);
                if (m_inputProcessor.IsBoosting)
                {
                    next = true;
                }

                yield return new WaitForSeconds(1);
            }

            _hintText.text = "Find an Apple and use [E] to bite of a piece.";
            next = false;
            while (!next)
            {
                yield return new WaitForSeconds(1);
                if (gatherFood.hasCurrentPiece)
                {
                    next = true;
                }

                yield return new WaitForSeconds(1);
            }
            
            _hintText.text = "Bring the Piece to the burrow entry.";
            next = false;
            while (!next)
            {
                yield return new WaitForSeconds(1);
                if (gatherFood.delivered)
                {
                    next = true;
                }

                yield return new WaitForSeconds(1);
            }
            
            _hintText.text = "Good job!";
            
            yield return new WaitForSeconds(3);
            
            _hintText.text = "Beware from the cat!";
            yield return new WaitForSeconds(3);
            catSpawner.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
             
            _hintText.text = "";
            next = false;
            while (!next)
            {
                yield return new WaitForSeconds(1);
                if (gatherFood.hasCurrentPiece)
                { 
                    next = true;
                } 
            }
            
            _hintText.text = "Drop piece with [E] to become faster again.";
            next = false;
            while (!next)
            { 
                if (!gatherFood.hasCurrentPiece)
                { 
                    next = true;
                }
                yield return new WaitForSeconds(1);
            }
            
            _hintText.text = "";
        }
    }
}