using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Battleship
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private int[,] grid = new int[,]
        {
            //Top left is (0,0)
            { 1,1,0,0,1 },
            { 0,0,0,0,0 },
            { 0,0,1,0,1 },
            { 1,0,1,0,0 },
            { 1,0,1,0,1 }
            //Bottom right is 4,4
        };

        //Represents where the player has fired
        private bool[,] hits;

        //Total rows and columns
        private int nRows;
        private int nCols;
        //Current row/column
        private int row;
        private int col;
        //Correctly hit ships
        private int score;
        //Total time game has been running
        private int time;

        //Parent of all cells
        [SerializeField] Transform gridRoot;
        //Template to populate grid
        [SerializeField] GameObject cellPrefab;
        [SerializeField] GameObject winLabel;
        [SerializeField] TextMeshProUGUI timeLabel;
        [SerializeField] TextMeshProUGUI scoreLabel;

        Transform GetCurrentCell()
        {
            //You can figure out the child index
            //of the cell that is a part of the grid
            //by calculating (rows*cols) + col
            int index = (row * nCols) + col;
            //Return child by index
            return gridRoot.GetChild(index);
        }

       void SelectCurrentCell()
        {
            //Get current cell
            Transform cell = GetCurrentCell();
            //Set the "cursor" image on
            Transform cursor = cell.Find("Cursor");
            cursor.gameObject.SetActive(true);
        }

        void UnselectCurrentCell()
        {
            //Get current cell
            Transform cell = GetCurrentCell();
            //Set the "cursor" image off
            Transform cursor = cell.Find("Cursor");
            cursor.gameObject.SetActive(false);
        }

        public void MoveHorizontal(int amt)
        {
            //Unselect previous cell
            UnselectCurrentCell();

            //Update column
            col += amt;
            //Column stays within bounds of grid
            col = Mathf.Clamp(col, 0, nCols-1);

            //Select new cell
            SelectCurrentCell();
        }

        public void MoveVertical(int amt)
        {
            //Unselect previous cell
            UnselectCurrentCell();

            //Update row
            row += amt;
            //Row stays within bounds of grid
            row = Mathf.Clamp(row, 0, nRows - 1);

            //Select new cell
            SelectCurrentCell();
        }


        private void Awake()
        {
            //Initialize rows/cols to help w/ operations
            nRows = grid.GetLength(0);
            nCols = grid.GetLength(1);
            //Create identical 2D array to grid of type bool
            hits = new bool[nRows, nCols];

            /*Populate the grid using a loop
            Needs to execute as many times to fill up grid
            Can figure out by calculating rows*cols
            */
            for(int i = 0; i < nRows * nCols; i++)
            {
                //Create instance of prefab and child it to gridRoot
                Instantiate(cellPrefab, gridRoot);
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}


