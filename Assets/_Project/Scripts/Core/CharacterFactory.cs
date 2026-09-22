using Duels.Characters;
using UnityEngine;

namespace Duels
{
    public sealed class CharacterFactory : MonoBehaviour
    {
        [SerializeField] private Character[] characterPrefabs;

        [Header("Spawn Points")]
        [SerializeField] private Transform player1SpawnPoint;
        [SerializeField] private Transform player2SpawnPoint;

        public Character CreatePlayer(bool firstPlayer)
        {
            Character prefab = GetRandomPrefab();

            Transform spawnPoint = firstPlayer
                ? player1SpawnPoint
                : player2SpawnPoint;

            return Instantiate(
                prefab,
                spawnPoint.position,
                spawnPoint.rotation);
        }

        private Character GetRandomPrefab()
        {
            int index = Random.Range(0, characterPrefabs.Length);

            return characterPrefabs[index];
        }
    }
}