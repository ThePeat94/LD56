using UnityEngine;

namespace Nidavellir
{
    public class AppleManager : MonoBehaviour
    {
        private int pieces;
        private int piecesLeft;
        
        public GameObject[] applePieces;
        
        private void Start()
        {
            pieces = applePieces.Length;
            piecesLeft = pieces;
            foreach (GameObject applePiece in applePieces)
            {
                applePiece.SetActive(false);
            }
            applePieces[pieces - piecesLeft].SetActive(true);
        }

        public  void TakeBite()
        {
            piecesLeft--;
            
            if (piecesLeft == 0)
            {
                Destroy(gameObject);
            }
            else
            {
                applePieces[pieces - piecesLeft].SetActive(true); 
                applePieces[pieces - piecesLeft-1].SetActive(false); 
            }
        }
    }
}
