using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour {

    [SerializeField]
    Canvas canvas;
    [SerializeField]
    MessageScript messagePrefab;
    GameState currentState;
    public InBoardToken player, rival;
    public Dice dice;
    InBoardToken currentPlayer;

    [SerializeField]
    ParticleSystem punchParticles;

    [SerializeField]
    int attackForce = 10;
    [SerializeField]
    int healthRecovery = 10;

    [SerializeField]
    AudioClip hit, health;

    [SerializeField]
    AudioSource source;

    [SerializeField]
    SceneLoader loader;

    public enum GameState {
        DecideTurn,
        ChangeTurn,
        PlayerTurn,
        DiceThrow,
        TileResolution
    }

	void Start () {
        currentState = GameState.DecideTurn;
        StartCoroutine(PerformAction(2f, DecideTurn));
    }

    public void DecideTurn()
    {
        int turn = Random.Range(0, 2);
        currentPlayer = turn > 0 ? rival : player;
        MessageScript msg = Instantiate(messagePrefab, canvas.transform);
        msg.SetText(currentPlayer.name + " goes first!");
        StartCoroutine(PerformAction(1f, DiceThrow));
    }

    void ChangeScene()
    {
        loader.LoadScene();
    }

    public void ChangePlayerTurn()
    {
        if (rival.GetHealth() <= 0 || player.GetHealth() <= 0)
        {
            string pnme = rival.GetHealth() <= 0 ? player.name : rival.name;
            MessageScript msg = Instantiate(messagePrefab, canvas.transform);
            msg.SetText(pnme + " won the game!");
            StartCoroutine(PerformAction(2f, ChangeScene));
        } else
        {
            currentPlayer = currentPlayer == player ? rival : player;
            MessageScript msg = Instantiate(messagePrefab, canvas.transform);
            msg.SetText("Ready " + currentPlayer.name);
            StartCoroutine(PerformAction(1f, DiceThrow));
        }
    }

    public void ChangeState(GameState state)
    {
        currentState = state;
    }

    void DiceResolution()
    {
        StartCoroutine(PerformAction(1f, PlayerMovement));
    }

    public void DiceThrow()
    {
        dice.SetThrowable(true, DiceResolution);
    }

    void PlayerMovementDone()
    {
        StartCoroutine(PerformAction(1f, TileResolution));
    }

    public void PlayerMovement()
    {
        currentPlayer.Move(dice.GetDiceValue(), PlayerMovementDone);
    }

    InBoardToken GetTargetPlayer()
    {
        return currentPlayer == player ? rival : player;
    }

    public void TileResolution()
    {
        BoardTile.Type type = currentPlayer.GetCurrentOccupiedTileType();
        Debug.Log(type);
        switch (type)
        {
            case BoardTile.Type.attack:
                GetTargetPlayer().DealDamage(attackForce);
                source.clip = hit;
                source.Play();
                punchParticles.Emit(1);
                break;
            case BoardTile.Type.attackX2:
                source.clip = hit;
                source.Play();
                GetTargetPlayer().DealDamage(attackForce * 2);
                punchParticles.Emit(2);
                break;
            case BoardTile.Type.increaseHealth:
                source.clip = health;
                source.Play();
                currentPlayer.AddHealth(healthRecovery);
                break;
            case BoardTile.Type.goal:
                source.clip = hit;
                source.Play();
                GetTargetPlayer().DealDamage(attackForce * 10);
                punchParticles.Emit(10);
                break;
            default:
                break;
        }
        StartCoroutine(PerformAction(1f, ChangePlayerTurn));
    }

    IEnumerator PerformAction(float time, System.Action action)
    {
        yield return new WaitForSeconds(time);
        action();
    }
}
