using UnityEngine;
using BoardStruct;

/// <summary>
/// 黒ターン時または白ターン時の、自分または相手プレイヤーのカラー定数等を取得
/// </summary>
public class GetPlayerConstValues {

    private ConstValues constValues = new ConstValues();

    public BoardValues GetTurnPlayerBoardValue(GameState gameState)
    {
        if (gameState == GameState.BlackTurn)
        {
            return BoardValues.Black;
        }
        else
        {
            return BoardValues.White;
        }
    }

    public BoardValues GetUnTurnPlayerBoardValue(GameState gameState)
    {
        if (gameState == GameState.BlackTurn)
        {
            return BoardValues.White;
        }
        else
        {
            return BoardValues.Black;
        }
    }

    public Color GetTurnPlayerTextColor(GameState gameState)
    {
        if (gameState == GameState.BlackTurn)
        {
            return constValues.TextColorBlack;
        }
        else
        {
            return constValues.TextColorWhite;
        }
    }

    public Color GetUnTurnPlayerTextColor(GameState gameState)
    {
        if (gameState == GameState.BlackTurn)
        {
            return constValues.TextColorWhite;
        }
        else
        {
            return constValues.TextColorBlack;
        }
    }

    public string GetTurnPlayerColorString(GameState gameState)
    {
        if (gameState == GameState.BlackTurn)
        {
            return constValues.StringBlack;
        }
        else
        {
            return constValues.StringWhite;
        }
    }

    public string GetUnTurnPlayerColorString(GameState gameState)
    {
        if (gameState == GameState.BlackTurn)
        {
            return constValues.StringWhite;
        }
        else
        {
            return constValues.StringBlack;
        }
    }
}
