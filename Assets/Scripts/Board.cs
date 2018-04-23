using UnityEngine;
using System.Collections.Generic;

public class Board : MonoBehaviour {

    public List<BoardTile> tiles;
    public BoardTile health;
    public BoardTile attack;
    public BoardTile attackX2;
    public BoardTile goal;

    void Awake () {
        tiles = new List<BoardTile>();

        for (int x = 0; x < transform.childCount; x = x + 1)
        {
            tiles.Add(transform.GetChild(x).GetComponent<BoardTile>());
        }

        // lets add some random elements

        // goal
        BoardTile goalTile = Instantiate(goal, transform);
        int index = tiles.Count - 1;
        goalTile.transform.position = tiles[index].transform.position;
        Destroy(tiles[index].gameObject);
        tiles.Remove(tiles[index]);
        tiles.Insert(index, goalTile);

        AddTiles(attack, 8);
        AddTiles(attackX2, 4);
        AddTiles(health, 4);
    }

    void AddTiles(BoardTile tile, int count)
    {
        for (int x = 0; x < count; x = x + 1)
        {
            BoardTile attackTile = Instantiate(tile, transform);
            int index = GetRandomTileIndex();
            attackTile.transform.position = tiles[index].transform.position;
            Destroy(tiles[index].gameObject);
            tiles.Remove(tiles[index]);
            tiles.Insert(index, attackTile);
        }
    }

    int GetRandomTileIndex()
    {
        int index = Random.Range(1, tiles.Count - 2);
        if (tiles[index].GetTileType() != BoardTile.Type.normal)
        {
            return GetRandomTileIndex();
        }
        return index;
    }

}
