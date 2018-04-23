using UnityEngine;
using UnityEngine.UI;

public class HUDIndicator : MonoBehaviour {

    [SerializeField]
    Slider health;
    [SerializeField]
    ShakeElement shakeElement;
    int healthValue;


	void Update () {
        health.value = Mathf.MoveTowards(health.value, healthValue, Time.deltaTime * 100f);
	}

    public void SetHealth(int value)
    {
        healthValue = value;
    }

    public ShakeElement GetShakeElement()
    {
        return shakeElement;
    }
}
