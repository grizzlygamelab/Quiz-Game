[System.Serializable]
public class QuestionData
{
    public string questionText;
    public string[] answers; // An array of 4 strings
    public int correctAnswerIndex; // 0, 1, 2, or 3
}