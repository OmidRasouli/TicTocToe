using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum Character { Blank, X, O }
    public enum Player { A, B }

    [SerializeField] private List<List<Character>> grids = new List<List<Character>>();
    [SerializeField] private List<RowButtons> rowButtons = new List<RowButtons>();
    private Player turn;
    private Dictionary<Player, Character> players = new Dictionary<Player, Character>();
    private int cellInRow = 3;
    private bool gameOver;

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        gameOver = false;
        grids = new List<List<Character>>();
        for (int i = 0; i < cellInRow; i++)
        {
            var l = new List<Character>(cellInRow);
            grids.Add(l);
            for (int j = 0; j < cellInRow; j++)
            {
                grids[i].Add(Character.Blank);
                rowButtons[i].buttons[j].Reset();
            }
        }
        Debug.Log(grids.Capacity + " " + grids.Count);
        players.Add(Player.A, Character.X);
        players.Add(Player.B, Character.O);
        turn = Player.A;
        Gameplay();
    }

    private void Gameplay()
    {

    }

    public void OnButton(int index)
    {
        var row = index / cellInRow;
        var col = index % cellInRow;

        if (!rowButtons[row].buttons[col].isActive || gameOver) return;

        if (players[turn] == Character.X)
        {
            rowButtons[row].buttons[col].Clicked(Character.X.ToString());
            grids[row][col] = Character.X;
        }
        else if (players[turn] == Character.O)
        {
            rowButtons[row].buttons[col].Clicked(Character.O.ToString());
            grids[row][col] = Character.O;
        }

        var won = CheckCells(row, col);
        if (won)
        {
            Debug.Log("The Winner is " + turn);
            gameOver = true;
            return;
        }

        ChangeTurn();
    }

    private bool CheckCells(int x, int y)
    {
        for (int i = 0; i < grids.Count; i++)
        {
            if (grids[x][i] != players[turn] || grids[x][i] == Character.Blank)
                break;

            if (i == cellInRow - 1)
                return true;
        }

        for (int i = 0; i < grids.Count; i++)
        {
            if (grids[i][y] != players[turn] || grids[i][y] == Character.Blank)
                break;

            if (i == cellInRow - 1)
                return true;
        }

        for (int i = 0; i < grids.Count; i++)
        {
            if (grids[i][i] != players[turn] || grids[i][i] == Character.Blank)
                break;

            if (i == cellInRow - 1)
                return true;
        }

        for (int i = 0; i < grids.Count; i++)
        {
            if (grids[cellInRow - i - 1][i] != players[turn] || grids[cellInRow - i - 1][i] == Character.Blank)
                break;

            if (i == cellInRow - 1)
                return true;
        }

        return false;
    }

    private void ChangeTurn()
    {
        turn = turn == Player.A ? Player.B : Player.A;
    }
}

[System.Serializable]
public class RowButtons
{
    public List<CellButton> buttons;
}
