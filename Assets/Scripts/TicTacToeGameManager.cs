using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TicTacToeGameManager : MonoBehaviour
{
    public Button[] buttons;
    public TextMeshProUGUI[] texts = new TextMeshProUGUI[9];
    public TextMeshProUGUI infoText;
    private string[] board = new string[9];
    private string currentPlayer = "X";

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
            texts[i] = buttons[i].GetComponentInChildren<TextMeshProUGUI>();
            texts[i].text = "";
            board[i] = "";
        }

        infoText.text = "Player " + currentPlayer;
    }

    public void ResetGame()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            board[i] = "";
            texts[i].text = "";
            buttons[i].interactable = true;
            ChangeColor(i, true);
        }
        currentPlayer = "X";
        infoText.text = "Player " + currentPlayer;
    }

    public void OnButtonClick(int index)
    {
        if (board[index] == "")
        {
            board[index] = currentPlayer;
            texts[index].text = currentPlayer;
            buttons[index].interactable = false;

            if (CheckWin())
            {
                Debug.Log(currentPlayer + " Wins!");
                infoText.text = "Player " + currentPlayer + " Win!!";
                EndGame();
            }
            else if (CheckDraw())
            {
                Debug.Log("Draw!");
                infoText.text = "Draw!!";
                EndGame();
            }
            else
            {
                SwitchPlayer();
            }
        }
        else
        {
            Debug.Log("Error!");
        }
    }

    void ChangeColor(int index,bool reset = false)
    {
        ColorBlock cb = buttons[index].colors;
        if(reset)
        {
            cb.disabledColor = new Color32(200, 200, 200, 128);
        }
        else
        {
            cb.disabledColor = Color.red;
        }
        buttons[index].colors = cb;
    }

    bool CheckWin()
    {
        for (int i = 0; i < 9; i += 3)
        {
            if (board[i] != "" && board[i] == board[i + 1] && board[i] == board[i + 2])
            {
                ChangeColor(i);
                ChangeColor(i+1);
                ChangeColor(i+2);
                return true;
            }
        }
        for (int i = 0; i < 3; i++)
        {
            if (board[i] != "" && board[i] == board[i + 3] && board[i] == board[i + 6])
            {
                ChangeColor(i);
                ChangeColor(i + 3);
                ChangeColor(i + 6);
                return true;
            }
        }
        if (board[0] != "" && board[0] == board[4] && board[0] == board[8])
        {
            ChangeColor(0);
            ChangeColor(4);
            ChangeColor(8);
            return true;
        }
        if (board[2] != "" && board[2] == board[4] && board[2] == board[6])
        {
            ChangeColor(2);
            ChangeColor(4);
            ChangeColor(6);
            return true;
        }

        return false;
    }

    bool CheckDraw()
    {
        foreach (string cell in board)
        {
            if (cell == "")
                return false;
        }
        return true;
    }

    void SwitchPlayer()
    {
        currentPlayer = (currentPlayer == "X") ? "O" : "X";
        infoText.text = "Player " + currentPlayer;
    }

    void EndGame()
    {
        foreach (Button btn in buttons)
        {
            btn.interactable = false;
        }
    }
}
