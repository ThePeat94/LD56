using UnityEngine;

namespace Nidavellir
{
    public class AppleManager : MonoBehaviour
    {
        public int pieces;
        private int piecesLeft;

        public GameObject[] applePieces;

        private void Start()
        {
            if (applePieces != null && applePieces.Length > 0) // Use '&&' to avoid null reference exceptions
            {
                pieces = applePieces.Length;
                piecesLeft = pieces; // Initialize piecesLeft

                foreach (GameObject applePiece in applePieces)
                {
                    applePiece.SetActive(false); // Disable all pieces at the start
                }

                applePieces[pieces - piecesLeft].SetActive(true); // Activate the first piece
            }
            else
            {
                pieces = 1;
                piecesLeft = pieces; // Initialize piecesLeft
            }
        }

        public void TakeBite()
        {
            piecesLeft--;
            
            Debug.Log(piecesLeft);

            if (piecesLeft <= 0 || applePieces == null || applePieces.Length == 0)
            {
                Destroy(gameObject); // Destroy the apple when no pieces are left
            }
            else
            {
                applePieces[pieces - piecesLeft].SetActive(true); // Activate the current piece
                applePieces[pieces - piecesLeft - 1].SetActive(false); // Deactivate the previous piece
            }
        }
    }
}