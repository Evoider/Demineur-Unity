using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
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

    private GameObject canvasObject;
    private GameObject gridObject;
    private Cell[,] grid;

    private void Start()
    {
        gridObject = GameObject.Find("Grid");
        canvasObject = GameObject.Find("Canvas");
        CreateGrid();
        PlaceMines();
    }

    private void CreateGrid()
    {
        grid = new Cell[width, height];

        // Cr�e chaque cellule de la grille
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject newCell = Instantiate(cellPrefab, new Vector3(x, y, 0), Quaternion.identity);
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
        int minesPlaced = 0;

        while (minesPlaced < mineCount)
        {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);

            // Place une mine si la case est vide
            if (!grid[x, y].isMine)
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
            GameObject winTxt = Instantiate(winText, transform);
            winTxt.name = "WinText";
            winTxt.transform.SetParent(canvasObject.transform);
            winTxt.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 20);
        }
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

    public void CellClicked(Cell cell)
    {
        // Ignore les clics si la partie est termin�e
        if (IsGameOver())
        {
            return;
        }

        // R�v�le la cellule cliqu�e
        if (cell.isMine)
        {
            cell.RevealExplodedMine();
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

    public void CellMarked(Cell cell)
    {
        // Ignore les clics si la partie est termin�e
        if (IsGameOver())
        {
            return;
        }

        // Marque ou d�marque la cellule
        cell.ToggleMark();

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
        GameObject gameOvertxt = Instantiate(gameOverText, transform);
        gameOvertxt.name = "GameOverText";
        gameOvertxt.transform.SetParent(canvasObject.transform);
        gameOvertxt.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,20);
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
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Destroy(grid[x, y].gameObject);
            }
        }
        Destroy(GameObject.Find("GameOverText"));
        Destroy(GameObject.Find("WinText"));
        CreateGrid();
        PlaceMines();
    }

    public void Cheat()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (!grid[x, y].isMine)
                {
                    grid[x, y].Reveal();
                }
                else if (grid[x, y].isMine)
                {
                    grid[x, y].RevealMine();
                }
                CheckWinCondition();
            }
        }
    }
}

