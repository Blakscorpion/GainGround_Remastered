using UnityEngine;
using TMPro;


public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public float TimeForThisLevel = 120;
    public int currentEnnemyNumber;
    public int NumberOfBabiesAllowed = 1;
    
    [SerializeField] TextMeshProUGUI UIEnnemyRemaining;
    [SerializeField] TextMeshProUGUI UITimeRemaining;


    void Awake()
    {
        Instance=this;
    }
    
    void Start()
    {
        currentEnnemyNumber = GameObject.FindGameObjectsWithTag("Ennemy").Length;
        UIEnnemyRemaining.text= currentEnnemyNumber.ToString();        
    }

    public void RemoveEnnemy() {
        currentEnnemyNumber--;
        UIEnnemyRemaining.text=currentEnnemyNumber.ToString();
        if (currentEnnemyNumber<=0)
        {
            GameManager.Instance.UpdateGameState(GameState.StageEnd);
        }
    }
}
