using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2,6)]
    [SerializeField] string question = "Enter new question text here";
    [SerializeField] string[] answer = new string[4];
    [SerializeField] int CorrectAnswerIndex;

    public string GetQuestion()
    {
        return question;
    }
    public int GetCorrectAnswerIndex()
    {
         return CorrectAnswerIndex;
    }
    public string GetAnswer(int index)
    {
        return answer[index];
    }
    
}
