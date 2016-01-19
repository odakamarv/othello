using UnityEngine;
using BoardStruct;
using System.Collections.Generic;

namespace GameObjectController.BaseController
{
    public class BaseBoardController : MonoBehaviour
    {
        [SerializeField] private GameObject tiles;
        [SerializeField] private GameObject tile;
        [SerializeField] private GameObject stones;
        [SerializeField] private StoneController stone;

        private Dictionary<string, StoneController> stoneDictionary = new Dictionary<string, StoneController>();
        private Dictionary<string, GameObject> tileDictionary = new Dictionary<string, GameObject>();
        private Dictionary<GameObject, BoardPoint> tilePointDictionary = new Dictionary<GameObject, BoardPoint>();

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
            tileDictionary["Tile" + col + row] = tile;
            tilePointDictionary[tile] = new BoardPoint(col, row);
        }

        public void SetStone(int col, int row, BoardValues value)
        {
            var setX = IncreaseX * row + OffsetX;
            var setZ = IncreaseZ * col + OffsetZ;

            var stone = Instantiate(this.stone);

            stone.GetComponent<Transform>().SetParent(stones.transform);
            stone.transform.position = new Vector3(setX, 0f, setZ);
            stoneDictionary["Stone" + col + row] = stone;

            if (value == BoardValues.Black)
            {
                stone.setBlack();
            }
            else
            {
                stone.setWhite();
            }
        }

        public void TurnStone(int col, int row)
        {
            var stone = stoneDictionary["Stone" + col + row];
            stone.Turn();
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
            var targetTile = tileDictionary["Tile" + col + row];
            targetTile.GetComponent<Renderer>().material = material;
        }

        public BoardPoint GetPointOfTile(GameObject tile)
        {
            return tilePointDictionary[tile];
        }
    }
}