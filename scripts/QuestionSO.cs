using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]

public class QuestionSO : ScriptableObject
{
    
    [TextArea(2,6)]
    [SerializeField] string question = "Enter New Question";
    [SerializeField] string[] Answers = new string[4];
    [SerializeField] int CorrectAnswerIndex;



    public string GetAnswer(int index)
    {
        return Answers[index];
    }
    public int GetAnswerIndex()
    {
        return CorrectAnswerIndex;
    }
    public string GetQuestion()
    {
        return question; ;
    }

}