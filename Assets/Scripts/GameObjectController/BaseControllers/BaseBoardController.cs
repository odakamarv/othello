using UnityEngine;
using BoardStruct;

namespace BaseController
{
    public class BaseBoardController : MonoBehaviour
    {
        [SerializeField] private GameObject tiles;
        [SerializeField] private GameObject tile;
        [SerializeField] private GameObject stones;
        [SerializeField] private GameObject stone;

        // 座標情報
        private const float OffsetX = -35f;
        private const float OffsetZ = 35f;
        private const float IncreaseX = 10f;
        private const float IncreaseZ = -10f;

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

            if (value == BoardValues.Black)
            {
                stone.GetComponentInChildren<Animator>().SetTrigger("setBlack");
            }
            else
            {
                stone.GetComponentInChildren<Animator>().SetTrigger("setWhite");
            }
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
                Destroy(child.gameObject);
            }
            stones.transform.DetachChildren();
        }

        public void SetColorToTile(int col, int row, Material material)
        {
            var targetTile = tiles.transform.FindChild("Tile" + col + row);
            targetTile.GetComponent<Renderer>().material = material;
        }

        public BoardPoint getPointOfTile(GameObject tile)
        {
            // nameから座標を取得 （例: Tile04 → Point(0, 4)）
            var col = int.Parse(tile.name.Substring(tile.name.Length - 2, 1));
            var row = int.Parse(tile.name.Substring(tile.name.Length - 1, 1));

            return new BoardPoint(col, row);
        }
    }
}