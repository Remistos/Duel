using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
public class BattleManager : MonoBehaviour
{
    public GameObject MagePrefab;
    public GameObject ArcherPrefab;
    public GameObject WarriorPrefab;
    private Character player1;
    private Character player2;
    public Transform SpawnPoint1;
    public Transform SpawnPoint2;
    public UIBattle ui;
    void Start()
    {
        CreateFighters();
        StartCoroutine(BattleLoop());
    }
    public void RestartBattle()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
        UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    void CreateFighters()
    {
        Debug.Log("Spawn1: " + SpawnPoint1);
        Debug.Log("MagePrefab: " + MagePrefab);
        Debug.Log("Player1: " + player1);
        player1 = InstantiateRandomClass(true, SpawnPoint1);
        player2 = InstantiateRandomClass(false, SpawnPoint2);
        Debug.Log("Игрок 1: " + player1.ClassName);
        Debug.Log("Игрок 2: " + player2.ClassName);
    }
    Character InstantiateRandomClass(bool mirror, Transform spawnPoint)
    {
        int rnd = Random.Range(0, 3);
        GameObject prefab =
            rnd == 0 ? MagePrefab :
            rnd == 1 ? ArcherPrefab :
            WarriorPrefab;
        GameObject obj = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        if (mirror)
            obj.transform.rotation = Quaternion.Euler(0, 180, 0);

        return obj.GetComponent<Character>();
    }
    System.Collections.IEnumerator BattleLoop()
    {
        while (player1.Health > 0 && player2.Health > 0)
        {
            player1.ProcessEffects();
            if (player1.IsStunned)
            {
                player1.stunDuration--;
                if (player1.stunDuration <= 0)
                    player1.IsStunned = false;
            }
            else
            {
                player1.Attack(player2);
            }

            ui.UpdateUI(player1, player2);
            if (player2.Health <= 0) break;
            yield return new WaitForSeconds(1f);
            player2.ProcessEffects();
            if (player2.IsStunned)
            {
                player2.stunDuration--;
                if (player2.stunDuration <= 0)
                    player2.IsStunned = false;
            }
            else
            {
                player2.Attack(player1);
            }

            ui.UpdateUI(player1, player2);
            if (player1.Health <= 0) break;
            yield return new WaitForSeconds(1f);
        }
        string winner = player1.Health > 0 ? player1.ClassName : player2.ClassName;
        ui.ShowWinScreen(winner);
    }

}