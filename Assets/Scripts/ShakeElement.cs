using UnityEngine;

public class ShakeElement : MonoBehaviour {

    [SerializeField]
    float shakeAmount = 10f;
    [SerializeField]
    float decreaseFactor = 2f;
    Vector3 originalPosition;
    float shakeDuration;

	void Start () {
        originalPosition = transform.localPosition;
    }

    public void Shake()
    {
        originalPosition = transform.localPosition;
        shakeDuration = shakeAmount;
    }

    void Update () {
        if (shakeDuration > 0)
        {
            transform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        } else
        {
            shakeDuration = 0f;
            transform.localPosition = originalPosition;
        }
    }
}
