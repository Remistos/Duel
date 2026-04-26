using UnityEngine;

public class GameEntryPoint : MonoBehaviour
{
    [SerializeField] private CharacterFactory factory;
    [SerializeField] private Transform spawn1;
    [SerializeField] private Transform spawn2;
    [SerializeField] private BattleUI ui;

    private void Start()
    {
        Character player1 = factory.CreateRandom(spawn1, true);
        Character player2 = factory.CreateRandom(spawn2, false);

        BattleSystem battle = new BattleSystem();

        StartCoroutine(battle.Fight(player1, player2, ui));
    }
}