using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Nidavellir.BuildSystem
{
    public class Grid : MonoBehaviour
    {
        
        private List<GridEntity> _gridEntities = new();
        private SpriteRenderer _spriteRenderer;
        private Renderer _renderer;

        [SerializeField]
        private int rows = 10;
        
        [SerializeField]
        private int columns = 5;

        [SerializeField] private GameObject template;
        [SerializeField] private GameObject parent;
        
        // Start is called before the first frame update
        void Start()
        {
            this.parent = GameObject.Find("Plane");
            this._spriteRenderer = this.template.GetComponent<SpriteRenderer>();
            this._renderer = this.parent.GetComponent<Renderer>();
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
            if (this._gridEntities.Contains(entity))
            {
                this._gridEntities.Remove(entity);
                return;
            }
            this._gridEntities.Add(entity);
            var s = entity.GetComponentInChildren<TextMeshProUGUI>();
            Debug.Log("OnGridTileClicked " + s.text);
        }
        
    }
}
