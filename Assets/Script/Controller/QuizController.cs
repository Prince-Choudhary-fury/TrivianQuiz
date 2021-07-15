using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizController : MonoBehaviour
{
    [Header("QuizQuation")]
    public List<Quiz> quiz;
    [Space(10)]
    public List<int> randomIndex = new List<int>();

    public GameObject[] options;
    public GameObject questionImage;
    public GameObject Barrier;
    public Text correctScore;
    public Text incorrectScore;
    public Text currentQuetionNumeber;
    public Text points;
    public Text finalCredits;
    public Text BestCredits;
    public Text Timer;
    public GameObject timerBar;
    public GameObject questionPanal;
    public GameObject scoreBoard;
    public GameObject[] progressBar;
    
    public static int currentScene;

    private Color optionColor;

    private bool isAnswered = false;

    private int seconds = 45;
    private float miliCount;
    private int maxQuestions = 7;
    public static int bestCredits;
    private int credits;
    private int pointCredits = 900;
    [SerializeField]
    private int[] optionSelected;
    private int questionAnswered = 0;
    private int randomNumber = 0;
    private int correctAnswered = 0;

    void Start()
    {
        if (WheelController.isChalange)
        {
            for (int i = 0; i < 7; i++)
            {
                progressBar[i].SetActive(false);
            }
            maxQuestions = 100;
        }
        else
        {
            maxQuestions = 7;
        }
        optionSelected = new int[maxQuestions];
        optionColor = options[0].GetComponent<Image>().color;
        currentScene = SceneManager.GetActiveScene().buildIndex;
        RandomIndexListGenerator(1, 16);
        GenerateQuation();
    }

    // Update is called once per frame
    void Update()
    {
        miliCount += Time.deltaTime * 10;
        if (miliCount >= 10)
        {
            seconds -= 1;
            miliCount = 0;
            Timer.text = "" + seconds;
        }
        if (seconds == 0 || isAnswered)
        {
            timerBar.SetActive(false);
            seconds = 45;
            timerBar.SetActive(true);
            isAnswered = false;
            GenerateQuation();
        }
    }

    public void AnswerCheker(int optionIndex)
    {
        StartCoroutine(CheckAnswer(optionIndex));
    }

    IEnumerator CheckAnswer(int optionIndex)
    {
        optionSelected[questionAnswered - 1] = optionIndex - 1;
        Barrier.SetActive(true);
        if (quiz[randomIndex[questionAnswered - 1] - 1].correctOption == optionIndex)
        {
            options[optionIndex - 1].GetComponent<Image>().color = Color.green;
            correctAnswered += 1;
            progressBar[questionAnswered - 1].GetComponent<Image>().color = Color.green;
            credits += pointCredits;
            points.text = credits + " pts";
            if (bestCredits <= credits)
            {
                bestCredits = credits;
            }
        }
        else
        {
            options[optionIndex - 1].GetComponent<Image>().color = Color.red;
            progressBar[questionAnswered - 1].GetComponent<Image>().color = Color.red;
            options[quiz[randomIndex[questionAnswered - 1] - 1].correctOption - 1].GetComponent<Image>().color = Color.green;
        }
        yield return new WaitForSeconds(2);
        Barrier.SetActive(false);
        isAnswered = true;
    }

    void SetAnswer()
    {
        for (int i = 0; i < 4; i++)
        {
            options[i].transform.GetChild(0).GetComponent<Text>().text = quiz[randomIndex[questionAnswered] - 1].Option[i];
            options[i].GetComponent<Image>().color = optionColor;
        }
    }

    public void GenerateQuation()
    {
        if (questionAnswered < 7)
        {
            if (questionAnswered > 0)
            {
                questionPanal.GetComponent<Animator>().Play("questionPanal");
            }
            Barrier.SetActive(true);
            StartCoroutine(setQuetions(randomIndex[questionAnswered] - 1));
            questionAnswered += 1;
        }
        else
        {
            finalCredits.text = "" + credits;
            BestCredits.text = "" + bestCredits;
            correctScore.text = "" + correctAnswered;
            incorrectScore.text = "" + (maxQuestions - correctAnswered);
            scoreBoard.SetActive(true);
        }
    }

    IEnumerator setQuetions(int questionNumber)
    {
        questionImage.GetComponent<Image>().sprite = quiz[questionNumber].question;
        SetAnswer();
        yield return new WaitForSeconds(1f);
        questionPanal.GetComponent<Animator>().Play("Empty");
        Barrier.SetActive(false);
    }

    public void RandomIndexListGenerator(int initialIndex, int lastIndex)
    {
        randomIndex = new List<int>(new int[maxQuestions]);
        for (int i = 0; i < maxQuestions; i++)
        {
            randomNumber = Random.Range(initialIndex, lastIndex);
            while (randomIndex.Contains(randomNumber))
            {
                randomNumber = Random.Range(initialIndex, lastIndex);
            }
            randomIndex[i] = randomNumber;
        }
    }

    public void ViewQuestions(int questionNumber)
    {
        timerBar.SetActive(false);
        Barrier.SetActive(true);
        currentQuetionNumeber.text = (questionNumber + 1) + "/" + maxQuestions;
        questionAnswered = questionNumber;
        ShowQuestion(randomIndex[questionNumber] - 1);
    }

    public void ShowQuestion(int questionNumber)
    {
        Debug.Log(questionAnswered);
        questionImage.GetComponent<Image>().sprite = quiz[questionNumber].question;
        SetAnswer();
        options[quiz[optionSelected[questionAnswered]].correctOption - 1].GetComponent<Image>().color = Color.green;
        if (optionSelected[questionAnswered] != (quiz[questionNumber].correctOption - 1))
        {
            options[quiz[questionNumber].correctOption - 1].GetComponent<Image>().color = Color.red;
        }
        StartCoroutine(Anim());
    }

    IEnumerator Anim()
    {
        yield return new WaitForSeconds(1);
        Barrier.SetActive(false);
        questionPanal.GetComponent<Animator>().Play("Empty");
    }

}
