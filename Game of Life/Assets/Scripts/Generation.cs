using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Generation {

    private CellState[,] cells;
    private int generationNumber;

    public enum CellState
    {
        Dead = 0,
        Alive = 1
    }

    public CellState[,] Next()
    {
        for (int i = 0; i < cells.GetLength(0); i++)
            for (int j = 0; j < cells.GetLength(1); j++)
            {
                List<CellState> neighbors = GetNeighbors(i, j);
                int aliveNeighbors = 0;
                foreach (var neighbor in neighbors)
                    if (neighbor == CellState.Alive)
                        aliveNeighbors++;
                if (cells[i, j] == CellState.Alive)
                {
                    if (aliveNeighbors == 2 || aliveNeighbors == 3)
                        cells[i, j] = CellState.Alive;
                    else
                        cells[i, j] = CellState.Dead;
                }
                else
                {
                    if (aliveNeighbors == 3)
                        cells[i, j] = CellState.Alive;
                }
            }
        generationNumber++;
        return cells;
    }

    public CellState[,] Init(int seed, int tilemapWidth, int tilemapHeight)
    {
        cells = new CellState[tilemapWidth, tilemapHeight];
        Random.InitState(seed);
        for (int i = 0; i < tilemapWidth; i++)
            for (int j = 0; j < tilemapHeight; j++)
            {
                cells[i, j] = (CellState)Random.Range(0, 2);
            }
        generationNumber = 0;
        return cells;
    }

    private List<CellState> GetNeighbors(int x, int y)
    {
        int cellsX = cells.GetLength(0) - 1;
        int cellsY = cells.GetLength(1) - 1;
        List<CellState> neighbors = new List<CellState>();
        if (x > 0 && y > 0)
            neighbors.Add(cells[x - 1, y - 1]);
        if (x > 0)
            neighbors.Add(cells[x - 1, y]);
        if (y > 0)
            neighbors.Add(cells[x, y - 1]);
        if (x > 0 && y < cellsY)
            neighbors.Add(cells[x - 1, y + 1]);
        if (x < cellsX && y > 0)
            neighbors.Add(cells[x + 1, y - 1]);
        if (x < cellsX && y < cellsY)
            neighbors.Add(cells[x + 1, y + 1]);
        if (x < cellsX)
            neighbors.Add(cells[x + 1, y]);
        if (y < cellsY)
            neighbors.Add(cells[x, y + 1]);
        return neighbors;
    }
}
