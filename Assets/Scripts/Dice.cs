using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class Dice : MonoBehaviour {

    CanvasGroup cGroup;
    bool canBeThrown, update;
    System.Action callback;
    int currentValue;
    public Image image;
    public List<Sprite> diceImages;
    ShakeElement shake;
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip move;

    void Awake () {
        cGroup = GetComponentInParent<CanvasGroup>();
        shake = GetComponent<ShakeElement>();
        cGroup.alpha = 0;
	}

    void CheckStatus()
    {
        update = true;
    }

    public void Throw() {
        if (!canBeThrown)
        {
            return;
        }
        StartCoroutine(Shuffle());
    }

    public void SetThrowable(bool state, System.Action cb)
    {
        cGroup.alpha = 1;
        canBeThrown = state;
        callback = cb;
    }

    IEnumerator Shuffle()
    {
        canBeThrown = false;
        int randomRotations = Random.Range(10, 20);
        for (int x = 0; x < randomRotations; x = x + 1)
        {
            int randomImage = Random.Range(0, diceImages.Count);
            image.sprite = diceImages[randomImage];
            currentValue = randomImage + 1;
            shake.Shake();
            audioSource.clip = move;
            audioSource.Play();
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        cGroup.alpha = 0;
        callback();
    }

    public int GetDiceValue()
    {
        return currentValue;
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && canBeThrown) {
            StartCoroutine(Shuffle());
            canBeThrown = false;
        }
    }
}
