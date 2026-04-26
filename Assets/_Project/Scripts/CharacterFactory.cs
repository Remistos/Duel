using UnityEngine;

public class CharacterFactory : MonoBehaviour
{
    [SerializeField] private GameObject magePrefab;
    [SerializeField] private GameObject archerPrefab;
    [SerializeField] private GameObject warriorPrefab;

    public Character CreateRandom(Transform point, bool mirror)
    {
        GameObject prefab = GetRandomPrefab();

        Character character = Instantiate(prefab, point.position, Quaternion.identity)
            .GetComponent<Character>();

        if (mirror)
            character.transform.rotation = Quaternion.Euler(0, 180, 0);

        return character;
    }

    private GameObject GetRandomPrefab()
    {
        int rnd = Random.Range(0, 3);

        if (rnd == 0) return magePrefab;
        if (rnd == 1) return archerPrefab;
        return warriorPrefab;
    }
}