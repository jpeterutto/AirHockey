using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public enum GoalOwner { Top, Bottom }

    public GoalOwner goalOwner;  
    public GameManager gameManager; 
    public string puckTag = "Puck"; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(puckTag)) return;

        if (goalOwner == GoalOwner.Top)
            gameManager.GoalScoredByBottomPlayer();
        else
            gameManager.GoalScoredByTopPlayer();
    }
}