using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class QuizeManager : MonoBehaviour
{
    public List<QuestionAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public Text questionText;

    public Text counterText;
    private int counter;

    public GameObject quizPanel;
    public GameObject gameOverPanel;

    public Text scoreText;
    private int totalQuestion;
    public int scoreCount;

    public Animator anim;


    public Text TimerText;
    private int timer;
    private int remaningTime;
    private bool keepcounting;
    //public Text remanigTime_Text;

    public GameObject StartGame_Panel;

    private void Start()
    {
        anim = quizPanel.GetComponent<Animator>();
        totalQuestion = QnA.Count;
        StartGame_Panel.SetActive(true);
        gameOverPanel.SetActive(false);
        scoreCount = 0;
        counter = 0;
        counterText.text = counter.ToString();
        currentQuestion = 0;
    }

    public void StartButtonPress()
    {
        StartGame_Panel.SetActive(false);
        GenerateQuestion();
        timer = 900;
        remaningTime = timer;
        keepcounting = true;
        StartCoroutine(UpdateTimer());

        anim.Play("arrange");
    }


    public void Correct()
    {
        scoreCount += 5;
        QnA.RemoveAt(currentQuestion);
        GenerateQuestion();
        //counter++;
        //counterText.text = counter.ToString();
        //anim.Play("arrange");
    }
    public void Wrong()
    {
        QnA.RemoveAt(currentQuestion);
        //counter++;
        //counterText.text = counter.ToString();
        GenerateQuestion();
        //anim.Play("arrange");
    }

    void SetAnswers()
    {
        for(int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].answers[i];

            if(QnA[currentQuestion].correctAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    void GenerateQuestion()
    {
        if(QnA.Count > 0)
        {
            anim.Play("arrange");
            counter += 1; ;
            counterText.text = counter.ToString();
            //StartCoroutine(TypeScenetences(QnA[currentQuestion].questions));
            questionText.text = QnA[currentQuestion].questions;
            AudioManager.instance.KBC();
            SetAnswers();
        }
        else
        {
            questionText.text = "...";
            Debug.Log("Out of Questions");
            Gameover();
        }
    }

    IEnumerator TypeScenetences(string scetence)
    {
        questionText.text = "";
        foreach (char letter in scetence.ToCharArray())
        {
            questionText.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator UpdateTimer()
    {
        while(keepcounting)
        {
            if(remaningTime > 0)
            {
                UpdateUI(remaningTime);
                remaningTime--;
                yield return new WaitForSeconds(1f);
            }
            else
            {
                keepcounting = false;
                ResetTimer();
            }
        }

    }

    private void UpdateUI(int seconds)
    {
        TimerText.text = string.Format("{0:D2}:{1:D2}", seconds / 60, seconds % 60);

    }

    private void ResetTimer()
    {
        TimerText.text = "00:00";
        timer = remaningTime = 0;
        Gameover();
    }

    public void Gameover()
    {
        keepcounting = false;
        AudioManager.instance.Stop();
        //gameOverPanel.SetActive(true);
        //quizPanel.SetActive(false);
        //scoreText.text = "Score: " + scoreCount + " / " + totalQuestion;

        //remanigTime_Text.text = "Time" + remaningTime.ToString() + "sec";

        gameOverPanel.SetActive(true);
        urlSplit.instance.marks = scoreCount.ToString();
        urlSplit.instance.time = (900 - remaningTime).ToString();
        urlSplit.instance.CallSaveData();
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
