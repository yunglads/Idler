using TMPro;
using UnityEngine;
using System.Collections;

public class RaidingTextAnimator : MonoBehaviour
{
    public TMP_Text raidingText;
    public string fullText = "Raiding...";
    public float letterDelay = 0.2f;
    public float resetDelay = 1.5f;

    private void OnEnable()
    {
        StartCoroutine(TypeTextLoop());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator TypeTextLoop()
    {
        while (true)
        {
            raidingText.text = "";

            for (int i = 0; i < fullText.Length; i++)
            {
                raidingText.text += fullText[i];
                yield return new WaitForSeconds(letterDelay);
            }

            yield return new WaitForSeconds(resetDelay);
        }
    }
}

