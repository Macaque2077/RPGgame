using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
#region singleton
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    private bool gamestate = GameModel.dummy;

    public GameObject scoreText;
    Text text;

    Person currentPlayer;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("loading save-------------");
        //PlayerManager.instance.loadPlayerSave(GameModel.currentPlayer);
        //PlayerStats.instance.LoadPlayer();

    }

    public void updateScore()
    {
        text = scoreText.GetComponent<Text>();
        text.text = "Score: " + currentPlayer.score;
    }

    public void setCurrentPlayer(Person player)
    {
        currentPlayer = player;

    }

    public void createCurrentPlayer()
    {
        PlayerManager.instance.PlayerSaveManager();
    }


}
