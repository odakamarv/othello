using UnityEngine;
using BoardStruct;

namespace GameObjectController
{
    public class BoardController : MonoBehaviour
    {
        [SerializeField]
        private GameObject tiles;
        [SerializeField]
        private GameObject tile;
        [SerializeField]
        private GameObject stones;
        [SerializeField]
        private GameObject stone;
        [SerializeField]
        private Material tileMaterialNormal;
        [SerializeField]
        private Material tileMaterialValid;

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

        public void setTile(int col, int row)
        {
            var setX = IncreaseX * row + OffsetX;
            var setZ = IncreaseZ * col + OffsetZ;

            var tile = Instantiate(this.tile);

            tile.GetComponent<Transform>().SetParent(tiles.transform);
            tile.transform.position = new Vector3(setX, 0.0f, setZ);
            tile.name = "Tile" + col + row;
        }

        public void setStone(int col, int row, BoardValues value)
        {
            var setX = IncreaseX * row + OffsetX;
            var setZ = IncreaseZ * col + OffsetZ;

            var stone = Instantiate(this.stone);

            stone.GetComponent<Transform>().SetParent(stones.transform);
            stone.transform.position = new Vector3(setX, 0f, setZ);
            stone.name = "Stone" + col + row;

            if (value == BoardValues.Black)
            {
                stone.GetComponentInChildren<Animator>().SetTrigger("setBlack");
            }
            else
            {
                stone.GetComponentInChildren<Animator>().SetTrigger("setWhite");
            }
        }

        public void turnStone(int col, int row)
        {
            var stone = stones.transform.FindChild("Stone" + col + row);
            stone.GetComponentInChildren<Animator>().SetTrigger("doTurn");
        }

        public void RemoveAllStones()
        {
            foreach (Transform child in stones.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            stones.transform.DetachChildren();
        }

        public void SetAllTileNormalColor()
        {
            foreach (BoardPoint point in boardInfo.AllBoardPoint)
            {
                var targetTile = tiles.transform.FindChild("Tile" + point.col + point.row);
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