using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class GameGrid : MonoBehaviour
{
    public Grid grid;
    public Canvas canvas;

    public Tile tileObject;
    public Tilemap tilemap;
    public Tile[,] tiles = new Tile[3,3]; 

    // Start is called before the first frame update
    void Awake()
    {
        tilemap.size = new Vector3Int(3, 3, 0);
        Vector3Int position = new Vector3Int();

        for (int x = 0; x < tilemap.size.x; x++)
        {
            for (int y = 0; y < tilemap.size.y; y++)
            {
                position.Set(x, y, 0);
                var newTile = Instantiate(tileObject, tilemap.CellToLocal(position), Quaternion.identity);
                tiles[x, y] = newTile;
                TileBase tile = Resources.Load<TileBase>("Tile");
                tilemap.SetTile(position, tile);
            }
        }
    }

   
    public void ClickTile()
    {
        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var tilePosition = grid.LocalToCellInterpolated(position);

        if ((Mathf.RoundToInt(tilePosition.x) >= 0 && Mathf.RoundToInt(tilePosition.x) < 3) && (Mathf.RoundToInt(tilePosition.y) >= 0 && Mathf.RoundToInt(tilePosition.y) < 3))
            tiles[Mathf.RoundToInt(tilePosition.x), Mathf.RoundToInt(tilePosition.y)].Toggle();
    }
    public bool CheckTileClicked()
    {
        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var tilePosition = grid.LocalToCellInterpolated(position);

        if ((Mathf.RoundToInt(tilePosition.x) >= 0 && Mathf.RoundToInt(tilePosition.x) < 3) && (Mathf.RoundToInt(tilePosition.y) >= 0 && Mathf.RoundToInt(tilePosition.y) < 3))
        {
            if (tiles[Mathf.RoundToInt(tilePosition.x), Mathf.RoundToInt(tilePosition.y)].image.enabled)
            {
                return true;
            }
        }
        return false;
    }
    public void ClickTile(Sprite sprite)
    {
        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var tilePosition = grid.LocalToCellInterpolated(position);

        if ((Mathf.RoundToInt(tilePosition.x) >= 0 && Mathf.RoundToInt(tilePosition.x) < 3) && (Mathf.RoundToInt(tilePosition.y) >= 0 && Mathf.RoundToInt(tilePosition.y) < 3))
        {
            if (!tiles[Mathf.RoundToInt(tilePosition.x), Mathf.RoundToInt(tilePosition.y)].image.enabled)
                tiles[Mathf.RoundToInt(tilePosition.x), Mathf.RoundToInt(tilePosition.y)].DrawShape(sprite);
        }
    }
    private void OnMouseEnter()
    {
        
    }
}
