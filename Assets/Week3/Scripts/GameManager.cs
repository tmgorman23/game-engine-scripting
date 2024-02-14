using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Battleship
{
    public class GameManager : MonoBehaviour
    {
        #region VARIABLES + METHODS
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
        [SerializeField] GameObject restartButton;
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

        void NewArray()
        {
            for (int row = 0; row < nRows; row++)
            {
                //every column
                for (int col = 0; col < nCols; col++)
                {
                    //go through grid and replace based on random
                    int random = Random.Range(0, 10);
                    if (random > 6) { grid[row, col] = 1; }
                    else { grid[row, col] = 0; }
                }
            }

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

        void ShowHit()
        {
            //get current cell
            Transform cell = GetCurrentCell();
            //Turn "hit" image on
            Transform hit = cell.Find("Hit");
            hit.gameObject.SetActive(true);
        }

        void ShowMiss()
        {
            //get current cell
            Transform cell = GetCurrentCell();
            //Turn "miss" image on
            Transform miss = cell.Find("Miss");
            miss.gameObject.SetActive(true);
        }

        void IncrementScore()
        {
            //Add 1 to the score
            score++;
            //Update score label
            scoreLabel.text = string.Format($"Score: {score}");
        }

        public void Fire()
        {
            //Checks if cell in the hits data is true or false
            //If true, shot already fired there
            if (hits[row, col]) return;

            //Marks as being fire upon
            hits[row, col] = true;

            //If cell is ship
            if (grid[row, col] == 1)
            {
                //Hit + increment score
                ShowHit();
                IncrementScore();
            }
            //if no ship
            else
            {
                ShowMiss();
            }
        }

        void TryEndGame()
        {
            //check every row
            for (int row = 0; row < nRows; row++)
            {
                //every column
                for (int col = 0; col < nCols; col++)
                {
                    //If cell is not a ship, ignore
                    if (grid[row, col] == 0) continue;
                    //If cell is ship and hasn't been hit
                    //return
                    if (hits[row, col] == false) return;
                }
            }

            //if loop completes, all ships are destroyed
            winLabel.SetActive(true);
            restartButton.SetActive(true);
            //Stop timer
            CancelInvoke("IncrementTime");
        }

        void IncrementTime()
        {
            //add 1 to time
            time++;
            //Update time label with current time
            //format is mm:ss
            //mm displays only as many digits as necessary
            timeLabel.text = string.Format($"{time / 60}:{(time % 60).ToString("00")}");
        }

        public void Restart()
        {
            //Initialize rows/cols to help w/ operations
            nRows = grid.GetLength(0);
            nCols = grid.GetLength(1);
            //Create identical 2D array to grid of type bool
            hits = new bool[nRows, nCols];
            UnselectCurrentCell();
            //resetting hit/miss icons
            for (int temprow = 0; temprow < nRows; temprow++)
            {
                //every column
                for (int tempcol = 0; tempcol < nCols; tempcol++)
                {
                    col = tempcol;
                    row = temprow;
                    //get current cell
                    Transform cell = GetCurrentCell();
                    //Turn "miss" image off
                    Transform miss = cell.Find("Miss");
                    miss.gameObject.SetActive(false);
                    Transform hit = cell.Find("Hit");
                    hit.gameObject.SetActive(false);
                }
            }
            //Unselects cell and resets current cell to origin
            UnselectCurrentCell();
            row = 0; col = 0;
            SelectCurrentCell();
            //Reset score and timer
            time = 0;
            score = -1;
            IncrementScore();
            //Turn restart button off
            restartButton.SetActive(false);
            winLabel.SetActive(false);
            NewArray();
            InvokeRepeating("IncrementTime", 1f, 1f);

        }

        #endregion


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

            SelectCurrentCell();
            InvokeRepeating("IncrementTime", 1f, 1f);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            TryEndGame();
        }
    }
}


