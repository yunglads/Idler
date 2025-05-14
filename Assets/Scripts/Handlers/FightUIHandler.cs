using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class FightUI : MonoBehaviour
{
    public Image playerIcon, enemyIcon;
    public TMP_Text playerHealthText, playerDamageText, playerDefenseText, enemyHealthText, enemyDamageText, enemyDefenseText;
    public TMP_Text attackResultText;

    // Update is called once per frame
    void Update()
    {
        playerIcon.sprite = PlayerStats.Instance.playerIcon;
        enemyIcon.sprite = FightController.Instance.currentEnemy.enemyIcon;

        playerHealthText.text = "Health: " + PlayerStats.Instance.health.ToString();
        playerDamageText.text = "Damage: " + PlayerStats.Instance.damage.ToString();
        playerDefenseText.text = "Defense: " + PlayerStats.Instance.defense.ToString();

        enemyHealthText.text = "Health: " + FightController.Instance.currentEnemy.health.ToString();
        enemyDamageText.text = "Damage: " + FightController.Instance.currentEnemy.damage.ToString();
        enemyDefenseText.text = "Defense: " + FightController.Instance.currentEnemy.defense.ToString();

        attackResultText.text = FightController.Instance.attackResult;
    }
}
