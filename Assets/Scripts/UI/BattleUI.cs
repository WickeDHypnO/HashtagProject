using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [SerializeField]
    BattleState battleState;
    [SerializeField]
    BattleSkillbar battleSkillbar;
    public Slider _playerHP;
    public Slider _playerArmor;
    //private TextMeshProUGUI _enemyHP;
    // Start is called before the first frame update
    void Start()
    {
        //_playerHP = GetComponent<TextMeshProUGUI>();
        //_enemyHP = GetComponent<TextMeshProUGUI>();
        //SetPlayerHP(battleState.player.currentHp);
        //SetEnemyHP(battleState.enemies[0].currentHp);
        //TODO: Hardcoded first enemy HP
    }

    public void SetPlayerHP(int currentHp, int maxHp)
    {
        _playerHP.maxValue = maxHp;
        _playerHP.value = currentHp;
        _playerHP.GetComponentInChildren<TextMeshProUGUI>().text = $"{currentHp}/{maxHp}";
    }

    public void SetPlayerArmor(int currentArmor, int maxArmor)
    {
        _playerArmor.maxValue = maxArmor;
        _playerArmor.value = currentArmor;
        _playerArmor.GetComponentInChildren<TextMeshProUGUI>().text = $"{currentArmor}/{maxArmor}";
    }

    //public void SetEnemyHP(int hp)
    //{
    //    _enemyHP.text = $"Enemy HP: ${hp}";
    //}
}
