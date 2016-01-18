using UnityEngine;

public class ConstValues{
    public readonly string StringBlack = "黒";
    public readonly string StringWhite = "白";

    public readonly string TextFormatTurn = "$の番です。";
    public readonly string TextFormatWin = "ゲーム終了、$の勝ちです。";
    public readonly string TextFormatDraw = "ゲーム終了、引き分けです。";
    public readonly string TextFormatScore = "$：{0:D}個";
    public readonly string TextFormatPutLog = "$：X={0:D}, Y={1:D}";
    public readonly string TextFormatPathLog = "$：パス";

    public readonly Color TextColorBlack = Color.black;
    public readonly Color TextColorWhite = Color.white;
    public readonly Color TextColorOther = Color.gray;

    public readonly float AnimationWaitTime = 0.55f;
}
