using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    [SerializeField]
    BattleState battleState;
    private TextMeshProUGUI _playerHP;
    private TextMeshProUGUI _enemyHP;
    // Start is called before the first frame update
    void Start()
    {
        //_playerHP = GetComponent<TextMeshProUGUI>();
        //_enemyHP = GetComponent<TextMeshProUGUI>();
        //SetPlayerHP(battleState.player.currentHp);
        //SetEnemyHP(battleState.enemies[0].currentHp);
        //TODO: Hardcoded first enemy HP
    }

    //public void SetPlayerHP(int hp)
    //{
    //    _playerHP.text = $"Player HP: ${hp}";
    //}

    //public void SetEnemyHP(int hp)
    //{
    //    _enemyHP.text = $"Enemy HP: ${hp}";
    //}
}
