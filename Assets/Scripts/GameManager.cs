using UnityEngine;

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

    Person currentPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("loading save-------------");
        PlayerManager.instance.loadPlayerSave(GameModel.currentPlayer);
        
    }

    public void setCurrentPlayer(Person player)
    {
        currentPlayer = player;

    }

    public void createCurrentPlayer()
    {
        PlayerManager.instance.PlayerSaveManager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
