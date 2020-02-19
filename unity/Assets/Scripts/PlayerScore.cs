using UnityEngine;
using System.Collections.Generic;

public class PlayerScore : MonoBehaviour {

    [SerializeField]
    private int score = 0;

    public void IncreaseScore(int x) {
        score += x;
        //TODO: Upload to DB.
    }

    public int GetPlayerScore() {
        return score;
    }

    public void Start() {
       
    }
}