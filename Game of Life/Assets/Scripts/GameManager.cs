using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour {

    public Tile deadTile;
    public Tile aliveTile;
    public Tilemap tilemap;
    Generation generation = new Generation();
    
    public int tilemapWidth = 18;
    public int tilemapHeight = 10;
    public int seed;

	// Use this for initialization
	void Start () {
        tilemap.size = new Vector3Int(tilemapWidth, tilemapHeight, 0);
        tilemap.ResizeBounds();
        Camera.main.orthographicSize = tilemapHeight * 0.45f;
        Generation.CellState [,] cells = generation.Init(seed, tilemapWidth, tilemapHeight);
        DrawGeneration(cells);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        DrawGeneration(generation.Next());
	}

    void DrawGeneration(Generation.CellState[,] cells)
    {
        for (int i = 0; i < cells.GetLength(0); i++)
            for (int j = 0; j < cells.GetLength(1); j++)
            {
                Tile tileToDraw;
                if (cells[i, j] == Generation.CellState.Dead)
                    tileToDraw = deadTile;
                else if (cells[i, j] == Generation.CellState.Alive)
                    tileToDraw = aliveTile;
                else
                    tileToDraw = null;
                tilemap.SetTile(new Vector3Int(i - cells.GetLength(0)/2, j - cells.GetLength(1)/2, 0), tileToDraw);
            }
    }
}
