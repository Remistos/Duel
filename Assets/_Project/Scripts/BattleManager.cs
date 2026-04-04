using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
public class BattleManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject magePrefab;
    [SerializeField] private GameObject archerPrefab;
    [SerializeField] private GameObject warriorPrefab;

    [Header("Spawn Points")]
    [SerializeField] private Transform spawnPoint1;
    [SerializeField] private Transform spawnPoint2;

    [SerializeField] private UIBattle ui;

    private Character player1;
    private Character player2;

    private readonly WaitForSeconds turnDelay = new WaitForSeconds(1f);

    private void Start()
    {
        CreateFighters();
        StartCoroutine(BattleLoop());
    }

    public void RestartBattle()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    private void CreateFighters()
    {
        player1 = InstantiateRandomClass(true, spawnPoint1);
        player2 = InstantiateRandomClass(false, spawnPoint2);
    }

    private Character InstantiateRandomClass(bool mirror, Transform spawnPoint)
    {
        int randomIndex = Random.Range(0, 3);

        GameObject prefab =
            randomIndex == 0 ? magePrefab :
            randomIndex == 1 ? archerPrefab :
            warriorPrefab;

        Character character = Instantiate(prefab, spawnPoint.position, Quaternion.identity)
            .GetComponent<Character>();

        if (mirror)
            character.transform.rotation = Quaternion.Euler(0, 180, 0);

        return character;
    }

    private IEnumerator BattleLoop()
    {
        while (player1.IsAlive() && player2.IsAlive())
        {
            yield return ProcessTurn(player1, player2);
            if (!player2.IsAlive()) break;

            yield return turnDelay;

            yield return ProcessTurn(player2, player1);
            if (!player1.IsAlive()) break;

            yield return turnDelay;
        }

        string winner = player1.IsAlive() ? player1.ClassName : player2.ClassName;
        ui.ShowWinScreen(winner);
    }

    private IEnumerator ProcessTurn(Character attacker, Character target)
    {
        attacker.ProcessEffects();

        if (!attacker.IsStunned)
        {
            attacker.Attack(target);
        }

        ui.UpdateUI(player1, player2);

        yield return null;
    }
}