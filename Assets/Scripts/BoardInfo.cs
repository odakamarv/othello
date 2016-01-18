using System.Collections.Generic;
using BoardStruct;

public class BoardInfo
{
    private BoardMatrix boardMatrix;
    public readonly List<BoardPoint> AllBoardPoint;

    public BoardInfo()
    {
        boardMatrix = new BoardMatrix(BoardValues.Empty);

        AllBoardPoint = new List<BoardPoint>();
        for(var col=0; col<8; col++)
        {
            for(var row=0; row<8; row++)
            {
                AllBoardPoint.Add(new BoardPoint(col, row));
            }
        }
    }

    public void SetPoint(int col, int row, BoardValues value)
    {
        boardMatrix.Set(col, row, value);
    }

    public BoardValues GetPoint(int col, int row)
    {
        return boardMatrix.Get(col, row);
    }

    public void SetBoardMatrix(BoardMatrix boardMatrix)
    {
        this.boardMatrix = boardMatrix;
    }

    public BoardMatrix CloneBoardMatrix()
    {
        return boardMatrix.Clone();
    }

    public int CountBlack()
    {
        var countBlack = 0;

        foreach(BoardPoint point in AllBoardPoint)
        {
            if (boardMatrix.Get(point.col, point.row) == BoardValues.Black)
            {
                countBlack++;
            }
        }

        return countBlack;
    }

    public int CountWhite()
    {
        var countWhite = 0;

        foreach (BoardPoint point in AllBoardPoint)
        {
            if (boardMatrix.Get(point.col, point.row) == BoardValues.White)
            {
                countWhite++;
            }
        }

        return countWhite;
    }

    public bool IsFull()
    {
        var isFull = true;

        foreach (BoardPoint point in AllBoardPoint)
        {
            if (boardMatrix.Get(point.col, point.row) == BoardValues.Empty)
            {
                isFull = false;
                break;
            }
        }

        return isFull;
    }

    public bool IsNoPutPoint()
    {
        var isNoPutPoint = true;

        foreach (BoardPoint point in AllBoardPoint)
        {
            if (boardMatrix.Get(point.col, point.row) != BoardValues.Empty)
            {
                continue;
            }

            if (GetReversePointList(point.col, point.row, BoardValues.Black).Count > 0 ||
                GetReversePointList(point.col, point.row, BoardValues.White).Count > 0)
            {
                isNoPutPoint = false;
                break;
            }
        }

        return isNoPutPoint;
    }

    /// <summary>
    /// 石を置いた座標から、8方向を探索し、引っくり返しが発生する座標のリストを作成
    /// </summary>
    public List<BoardPoint> GetReversePointList(int putCol, int putRow, BoardValues putColor)
    {
        var reversePointList = new List<BoardPoint>();
        var tmpReversePointList = new List<BoardPoint>();

        var myColor = putColor;
        var targetColor = (myColor == BoardValues.Black) ? BoardValues.White : BoardValues.Black;

        // 探索の共通処理
        System.Func<int, int, bool> searchProcess = (col, row) =>
        {
            var searchPointValue = boardMatrix.Get(col, row);

            // 空
            if (searchPointValue == BoardValues.Empty)
            {
                return true;
            }

            // 置かれた石と同じ色
            if (searchPointValue == myColor)
            {
                reversePointList.AddRange(tmpReversePointList);
                return true;
            }

            // 置かれた石と違う色
            if (searchPointValue == targetColor)
            {
                tmpReversePointList.Add(new BoardPoint(col, row));
            }

            return false;
        };

        // 北方向
        for (var col = putCol - 1; col >= 0; col--)
        {
            if (searchProcess(col, putRow))
            {
                break;
            }
        }
        tmpReversePointList.Clear();

        // 北東方向
        for (int col = putCol - 1, row = putRow + 1; col >= 0 && row <= 7; col--, row++)
        {
            if (searchProcess(col, row))
            {
                break;
            }
        }
        tmpReversePointList.Clear();

        // 東方向
        for (var row = putRow + 1; row <= 7; row++)
        {
            if (searchProcess(putCol, row))
            {
                break;
            }
        }
        tmpReversePointList.Clear();

        // 南東方向
        for (int col = putCol + 1, row = putRow + 1; col <= 7 && row <= 7; col++, row++)
        {
            if (searchProcess(col, row))
            {
                break;
            }
        }
        tmpReversePointList.Clear();

        // 南方向
        for (var col = putCol + 1; col <= 7; col++)
        {
            if (searchProcess(col, putRow))
            {
                break;
            }
        }
        tmpReversePointList.Clear();

        // 南西方向
        for (int col = putCol + 1, row = putRow - 1; col <= 7 && row >= 0; col++, row--)
        {
            if (searchProcess(col, row))
            {
                break;
            }
        }
        tmpReversePointList.Clear();

        // 西方向
        for (var row = putRow - 1; row >= 0; row--)
        {
            if (searchProcess(putCol, row))
            {
                break;
            }
        }
        tmpReversePointList.Clear();

        // 北西方向
        for (int col = putCol - 1, row = putRow - 1; col >= 0 && row >= 0; col--, row--)
        {
            if (searchProcess(col, row))
            {
                break;
            }
        }
        tmpReversePointList.Clear();

        return reversePointList;
    }

    public bool IsValidPoint(int putCol, int putRow, BoardValues putColor)
    {
        if (GetReversePointList(putCol, putRow, putColor).Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<BoardPoint> GetValidPointList(BoardValues putColor)
    {
        var validPointList = new List<BoardPoint>();

        foreach (BoardPoint point in AllBoardPoint)
        {
            if (boardMatrix.Get(point.col, point.row) == BoardValues.Empty)
            {
                if(IsValidPoint(point.col, point.row, putColor))
                {
                    validPointList.Add(new BoardPoint(point.col, point.row));
                }
            }
        }

        return validPointList;
    }
}