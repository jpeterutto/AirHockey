using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public PuckController puck;                 
    public SpriteRenderer puckRenderer;        
    public Rigidbody2D puckRb;                  

    [Header("UI")]
    public TextMeshProUGUI scoreText;           

    [Header("Goal Pause")]
    public float goalPauseSeconds = 0.8f;

    private int bottomScore = 0; 
    private int topScore = 0;   

    void Awake()
    {
        if (puck != null)
        {
            if (puckRenderer == null) puckRenderer = puck.GetComponent<SpriteRenderer>();
            if (puckRb == null) puckRb = puck.GetComponent<Rigidbody2D>();
        }
        UpdateScoreUI();
    }

    public void GoalScoredByBottomPlayer()
    {
        bottomScore++;
        StartCoroutine(HandleGoalPauseAndReset());
    }

    public void GoalScoredByTopPlayer()
    {
        topScore++;
        StartCoroutine(HandleGoalPauseAndReset());
    }

    private IEnumerator HandleGoalPauseAndReset()
    {
        if (puckRb != null)
        {
            puckRb.linearVelocity = Vector2.zero;
            puckRb.angularVelocity = 0f;
        }

        if (puckRenderer != null)
            puckRenderer.enabled = false;

        UpdateScoreUI();

        yield return new WaitForSeconds(goalPauseSeconds);

        if (puckRenderer != null)
            puckRenderer.enabled = true;

        if (puck != null)
            puck.ResetPuck();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = $"{bottomScore}  -  {topScore}";
    }
}