using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TMP_Text maneyText;
    public TMP_Text ReputationText;
    private int maney = 0;
    private int reputation = 0;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void AddScore(int newManey)
    {
        maney += newManey;
        maneyText.text = "Maney : " + maney;
    }

    public void AddReputation(int newReputation)
    {
        reputation += newReputation;
        if(reputation < 0)
        {
            reputation = 0;
        }
        ReputationText.text = "Reputation : " + reputation;
    }

}
