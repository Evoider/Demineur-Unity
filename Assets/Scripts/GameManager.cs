using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;

public class GameManager : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public int PercentOfMines = 15;

    public float spacing = 1.2f;
    public int mineCount = 10;
    public GameObject cellPrefab;
    public Sprite[] numberSprites;
    public Sprite mineSprite;
    public Sprite loseMineSprite;
    public Sprite emptySprite;
    public Sprite defaultSprite;
    public Sprite flagSprite;
    public GameObject gameOverText;
    public GameObject winText;
    public GameObject gameTimer;
    public GameObject SelectMapSizeObj;
    public enum ModeEnum
    {
        Basic, Rotate, Infinite
    }
    public ModeEnum mode = ModeEnum.Basic;
    [SerializeField] GameObject MenuObject;
    private GameObject winLoseScreenObject;
    private GameObject gridObject;
    private Cell[,] grid;
    private bool firstCell;
    private int minesPlaced = 0;
    public int level { get; private set; }
    private int startSize = 3;

    private void Start()
    {
        winLoseScreenObject = GameObject.Find("Win/LoseScreen");
        StartGame();
    }
    private void Update()
    {
        if (mode == ModeEnum.Rotate)
        {
            gridObject.transform.RotateAround(new Vector3(width / 2, height / 2), Vector3.forward, 10 * Time.deltaTime);
            //gridObject.transform.Rotate(0,0,10*Time.deltaTime);
        }
    }

    public void SetMapSize(string input)
    {
        if (input.Length != 0)
        {
            MapSize(int.Parse(input));
        }


    }
    private void MapSize(int size)
    {
        int number = size;
        width = number;
        height = number;
    }

    private void StartGame()
    {
        startSize = FindAnyObjectByType<Parameter>().MapSize;
        MapSize(startSize);
        mode = FindAnyObjectByType<Parameter>().mode;
        if (mode == ModeEnum.Infinite)
        {
            level = 1;
            SelectMapSizeObj.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Info base").SetActive(false);
        }else GameObject.FindGameObjectWithTag("Info infini").SetActive(false);
        CreateGrid();
        PlaceMines();
        FindAnyObjectByType<BombCounter>().Init();
        FindAnyObjectByType<CameraManager>().UpdateCamera();
    }

    private void CreateGrid()
    {
        firstCell = true;
        gridObject = new GameObject();
        gridObject.name = "Grid";

        grid = new Cell[width, height];

        // Cr�e chaque cellule de la grille
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject newCell = Instantiate(cellPrefab, new Vector3(x * spacing, y * spacing, 0), Quaternion.identity);
                newCell.name = string.Format("Cell ({0}, {1})", x, y);
                newCell.transform.parent = gridObject.transform; // set the grid object as the parent of the new cell
                Cell cell = newCell.GetComponent<Cell>();
                cell.x = x;
                cell.y = y;
                grid[x, y] = cell;
            }
        }
    }

    private void PlaceMines()
    {
        mineCount = width * height * PercentOfMines / 100;
        while (minesPlaced < mineCount)
        {

            int x = Random.Range(0, width);
            int y = Random.Range(0, height);
            int adjacentMines = CountAdjacentMines(x, y);
            // Place une mine si la case est vide
            if (adjacentMines >= 3)
            {
                if (!grid[x, y].isMine || !grid[x, y].isRevealed)
                {
                    continue;
                }
            }
            else if (!grid[x, y].isMine)
            {
                grid[x, y].isMine = true;
                minesPlaced++;
            }
        }

        // Compte le nombre de mines adjacentes pour chaque cellule
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (!grid[x, y].isMine)
                {
                    int adjacentMines = CountAdjacentMines(x, y);
                    grid[x, y].adjacentMines = adjacentMines;
                }
            }
        }
    }

    private int CountAdjacentMines(int x, int y)
    {
        int count = 0;

        // Parcourt les cases adjacentes
        for (int x2 = x - 1; x2 <= x + 1; x2++)
        {
            for (int y2 = y - 1; y2 <= y + 1; y2++)
            {
                // Ignore la case elle-m�me
                if (x2 == x && y2 == y)
                {
                    continue;
                }

                // V�rifie si la case est dans la grille
                if (x2 >= 0 && x2 < width && y2 >= 0 && y2 < height)
                {
                    if (grid[x2, y2].isMine)
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }

    private void CheckWinCondition()
    {
        bool allCellsRevealed = true;

        // V�rifie si toutes les cellules ont �t� r�v�l�es, sauf les mines
        foreach (Cell cell in grid)
        {
            if (!cell.isRevealed && !cell.isMine)
            {
                allCellsRevealed = false;
                break;
            }
        }

        if (allCellsRevealed)
        {
            switch (mode)
            {
                case ModeEnum.Basic:
                case ModeEnum.Rotate:
                    FindObjectOfType<Timer>().Pause();
                    GameObject winTxt = Instantiate(winText, winLoseScreenObject.transform);
                    winTxt.name = "WinText";
                    winTxt.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 20);
                    winTxt.GetComponent<RectTransform>().localScale = new Vector2(2, 2);
                    break;
                case ModeEnum.Infinite:
                    InfiniteMode();
                    break;

            }

        }
    }

    private void InfiniteMode()
    {
        MapSize(startSize + level);
        level++;
        Destroy(GameObject.Find("Grid"));
        minesPlaced = 0;
        CreateGrid();
        PlaceMines();
        GameObject.Find("BombCount").GetComponent<BombCounter>().Init();
        FindAnyObjectByType<CameraManager>().UpdateCamera();
    }

    private void RevealEmptyCells(int x, int y)
    {
        // Parcourt les cases adjacentes
        for (int x2 = x - 1; x2 <= x + 1; x2++)
        {
            for (int y2 = y - 1; y2 <= y + 1; y2++)
            {
                // Ignore la case elle-m�me
                if (x2 == x && y2 == y)
                {
                    continue;
                }

                // V�rifie si la case est dans la grille
                if (x2 >= 0 && x2 < width && y2 >= 0 && y2 < height)
                {
                    Cell cell = grid[x2, y2];

                    // Ignore les cases d�j� r�v�l�es ou marqu�es
                    if (cell.isRevealed || cell.isMarked)
                    {
                        continue;
                    }

                    // R�v�le les cases vides
                    if (cell.adjacentMines == 0)
                    {
                        cell.Reveal();
                        RevealEmptyCells(x2, y2);
                    }
                    // R�v�le les cases avec des nombres adjacents
                    else
                    {
                        cell.RevealNumber();
                    }
                }
            }
        }
    }

    private void FirstCellClicked(Cell cell)
    {
        if (cell.isMine)
        {
            cell.isMine = false;
            minesPlaced--;
            PlaceMines();
        }
        if (cell.adjacentMines == 0)
        {
            cell.Reveal();
            RevealEmptyCells(cell.x, cell.y);
            CheckWinCondition();
        }
        else
        {
            cell.RevealNumber();
            CheckWinCondition();
        }

    }
    public void CellClicked(Cell cell)
    {
        // Ignore les clics si la partie est terminée ou en pause
        if (IsGameOver() || MenuObject.activeInHierarchy || cell.isMarked)
        {
            return;
        }

        if (firstCell)
        {
            firstCell = false;
            FirstCellClicked(cell);
            //foreach (Cell cel in grid)
            //{
            //    if (cel.isMine && !cel.isRevealed)
            //    {
            //        cel.Explode();
            //    }
            //}
        }
        // R�v�le la cellule cliqu�e
        else
        {
            if (cell.isMine)
            {
                cell.RevealExplodedMine();
                cell.Explode();
                StartCoroutine(ExplosionZone(cell));
                GameOver();
            }
            else if (cell.adjacentMines == 0)
            {
                cell.Reveal();
                RevealEmptyCells(cell.x, cell.y);
                CheckWinCondition();
            }
            else
            {
                cell.RevealNumber();
                CheckWinCondition();
            }
        }
    }



    IEnumerator ExplosionZone(Cell cell)
    {
        for (int x2 = cell.x - 2; x2 <= cell.x + 2; x2++)
        {
            for (int y2 = cell.y - 2; y2 <= cell.y + 2; y2++)
            {
                // Ignore la case elle-m�me
                if ((x2 == cell.x && y2 == cell.y) || x2 > cell.x + 1 || x2 < cell.x - 1 || y2 > cell.y + 1 || y2 < cell.y - 1)
                {
                    if ((x2 >= 0 && x2 < width && y2 >= 0 && y2 < height) && ((x2 == cell.x && (y2 == cell.y - 2 || y2 == cell.y + 2)) || (y2 == cell.y && (x2 == cell.x - 2 || x2 == cell.x + 2))))
                    {
                        if (grid[x2, y2].isMine && !grid[x2, y2].isExploded)
                        {
                            yield return new WaitForSeconds(0.22f);
                            grid[x2, y2].Explode();
                            StartCoroutine(ExplosionZone(grid[x2, y2]));
                        }
                        else grid[x2, y2].Explode();
                    }
                    else continue;


                }
                else
                {

                    // V�rifie si la case est dans la grille
                    if (x2 >= 0 && x2 < width && y2 >= 0 && y2 < height)
                    {

                        if (grid[x2, y2].isMine && !grid[x2, y2].isExploded)
                        {
                            yield return new WaitForSeconds(0.2f);
                            grid[x2, y2].Explode();
                            StartCoroutine(ExplosionZone(grid[x2, y2]));
                        }
                        else grid[x2, y2].Explode();
                    }
                }
            }
        }


    }

    public void CellMarked(Cell cell)
    {
        // Ignore les clics si la partie est termin�e
        if (IsGameOver())
        {
            return;
        }

        // Marque ou d�marque la cellule
        if (!cell.isRevealed)
        {
            cell.ToggleMark();
        }

        // V�rifie si la partie est gagn�e
        CheckWinCondition();
    }


    public void GameOver()
    {
        // Révéle toutes les mines
        foreach (Cell cell in grid)
        {
            if (cell.isMine && !cell.isRevealed)
            {
                cell.RevealMine();

            }
        }

        FindObjectOfType<Timer>().Pause();

        Destroy(GameObject.Find("GameOverText"));
        Destroy(GameObject.Find("WinText"));

        GameObject gameOvertxt = Instantiate(gameOverText, winLoseScreenObject.transform);
        gameOvertxt.name = "GameOverText";
        gameOvertxt.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 20);
        gameOvertxt.GetComponent<RectTransform>().localScale = new Vector2(2, 2);
    }

    public bool IsGameOver()
    {
        // Vérifie si une mine a été révélé
        foreach (Cell cell in grid)
        {
            if (cell.isRevealed && cell.isMine)
            {
                return true;
            }
        }

        return false;
    }

    public void Restart()
    {
        Destroy(GameObject.Find("Grid"));
        Destroy(GameObject.Find("GameOverText"));
        Destroy(GameObject.Find("WinText"));
        minesPlaced = 0;
        level = 1;
        if (mode == ModeEnum.Infinite) { MapSize(3); }
        CreateGrid();
        PlaceMines();
        GameObject.Find("BombCount").GetComponent<BombCounter>().Init();

    }

    public void Cheat()
    {

        Destroy(GameObject.Find("GameOverText"));
        Destroy(GameObject.Find("WinText"));

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (!grid[x, y].isMine)
                {
                    if (grid[x, y].adjacentMines == 0)
                        grid[x, y].Reveal();
                    else grid[x, y].RevealNumber();
                }
                else if (grid[x, y].isMine)
                {
                    grid[x, y].ToggleMark();
                }
            }
        }
        CheckWinCondition();
    }

    public void ClearMessage()
    {
        Destroy(GameObject.Find("GameOverText"));
        Destroy(GameObject.Find("WinText"));
    }
}

