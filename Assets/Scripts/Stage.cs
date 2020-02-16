using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Stage
{
    List<Player> GetPlayers();
    List<Square> GetSquares();
    IEnumerator WaitForGettingReady();
    IEnumerator RollDice(int min, int max);
    IEnumerator MovePlayer(Player player, int dicenum);
    void MoveCamera(Vector3 pos);
}
