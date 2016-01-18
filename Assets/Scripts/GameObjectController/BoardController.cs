using UnityEngine;
using BoardStruct;
using System.Linq;

namespace GameObjectController
{
    public class BoardController : MonoBehaviour
    {
        [SerializeField] readonly private GameObject tiles;
        [SerializeField] readonly private GameObject tile;
        [SerializeField] readonly private GameObject stones;
        [SerializeField] readonly private GameObject stone;
        [SerializeField] readonly private Material tileMaterialNormal;
        [SerializeField] readonly private Material tileMaterialValid;

        private BoardInfo boardInfo;

        // 座標情報
        private const float OffsetX = -35f;
        private const float OffsetZ = 35f;
        private const float IncreaseX = 10f;
        private const float IncreaseZ = -10f;

        public BoardController()
        {
            boardInfo = new BoardInfo();
        }

        public void SetTile(int col, int row)
        {
            var setX = IncreaseX * row + OffsetX;
            var setZ = IncreaseZ * col + OffsetZ;

            var tile = Instantiate(this.tile);

            tile.GetComponent<Transform>().SetParent(tiles.transform);
            tile.transform.position = new Vector3(setX, 0.0f, setZ);
            tile.name = "Tile" + col + row;
        }

        public void SetStone(int col, int row, BoardValues value)
        {
            var setX = IncreaseX * row + OffsetX;
            var setZ = IncreaseZ * col + OffsetZ;

            var stone = Instantiate(this.stone);

            stone.GetComponent<Transform>().SetParent(stones.transform);
            stone.transform.position = new Vector3(setX, 0f, setZ);
            stone.name = "Stone" + col + row;

            stone.GetComponentInChildren<Animator>().SetTrigger(value == BoardValues.Black ? "setBlack" : "setWhite");
        }

        public void TurnStone(int col, int row)
        {
            var stone = stones.transform.FindChild("Stone" + col + row);
            stone.GetComponentInChildren<Animator>().SetTrigger("doTurn");
        }

        public void RemoveAllStones()
        {
            foreach (Transform child in stones.transform)
            {
                GameObject.Destroy(child);
            }
            stones.transform.DetachChildren();
        }

        public void SetAllTileNormalColor()
        {
            foreach (var targetTile in boardInfo.AllBoardPoint.Select(point => tiles.transform.FindChild("Tile" + point.col + point.row))){
                targetTile.GetComponent<Renderer>().material = tileMaterialNormal;
            }
        }

        public void SetTileValidColor(int col, int row)
        {
            var targetTile = tiles.transform.FindChild("Tile" + col + row);
            targetTile.GetComponent<Renderer>().material = tileMaterialValid;
        }
    }
}