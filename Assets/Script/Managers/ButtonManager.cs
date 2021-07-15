using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public QuizController quizController;

    public GameObject questionPanal;
    public GameObject ScoreBoard;
    public GameObject upBarTwo;
    public GameObject Previous;
    public GameObject Next;

    public AudioSource Tap;

    private int currentScene;
    private int currentQuestion = 0;

    [Header("Only for Menu Pannel")]
    public GameObject categoryPanal;
    public GameObject countDownPanal;
    private float countDownWait = 0.5f;
    public AudioSource countDown;

    private void Update()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene != 0)
        {
            if (currentQuestion == 0)
            {
                Previous.GetComponent<Button>().interactable = false;
            }
            else
            {
                Previous.GetComponent<Button>().interactable = true;
            }
            if (currentQuestion == 6)
            {
                Next.GetComponent<Button>().interactable = false;
            }
            else
            {
                Next.GetComponent<Button>().interactable = true;
            }
        }
    }

    public void MainMenu()
    {
        StartCoroutine(SceneLoader(0));
    }

    IEnumerator SceneLoader(int sceneIndex)
    {
        Tap.Play();
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene(sceneIndex);
    }

    public void viewGame()
    {
        Tap.Play();
        if (currentScene != 0)
        {
            currentQuestion = 0;
            quizController.ViewQuestions(currentQuestion);
            ScoreBoard.SetActive(false);
            upBarTwo.SetActive(true);
        }
    }

    public void BackToScor()
    {
        Tap.Play();
        if (currentScene != 0)
        {
            ScoreBoard.SetActive(true);
            upBarTwo.SetActive(false);
        }
    }

    public void PreviousQuetion()
    {
        Tap.Play();
        if (currentQuestion > 0)
        {
            questionPanal.GetComponent<Animator>().Play("questionPanal");
            currentQuestion -= 1;
            quizController.ViewQuestions(currentQuestion);
        }
    }

    public void NextQuestion()
    {
        Tap.Play();
        if (currentScene != 0)
        {
            questionPanal.GetComponent<Animator>().Play("PreviousQuestion");
            currentQuestion += 1;
            quizController.ViewQuestions(currentQuestion);
        }
    }

    public void CategoryButton()
    {
        Tap.Play();
        categoryPanal.SetActive(true);
    }

    public void GeoButton()
    {
        StartCoroutine(LevelLoader(1));
    }
    
    public void EntButton()
    {
        StartCoroutine(LevelLoader(2));
    }
    
    public void HisButton()
    {
        StartCoroutine(LevelLoader(3));
    }
    
    public void ArtButton()
    {
        StartCoroutine(LevelLoader(4));
    }

    public void sciButton()
    {
        StartCoroutine(LevelLoader(5));
    }

    public void SportButton()
    {
        StartCoroutine(LevelLoader(6));
    }
    
    public void MusicButton()
    {
        StartCoroutine(LevelLoader(7));
    }
    
    public void movieButton()
    {
        StartCoroutine(LevelLoader(8));
    }
    
    public void footballButton()
    {
        StartCoroutine(LevelLoader(9));
    }

    IEnumerator LevelLoader(int index)
    {
        Tap.Play();
        yield return new WaitForSeconds(countDownWait);
        countDownPanal.SetActive(true);
        countDown.Play();
        countDownPanal.transform.GetChild(0).GetComponent<Text>().text = "3";
        yield return new WaitForSeconds(countDownWait);
        countDown.Play();
        countDownPanal.transform.GetChild(0).GetComponent<Text>().text = "2";
        yield return new WaitForSeconds(countDownWait);
        countDown.Play();
        countDownPanal.transform.GetChild(0).GetComponent<Text>().text = "1";
        SceneManager.LoadScene(index);
    }

}
