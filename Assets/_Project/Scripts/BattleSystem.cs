using UnityEngine;
using System.Collections;

public class BattleSystem
{
    public IEnumerator Fight(Character p1, Character p2, BattleUI ui)
    {
        while (p1.IsAlive() && p2.IsAlive())
        {
            yield return Turn(p1, p2, p1, p2, ui);
            if (!p2.IsAlive()) break;

            yield return new WaitForSeconds(1f);

            yield return Turn(p2, p1, p1, p2, ui);
            if (!p1.IsAlive()) break;

            yield return new WaitForSeconds(1f);
        }

        string winner = p1.IsAlive() ? p1.ClassName : p2.ClassName;
        ui.ShowWinner(winner);
    }

    private IEnumerator Turn(
        Character attacker,
        Character target,
        Character player1,
        Character player2,
        BattleUI ui)
    {
        attacker.ProcessEffects();

        if (!attacker.IsStunned)
            attacker.Attack(target);

        ui.UpdateUI(player1, player2);

        yield return null;
    }
}