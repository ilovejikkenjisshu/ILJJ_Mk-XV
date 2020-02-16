using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Stage
{
    Player[] GetPlayers();
    Square[] GetSquares();
    IEnumerator WaitForGettingReady();
    IEnumerator RollDice(int min, int max);
    IEnumerator MovePlayer(Player player, int dicenum);
}
