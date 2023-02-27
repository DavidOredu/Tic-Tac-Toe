using System.Collections.Generic;
using UnityEngine;
using static GameLogic;

public class AIController : MonoBehaviour
{
    private int weight;

    private GameLogic gameLogic;

    public PlayerTurn aiTurn;
    // Start is called before the first frame update
    void Start()
    {
        gameLogic = GetComponent<GameLogic>();
    }

    public void SetAiTurn(PlayerTurn turn)
    {
        aiTurn = turn;
    }
    public void PlayTurn()
    {
        var bestRow = CheckRows();
        var bestColumn = CheckColumns();

        Tile bestTile = null;
        foreach (var row in bestRow)
        {
            foreach (var column in bestColumn)
            {
                if (row.Value > column.Value)
                {
                    bestTile = row.Key;
                }
                else if (row.Value < column.Value)
                {
                    bestTile = column.Key;
                }
                else if (row.Value == column.Value)
                {
                    var randInt = Random.Range(0, 2);
                    if (randInt == 0)
                        bestTile = row.Key;
                    else
                        bestTile = column.Key;
                }
            }
        }
        bestTile.DrawShape(gameLogic.playerImage[aiTurn]);
        gameLogic.aiTurn = false;
    }
    private Dictionary<Tile, int> CheckRows()
    {
        var gameGrid = gameLogic.gameGrid; if (gameGrid == null) return null;
        int rowWeight = 0;
        Tile emptyTile = null;

        Dictionary<Tile, int> positionWeights = new Dictionary<Tile, int>();

        for (int x = 0; x < 3; x++)
        {
            for (int i = 0; i < 3; i++)
            {
                if (gameGrid.tiles[i, x].image.sprite == gameLogic.playerImage[aiTurn])
                {
                    rowWeight++;
                }
                else if (!gameGrid.tiles[i, x].image.sprite)
                {
                    emptyTile = gameGrid.tiles[i, x];
                }
            }
            positionWeights.Add(emptyTile, rowWeight);
        }
        return GetBestOption(positionWeights);
    }
    private Dictionary<Tile, int> CheckColumns()
    {
        var gameGrid = gameLogic.gameGrid; if (gameGrid == null) return null;
        int columnWeight = 0;
        Tile emptyTile = null;

        Dictionary<Tile, int> positionWeights = new Dictionary<Tile, int>();

        for (int y = 0; y < 3; y++)
        {
            for (int i = 0; i < 3; i++)
            {
                if (gameGrid.tiles[y, i].image.sprite == gameLogic.playerImage[aiTurn])
                {
                    columnWeight++;
                }
                else if (!gameGrid.tiles[y, i].image.sprite)
                {
                    emptyTile = gameGrid.tiles[y, i];
                }
            }
            positionWeights.Add(emptyTile, columnWeight);
        }
        return GetBestOption(positionWeights);
    }

    private Dictionary<Tile, int> GetBestOption(Dictionary<Tile, int> positionWeights)
    {
        int weight = 0;
        Dictionary<Tile, int> bestTile = new Dictionary<Tile, int>();

        foreach (var pair in positionWeights)
        {
            if(pair.Value > weight)
            {
                weight = pair.Value;
                bestTile.Clear();
                bestTile.Add(pair.Key, weight);
            }
        }
        return bestTile;
    }
    private void CheckLargeWeight(int newWeight)
    {
        if (newWeight > weight)
        {
            weight = newWeight;
        }
    }
}
