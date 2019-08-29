using UnityEngine;

//to keep track of player for enemy ai
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    #region Singleton
    private void Awake()
    {
        instance = this;
    }

    #endregion

    //public gameobject to drag the player into 
    public GameObject player;
}
