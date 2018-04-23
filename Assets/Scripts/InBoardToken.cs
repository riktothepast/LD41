using UnityEngine;
using System.Collections;

public class InBoardToken : MonoBehaviour {

    public Board board;
    [SerializeField]
    Vector3 positionOffset;
    [SerializeField]
    int health;
    [SerializeField]
    HUDIndicator hud;
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip move;
    int currentIndex;
    System.Action callback;

	void Start () {
        currentIndex = 0;
        transform.position = board.tiles[currentIndex].transform.position + positionOffset;
        hud.SetHealth(health);
    }

    public void DealDamage(int value)
    {
        health -= value;
        hud.GetShakeElement().Shake();
        hud.SetHealth(health);
    }

    public void AddHealth(int value)
    {
        health += value;
        if (health > 100)
        {
            health = 100;
        }
        hud.SetHealth(health);
    }

    public int GetHealth()
    {
        return health;
    }

    public BoardTile.Type GetCurrentOccupiedTileType()
    {
        return board.tiles[currentIndex].GetTileType();
    }

	public void Move (int amount, System.Action cb, bool reverse = false) {
        callback = cb;
        StartCoroutine(MovePiece(amount, reverse));
	}

    IEnumerator MovePiece(int amount, bool reverse)
    {
        for (int x = amount; x > 0; x = x - 1)
        {
            if (currentIndex + 1 > board.tiles.Count - 1) // oh no! 
            {
                reverse = true;
            }
            currentIndex += reverse ? -1 : 1;
            currentIndex = currentIndex < 0 ? 0 : currentIndex;
            transform.position = board.tiles[currentIndex].transform.position + positionOffset;
            audioSource.clip = move;
            audioSource.Play();
            yield return new WaitForSeconds(0.2f);
        }
        callback();
    }
}
