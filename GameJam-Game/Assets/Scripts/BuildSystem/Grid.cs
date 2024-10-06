using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Nidavellir.BuildSystem
{
    public class Grid : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Renderer _renderer;

        [SerializeField]
        private int rows = 10;
        
        [SerializeField]
        private int columns = 5;

        [SerializeField] private GameObject template;
        [SerializeField] private GameObject parent;
        
        [SerializeField] private Button bedroomButton;
        [SerializeField] private GameObject bathroomPrefab;
        
        private bool _addingBedroom = false;
        
        // Start is called before the first frame update
        void Start()
        {
            this.parent = GameObject.Find("Plane");
            this._spriteRenderer = this.template.GetComponent<SpriteRenderer>();
            this._renderer = this.parent.GetComponent<Renderer>();
            bedroomButton.onClick.AddListener(() =>
            {
                this._addingBedroom = !this._addingBedroom;
            });
            this.CreateGrid();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void CreateGrid()
        {
            for (var i = 0; i < rows; i++)
            {
                this.CreateRow(i);
            }
        }

        private void CreateRow(int row)
        {
            var xSize = this._spriteRenderer.bounds.size.x;
            var ySize = this._spriteRenderer.bounds.size.y;
            var xOffset = xSize * row + xSize / 2 - this._renderer.bounds.size.x / 2;
            
            for (int i = 0; i < this.columns; i++)
            {
                var yOffset = ySize * i + this._renderer.bounds.size.y / 2 - ySize * 2 - ySize / 2;
                GameObject go = Instantiate(this.template, new Vector3(xOffset, yOffset, -0.1f), new Quaternion());
                go.transform.parent = this.parent.transform;
                var text = go.GetComponentInChildren<TextMeshProUGUI>(true);
                text.text = (row * this.columns + i).ToString();
                go.SetActive(true);
            }
        }

        public void OnGridTileClicked(GridEntity entity)
        {
            if (this._addingBedroom)
            {
                var go = Instantiate(bathroomPrefab, entity.transform.position, Quaternion.identity, entity.transform.parent);
                Vector3 parentSize = entity.GetComponent<Renderer>().bounds.size;
                Vector3 childSize = go.GetComponentInChildren<Renderer>().bounds.size;
                var scale = new Vector3(parentSize.x / childSize.x, parentSize.y / childSize.y, 1);
                
                go.transform.localScale = scale;
                Destroy(entity.gameObject);
                this._addingBedroom = false;
            }
        }
        
    }
}
