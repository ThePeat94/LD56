using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nidavellir.BuildSystem
{
    public class GridEntity : MonoBehaviour
    {
        private ClickableObject _clickableObject;
        private Grid _grid;
        
        // Start is called before the first frame update
        void Start()
        {
            this._clickableObject = GetComponent<ClickableObject>();
            this._grid = GetComponentInParent<Grid>();
            this._clickableObject.OnClick += OnClick;
        }

        private void OnClick (object sender, System.EventArgs eventArgs) {
            this._grid.OnGridTileClicked(this);
        }
    }
}
