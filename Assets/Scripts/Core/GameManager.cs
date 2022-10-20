using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Player player;
    public Player Player => player; // player 읽기전용 프로퍼티

    protected override void Initialize()
    {
        player = FindObjectOfType<Player>();
    }
}
