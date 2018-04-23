using UnityEngine;

public class BoardTile : MonoBehaviour {

    [SerializeField]
    Type tileType;

    public enum Type
    {
        normal,
        attack,
        attackX2,
        increaseHealth,
        backwards,
        goal
    }


    public Type GetTileType()
    {
        return tileType;
    }
}
