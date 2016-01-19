using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BoardStruct;
using GameObjectController;

public class MainController : MonoBehaviour
{
    // オブジェクトコントローラ
    [SerializeField] private BoardController boardController;
    [SerializeField] private TextMessageController textMessageController;
    [SerializeField] private ScoreBoardController scoreBoardController;
    [SerializeField] private LogBoardController logBoardController;
    [SerializeField] private PathButtonController pathButtonController;
    [SerializeField] private BackButtonController backButtonController;
    [SerializeField] private GiveUpButtonController giveUpButtonController;

    // 盤面データ
    private BoardInfo boardInfo;
    private List<BoardMatrix> boardMatrixLog;

    // ゲームの状態
    private GameState gameState;

    // 処理中はクリックを禁止する
    private bool isLockClick;

    // その他
    private ConstValues constValues = new ConstValues();
    private GetPlayerConstValues getPlayerConstValues = new GetPlayerConstValues();

    /* ------ FROM 進行管理 ------ */
    private void Start()
    {
        // ロック
        LockClick();

        // フィールド初期化
        boardInfo = new BoardInfo();
        boardMatrixLog = new List<BoardMatrix>();

        // オセロ盤を作成
        boardController.MakeBoard(boardInfo);

        // 石の初期配置
        PutStone(3, 3, BoardValues.White);
        PutStone(3, 4, BoardValues.Black);
        PutStone(4, 3, BoardValues.Black);
        PutStone(4, 4, BoardValues.White);

        // スコア表示更新
        scoreBoardController.UpdateScoreBoard(boardInfo);

        // ターン開始時処理
        SetGameState(GameState.BlackTurn);
        PrepareTurn();

        // ロック解除
        UnlockClick();
    }
    
    private void PrepareTurn()
    {
        // boardMatrixLogに追加
        AddBoardMatrixLog();

        // テキストメッセージ変更
        textMessageController.SetTurnMessage(gameState);

        // 置けるタイルのカラーを変更する
        boardController.SetNormalOrValidColorToAllTiles(gameState, boardInfo);

        // パスボタンの有効化・無効化
        UpdatePathButton();

        // 1手戻るボタンの有効化・無効化
        UpdateBackButton();
    }

    public void ReceiveClickEvent(GameObject clickedObject)
    {
        // ロック中はクリック不可
        if (IsLockClick())
        {
            return;
        }

        // タイルがクリックされた
        if (clickedObject.tag.Equals("Tile"))
        {
            // コルーチンにしているのは、Waitを使うため
            StartCoroutine("TileClicked", clickedObject);
        }

        // パスボタンがクリックされた
        if (clickedObject.name.Equals("PathButton"))
        {
            PathButtonClicked();
        }

        // 1手戻るボタンがクリックされた
        if (clickedObject.name.Equals("BackButton"))
        {
            BackButtonClicked();
        }

        // 降参ボタンがクリックされた
        if (clickedObject.name.Equals("GiveUpButton"))
        {
            GiveUpButtonClicked();
        }
    }

    private IEnumerator TileClicked(GameObject clickedTileObject)
    {
        // ロック
        LockClick();

        // タイルの座標取得
        BoardPoint putPoint = boardController.getPointOfTile(clickedTileObject);
        var putCol = putPoint.col;
        var putRow = putPoint.row;

        // ターン中のプレイヤーのカラー値
        BoardValues turnPlayerBoardValue = getPlayerConstValues.GetTurnPlayerBoardValue(gameState);

        // 有効なタイルでなければリターン
        if (!boardInfo.IsValidPoint(putCol, putRow, turnPlayerBoardValue))
        {
            UnlockClick();
            yield break;
        }

        // boardInfo上およびGUI上に石を置く
        PutStone(putCol, putRow, turnPlayerBoardValue);

        // 挟まれた石のリストを取得
        var reversePointList = boardInfo.GetReversePointList(putCol, putRow, turnPlayerBoardValue);

        // 挟まれた石を引っくり返し、BoardInfoの情報を更新
        foreach (BoardPoint reversePoint in reversePointList)
        {
            boardController.TurnStone(reversePoint.col, reversePoint.row);
            boardInfo.SetPoint(reversePoint.col, reversePoint.row, turnPlayerBoardValue);
        }

        // スコア表示を更新
        scoreBoardController.UpdateScoreBoard(boardInfo);

        // アニメーションの終了を待つ
        yield return new WaitForSeconds(constValues.AnimationWaitTime);

        // ログボードに１行追加
        logBoardController.AddPutLog(gameState, putCol, putRow);

        // 終了判定
        if (boardInfo.IsFull() || boardInfo.IsNoPutPoint())
        {
            // 結果表示
            textMessageController.SetResultMessage(boardInfo);

            // ゲーム終了
            FinishGame();

            yield break;
        }

        // 次のターン
        SwitchGameState();
        PrepareTurn();

        // アンロック
        UnlockClick();
    }

    private void PathButtonClicked()
    {
        // ロック
        LockClick();

        // ログボードにパスを追加
        logBoardController.AddPathLog(gameState);

        // 次のターン
        SwitchGameState();
        PrepareTurn();

        // アンロック
        UnlockClick();
    }

    private void BackButtonClicked()
    {
        // ロック
        LockClick();

        // boardInfoに2手前のboardMatrixをセット
        BoardMatrix beforeTwoBoardMatrixClone = boardMatrixLog[boardMatrixLog.Count - 3].Clone();
        boardInfo.SetBoardMatrix(beforeTwoBoardMatrixClone);

        // boardMatrixLogから3個削除（PrepareTurnで追加されるため）
        boardMatrixLog.RemoveAt(boardMatrixLog.Count - 1);
        boardMatrixLog.RemoveAt(boardMatrixLog.Count - 1);
        boardMatrixLog.RemoveAt(boardMatrixLog.Count - 1);

        // ログボードから2個削除
        logBoardController.RemoveLastLine();
        logBoardController.RemoveLastLine();

        // 石配置をリセット
        ResetStones();

        // スコア表示更新
        scoreBoardController.UpdateScoreBoard(boardInfo);

        // ターン開始時処理（GameStateはそのまま）
        PrepareTurn();

        // アンロック
        UnlockClick();
    }

    private void GiveUpButtonClicked()
    {
        // ロック
        LockClick();

        // テキストメッセージ更新
        var unTurnPlayerBoardValue = getPlayerConstValues.GetUnTurnPlayerBoardValue(gameState);
        textMessageController.SetWinMessage(unTurnPlayerBoardValue);

        // ゲーム終了
        FinishGame();
    }

    private void UpdatePathButton()
    {
        var turnPlayerBoardValue = getPlayerConstValues.GetTurnPlayerBoardValue(gameState);

        if (boardInfo.GetValidPointList(turnPlayerBoardValue).Count > 0)
        {
            pathButtonController.SetDisabled();
        }
        else
        {
            pathButtonController.SetEnabled();
        }
    }

    private void UpdateBackButton()
    {
        if (boardMatrixLog.Count > 2)
        {
            backButtonController.SetEnabled();
        }
        else
        {
            backButtonController.SetDisabled();
        }
    }

    private void AddBoardMatrixLog()
    {
        BoardMatrix clone = boardInfo.CloneBoardMatrix();
        boardMatrixLog.Add(clone);
    }

    private void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
    }

    private void SwitchGameState()
    {
        if (gameState == GameState.BlackTurn)
        {
            SetGameState(GameState.WhiteTurn);
        }
        else
        {
            SetGameState(GameState.BlackTurn);
        }
    }

    private void FinishGame()
    {
        pathButtonController.SetDisabled();
        backButtonController.SetDisabled();
        giveUpButtonController.SetDisabled();

        boardController.SetNormalColorToAllTiles(boardInfo);

        SetGameState(GameState.Finished);
    }
    /* ------ TO 進行管理 ------ */

    /* ------ FROM オセロボード操作 ------ */
    private void PutStone(int col, int row, BoardValues value)
    {
        // boardInfo上に石を置く
        boardInfo.SetPoint(col, row, value);

        // GUI上に石を置く
        boardController.SetStone(col, row, value);
    }

    private void ResetStones()
    {
        // 石を全て削除
        boardController.RemoveAllStones();

        // 石を再配置
        foreach (BoardPoint point in boardInfo.AllBoardPoint)
        {
            var boardValue = boardInfo.GetPoint(point.col, point.row);

            if (boardValue == BoardValues.Black || boardValue == BoardValues.White)
            {
                PutStone(point.col, point.row, boardValue);
            }
        }
    }
    /* ------ TO オセロボード操作 ------ */

    /* ------ FROM クリックを許可・不許可 ------ */
    private void LockClick()
    {
        isLockClick = true;
    }

    private void UnlockClick()
    {
        isLockClick = false;
    }

    private bool IsLockClick()
    {
        return isLockClick;
    }
    /* ------ TO クリックを禁止 ------ */
}
