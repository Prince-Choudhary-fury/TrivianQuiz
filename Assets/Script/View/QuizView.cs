using UnityEngine;
using UnityEngine.EventSystems;

public class QuizView : MonoBehaviour
{

    public QuizController quizController;

    private int ButtonIndex;
    private string tagName;

    public AudioSource Tap;

    public void ButtonChecker()
    {
        Tap.Play();
        tagName = EventSystem.current.currentSelectedGameObject.tag;
        int.TryParse(tagName, out ButtonIndex);
        quizController.AnswerCheker(ButtonIndex);
    }

}
