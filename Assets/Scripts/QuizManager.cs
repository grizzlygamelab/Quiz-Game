using UnityEngine;
using UnityEngine.UI;
using System.Collections; // Needed for Coroutines
using TMPro; // Important: Use this if using TextMeshPro
using UnityEngine.SceneManagement; // <--- Needed for Scene Management
public class QuizManager : MonoBehaviour
{
    public QuestionData[] questions; // A list of questions you fill in the Inspector
    public TextMeshProUGUI questionDisplay; // Drag your Text object here
    public Button[] answerButtons; // Drag your 4 Buttons here
    
    private int currentQuestionIndex = 0;
    
    public TextMeshProUGUI scoreDisplay; // Drag your Score Text here
    private int currentScore = 0;
    
    public GameObject endScreenPanel; // End Panel Display
    public TextMeshProUGUI finalScoreText; //
    public TextMeshProUGUI questionNumberText;
    void Start()
    {
        DisplayQuestion();
        questionNumberText.text = "Question " + (currentQuestionIndex + 1);
    }

    void DisplayQuestion()
    {
        // Set the text for the question number
        questionNumberText.text = "Question " + (currentQuestionIndex + 1);
        
        // Set the text for the question
        questionDisplay.text = questions[currentQuestionIndex].questionText;

        // Set the text for each of the 4 buttons
        for (int i = 0; i < answerButtons.Length; i++)
        {
            // Finding the Text component inside the button
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = questions[currentQuestionIndex].answers[i];
            
            // Clear old listeners so they don't stack up
            answerButtons[i].onClick.RemoveAllListeners();

            // Add the logic for when this specific button is clicked
            int index = i; 
            answerButtons[i].onClick.AddListener(() => CheckAnswer(index));
        }
    }

    public void CheckAnswer(int selectedIndex)
    {
        // 1. Determine the color
        if (selectedIndex == questions[currentQuestionIndex].correctAnswerIndex)
        {
            answerButtons[selectedIndex].image.color = Color.green;
            currentScore++;
            scoreDisplay.text = "Score: " + currentScore;
        }
        else
        {
            answerButtons[selectedIndex].image.color = Color.red;
        }

        // 2. Start the "Wait" process
        StartCoroutine(NextQuestionRoutine());
    }

    IEnumerator NextQuestionRoutine()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(1.0f);

        // 3. Reset all button colors to White before showing the next question
        foreach (Button btn in answerButtons)
        {
            btn.image.color = Color.white;
        }

        currentQuestionIndex++;
    
        if (currentQuestionIndex < questions.Length)
        {
            DisplayQuestion();
        }
        else
        {
            endScreenPanel.SetActive(true);
            finalScoreText.text = "Score: " + currentScore;
        }
    }
    
    public void RestartGame()
    {
        // This line tells Unity to get the currently active scene and load it again
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}