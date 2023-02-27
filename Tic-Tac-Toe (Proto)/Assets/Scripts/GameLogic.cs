using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public Sprite[] sprites;
    public Dictionary<PlayerTurn, Sprite> playerImage = new Dictionary<PlayerTurn, Sprite>();

    public GameGrid gameGrid;
    public AIController AIController;

    public PlayerTurn playerTurn = PlayerTurn.O;
    public GameMode gameMode = GameMode.Players;

    public bool aiTurn = false;
    public bool gameOver;

    private void Start()
    {
        playerImage.Add(PlayerTurn.O, sprites[0]);
        playerImage.Add(PlayerTurn.X, sprites[1]);
    }
    void Update()
    {
        switch (gameMode)
        {
            case GameMode.AI:
                if (aiTurn)
                {
                    AIController.PlayTurn();
                }
                else
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        PlayTurn();
                    }
                }
                break;
            case GameMode.Players:
                if (Input.GetMouseButtonDown(0))
                {
                    PlayTurn();
                }
                break;
        }
        
    }

    void PlayTurn()
    {
        if (!gameOver)
        {
            switch (playerTurn)
            {
                case PlayerTurn.O:
                    if (gameGrid.CheckTileClicked())
                        return;
                    else
                    {
                        gameGrid.ClickTile(playerImage[playerTurn]);
                        CheckWin();
                    }
                    playerTurn = PlayerTurn.X;
                    break;
                case PlayerTurn.X:
                    if (gameGrid.CheckTileClicked())
                        return;
                    else
                    {
                        gameGrid.ClickTile(playerImage[playerTurn]);
                        CheckWin();
                    }
                    playerTurn = PlayerTurn.O;
                    break;
            }

            if (gameMode == GameMode.AI)
                aiTurn = true;
        }
    }
    void CheckWin()
    {
        // Check rows
        for (int x = 0; x < 3; x++)
        {
            if ((gameGrid.tiles[0, x].image.sprite == gameGrid.tiles[1, x].image.sprite) && (gameGrid.tiles[1, x].image.sprite == gameGrid.tiles[2, x].image.sprite))
            {
                if (gameGrid.tiles[0, x].image.sprite != null)
                {
                    gameGrid.tiles[0, x].image.color = Color.blue;
                    gameGrid.tiles[1, x].image.color = Color.blue;
                    gameGrid.tiles[2, x].image.color = Color.blue;
                    Debug.Log(playerTurn.ToString() + " won the game!" + " at" + x.ToString());
                    gameOver = true;
                }
            }
        }

        // Check columns
        for (int y = 0; y < 3; y++)
        {
            if ((gameGrid.tiles[y, 0].image.sprite == gameGrid.tiles[y, 1].image.sprite) && (gameGrid.tiles[y, 1].image.sprite == gameGrid.tiles[y, 2].image.sprite))
            {
                if (gameGrid.tiles[y, 0].image.sprite != null)
                {
                    gameGrid.tiles[y, 0].image.color = Color.blue;
                    gameGrid.tiles[y, 1].image.color = Color.blue;
                    gameGrid.tiles[y, 2].image.color = Color.blue;
                    Debug.Log(playerTurn.ToString() + " won the game!" + " at" + y.ToString());
                    gameOver = true;
                }
            }
        }

        // Check diagonals
        for (int i = 1; i <= 1; i++)
        {
            if ((gameGrid.tiles[i, i].image.sprite == gameGrid.tiles[i + 1, i - 1].image.sprite) && (gameGrid.tiles[i + 1, i - 1].image.sprite == gameGrid.tiles[i - 1, i + 1].image.sprite))
            {
                if (gameGrid.tiles[i, i].image.sprite != null)
                {
                    gameGrid.tiles[i, i].image.color = Color.blue;
                    gameGrid.tiles[i + 1, i - 1].image.color = Color.blue;
                    gameGrid.tiles[i - 1, i + 1].image.color = Color.blue;
                    Debug.Log(playerTurn.ToString() + " won the game!" + " at" + i.ToString());
                    gameOver = true;
                }
            }
            else if ((gameGrid.tiles[i, i].image.sprite == gameGrid.tiles[i + 1, i + 1].image.sprite) && (gameGrid.tiles[i + 1, i + 1].image.sprite == gameGrid.tiles[i - 1, i - 1].image.sprite))
            {
                if (gameGrid.tiles[i, i].image.sprite != null)
                {
                    gameGrid.tiles[i, i].image.color = Color.blue;
                    gameGrid.tiles[i + 1, i + 1].image.color = Color.blue;
                    gameGrid.tiles[i - 1, i - 1].image.color = Color.blue;
                    Debug.Log(playerTurn.ToString() + " won the game!" + " at" + i.ToString());
                    gameOver = true;
                }
            }
        }

        // Check tie
        if(CheckIfTilesAreFilled() && !gameOver)
        {
            gameOver = true;
            Debug.Log("The game ended in a tie!");
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    gameGrid.tiles[x, y].image.color = Color.red;
                }
            }
        }
    }

    bool CheckIfTilesAreFilled()
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (!gameGrid.tiles[x, y].image.sprite)
                {
                    return false;
                }
            }
        }
        return true;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public enum PlayerTurn
    {
        None,
        O,
        X,
    }
    public enum GameMode
    {
        AI,
        Players,
    }
}
