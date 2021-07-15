using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WheelController : MonoBehaviour
{

    private int randomValue;
    private float timeInterval;
    private bool coroutineAllowed;
    private bool loadScene = false;
    private int finalAngle;
    public AudioSource Tap;
    public AudioSource wheelAudio;
    public AudioSource countDown;

    public GameObject countDownPanal;

    private float countDownWait = 0.5f;

    public static bool isChalange = false;

    // Start is called before the first frame update
    void Start()
    {
        coroutineAllowed = true;
    }

    // Update is called once per frame
    public void WheelUpdate()
    {
        Tap.Play();
        if (coroutineAllowed)
        {
            StartCoroutine(WheelSpin());
        }
    }

    IEnumerator WheelSpin()
    {
        wheelAudio.Play();
        coroutineAllowed = false;
        randomValue = Random.Range(20, 30);
        timeInterval = 0.1f;

        for (int i = 0; i < randomValue; i++)
        {
            transform.Rotate(0, 0, 22.5f);
            if (i > Mathf.RoundToInt(randomValue * 0.5f))
            {
                timeInterval = 0.2f;
            }
            else if (i > Mathf.RoundToInt(randomValue * 0.85f))
            {
                timeInterval = 0.4f;
            }
            yield return new WaitForSeconds(timeInterval);
        }
        finalAngle = Mathf.RoundToInt(transform.eulerAngles.z);
        LoadScene();
        if (loadScene)
        {
            transform.Rotate(0, 0, 20);
        }
        wheelAudio.Pause();
        LoadScene();
        coroutineAllowed = true;
    }

    public void LoadScene()
    {
        if (finalAngle >= 6 && finalAngle <= 58)
        {
            StartCoroutine(SceneLoader(1));
        }
        else if (finalAngle >= 67 && finalAngle <= 116)
        {
            StartCoroutine(SceneLoader(2));
        }
        else if (finalAngle >= 123 && finalAngle <= 180)
        {
            StartCoroutine(SceneLoader(3));
        }
        else if (finalAngle >= 188 && finalAngle <= 237)
        {
            StartCoroutine(SceneLoader(4));
        }
        else if (finalAngle >= 245 && finalAngle <= 298)
        {
            StartCoroutine(SceneLoader(5));
        }
        else if (finalAngle >= 306 && finalAngle <= 358)
        {
            StartCoroutine(SceneLoader(6));
        }
        else
        {
            loadScene = true;
        }
    }

    IEnumerator SceneLoader(int sceneIndex)
    {
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
        SceneManager.LoadScene(sceneIndex);
    }

    public void Challange()
    {
        isChalange = true;
        Tap.Play();
        StartCoroutine(SceneLoader(10));
    }

    public void PlayButton()
    {
        Tap.Play();
        StartCoroutine(SceneLoader(10));
    }


}
