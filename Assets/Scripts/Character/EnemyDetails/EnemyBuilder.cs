using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyBuilder: MonoBehaviour
{
    [SerializeField]
    private List<Character> _availableEnemies;

    public Character GenerateRandomEnemy()
    {
        return Instantiate(_availableEnemies[UnityEngine.Random.Range(0, _availableEnemies.Count)]);
    }
}

