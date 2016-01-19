using UnityEngine;
using System.Collections.Generic;
using BoardStruct;
using BaseController;

/// <summary>
/// BaseBoardController機能以外のオセロに必要な機能
/// </summary>
namespace GameObjectController
{
    public class BoardController : BaseBoardController
    {
        [SerializeField] private Material tileMaterialNormal;
        [SerializeField] private Material tileMaterialValid;

        private GetPlayerConstValues getPlayerConstValues = new GetPlayerConstValues();

        public void MakeBoard(BoardInfo boardInfo)
        {
            foreach (BoardPoint point in boardInfo.AllBoardPoint)
            {
                SetTile(point.col, point.row);
            }
        }

        public void SetNormalColorToTile(int col, int row)
        {
            SetColorToTile(col, row, tileMaterialNormal);
        }

        public void SetNormalColorToAllTiles(BoardInfo boardInfo)
        {
            foreach (BoardPoint point in boardInfo.AllBoardPoint)
            {
                SetNormalColorToTile(point.col, point.row);
            }
        }

        public void SetValidColorToTile(int col, int row)
        {
            SetColorToTile(col, row, tileMaterialValid);
        }

        public void SetNormalOrValidColorToAllTiles(GameState gameState, BoardInfo boardInfo)
        {
            // 全てのタイルカラーをNormalにする
            SetNormalColorToAllTiles(boardInfo);

            // 有効なタイルカラーを変更する
            BoardValues turnPlayerBoardValue = getPlayerConstValues.GetTurnPlayerBoardValue(gameState);
            List<BoardPoint> validPointList = boardInfo.GetValidPointList(turnPlayerBoardValue);

            foreach (BoardPoint point in validPointList)
            {
                SetValidColorToTile(point.col, point.row);
            }
        }
    }
}