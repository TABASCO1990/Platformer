using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    [SerializeField] private Vector3[] _spawnPosition;
    [SerializeField] private Coin _coin;

    private void Start()
    {
        for (int i = 0; i < _spawnPosition.Length; i++)
        {
            Instantiate(_coin, _spawnPosition[i], Quaternion.identity);
        }       
    }
}
