using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Nidavellir.BuildSystem
{
    public class Grid : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private SpriteRenderer _elevatorSpriteRenderer;
        private SpriteRenderer _elevatorSizeSpriteRenderer;

        [SerializeField]
        private int rows = 10;
        
        [SerializeField]
        private int columns = 5;

        [SerializeField] private GameObject template;
        
        [SerializeField] private Button bedroomButton;
        [SerializeField] private GameObject bedroomPrefab;
        [SerializeField] private GameObject elevatorPrefab;
        
        private bool _addingBedroom = false;
        
        // Start is called before the first frame update
        void Start()
        {
            this._spriteRenderer = this.template.GetComponent<SpriteRenderer>();
            this._elevatorSpriteRenderer = this.elevatorPrefab.GetComponentInChildren<SpriteRenderer>();
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
            var yOffset =  ySize * row + ySize / 2;

            var middle = (int)(Math.Floor(this.columns / 2f) + this.columns % 2);
            var xOffset = 0f;
            
            
            for (int i = 0; i < this.columns; i++)
            {
                
                if (i == middle - 1)
                {
                    this.AddElevator(xOffset, yOffset);
                    xOffset += xSize / 2;
                    continue;
                }
                GameObject go = Instantiate(this.template, new Vector3(xOffset + xSize / 2, yOffset, -0.1f), Quaternion.identity);
                go.transform.SetParent(this.transform, false);
                var text = go.GetComponentInChildren<TextMeshProUGUI>(true);
                text.text = (row * this.columns + i).ToString();
                go.SetActive(true);
                xOffset += xSize;
            }
        }

        private void AddElevator(float currentXOffset, float currentYOffset)
        {
            Vector3 templateSize = this.template.GetComponent<Renderer>().bounds.size;
            var templateSizeX = templateSize.x / 2;
            var templateSizeY = templateSize.y;
            GameObject go = Instantiate(this.elevatorPrefab, new Vector3(currentXOffset + templateSizeX / 2, currentYOffset, -0.1f), new Quaternion());
            go.transform.SetParent(this.transform, false);
            Vector3 elevatorSize = go.GetComponentInChildren<Renderer>().bounds.size;
            var scale = new Vector3(templateSizeX / elevatorSize.x, templateSizeY / elevatorSize.y, 1);
            go.transform.localScale = scale;
        }

        public void OnGridTileClicked(GridEntity entity)
        {
            if (this._addingBedroom)
            {
                var go = Instantiate(bedroomPrefab, entity.transform.position, Quaternion.identity, entity.transform.parent);
                Vector3 parentSize = this.template.GetComponent<Renderer>().bounds.size;
                Vector3 childSize = go.GetComponentInChildren<Renderer>().bounds.size;
                var scale = new Vector3(parentSize.x / childSize.x, parentSize.y / childSize.y, 1);
                
                go.transform.localScale = scale;
                Destroy(entity.gameObject);
                this._addingBedroom = false;
            }
        }
        
    }
}
