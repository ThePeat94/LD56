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
            
            if (piecesLeft <= 0 || applePieces == null || applePieces.Length == 0)
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
