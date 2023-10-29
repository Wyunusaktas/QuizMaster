using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class   Timer : MonoBehaviour
{
    float timerValue;
    [SerializeField] float timeToCompleteQuestion;
    [SerializeField] float timeToShowCorrectAnswer;
   
    public bool isAnswerShowing = false;
    public float FillFraction;
    public bool loadNextQuestion=false;
    Quiz ti;

     void Start()
    {
        
    }
    void Update()
    {
        UpdateTimer();
    }
    public void CancelTimer()
    {
        isAnswerShowing = false;
        timerValue =timeToShowCorrectAnswer;

    }
    void UpdateTimer()
    {  
        timerValue -= Time.deltaTime;
       
        
            if (isAnswerShowing)

            {
                if (timerValue > 0)
                {
                    FillFraction = timerValue / timeToCompleteQuestion;


                }
                else if (timerValue <= 0)
                {
                    timerValue = timeToShowCorrectAnswer;
                    isAnswerShowing = false;
                }
            }
            else if (!isAnswerShowing)
            {
                if (timerValue <= 0)
                {
                    timerValue = timeToCompleteQuestion;
                    isAnswerShowing = true;
                    loadNextQuestion = true;
                }
                else if (timerValue > 0)
                {
                    FillFraction = timerValue / timeToShowCorrectAnswer;

                }
            }
        
        
    }
   
}