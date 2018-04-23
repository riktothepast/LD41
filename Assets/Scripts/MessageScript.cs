using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageScript : MonoBehaviour {

    [SerializeField]
    Text text;
    [SerializeField]
    CanvasGroup cGroup;
    [SerializeField]
    float fadingConstant = 2f;
    [SerializeField]
    float fadingWait = 0.5f;

    private void Start()
    {
        StartCoroutine(Fade());
    }

    public void SetText(string data)
    {
        text.text = data;
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(fadingWait);
        while (cGroup.alpha > 0)
        {
            cGroup.alpha -= Time.deltaTime * fadingConstant;
            yield return new WaitForEndOfFrame();
        }

        Destroy(this.gameObject);

    }

}
