using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizeManager quizeManager;

    public Button button;

    public Sprite orignalSprite;
    public Sprite red;
    public Sprite green;
    private static bool islock;

    private void Start()
    {
    }
    public void Answers()
    {
        if (!islock)
        {
            if (isCorrect)
            {
                Debug.Log("Correct");
                islock = true;
                //button.image.sprite = green;
                GetComponent<Image>().color = Color.green;
                //AudioManager.instance.Correct();
                StartCoroutine(WaitCorrect());
            }
            else
            {
                Debug.Log("Wrong");
                islock = true;
                //button.image.sprite = red;
                GetComponent<Image>().color = Color.red;
                //AudioManager.instance.Error();
                StartCoroutine(WaitWrong());
            }
        }
        
    }



    IEnumerator WaitCorrect()
    {
        yield return new WaitForSeconds(1f);
        quizeManager.anim.Play("rev");
        yield return new WaitForSeconds(1f);
        islock = false;
        //button.image.sprite = orignalSprite;
        GetComponent<Image>().color = Color.white;
        quizeManager.Correct();

    }

    IEnumerator WaitWrong()
    {
        yield return new WaitForSeconds(1f);
        quizeManager.anim.Play("rev");
        yield return new WaitForSeconds(1f);
        islock = false;
        GetComponent<Image>().color = Color.white;
        //button.image.sprite = orignalSprite;
        quizeManager.Wrong();

    }
}
