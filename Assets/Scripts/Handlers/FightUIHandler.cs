using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class FightUIHandler : MonoBehaviour
{
    public GameObject fightPanel;
    public GameObject raidingPanel;
    //public GameObject closeUIButton;

    public Image playerIcon, enemyIcon;
    public TMP_Text playerHealthText, playerDamageText, playerDefenseText, enemyHealthText, enemyDamageText, enemyDefenseText;
    public TMP_Text playerName, enemyName, attackResultText, raidTimerText;

    public static FightUIHandler Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        var fightController = FightController.Instance;
        var currentEnemy = fightController.currentEnemy;

        bool isFighting = currentEnemy != null && currentEnemy.health > 0 && PlayerStats.Instance.health > 0;

        if (isFighting)
        {
            ShowFightPanel();

            playerIcon.sprite = PlayerStats.Instance.playerIcon;
            enemyIcon.sprite = currentEnemy.enemyIcon;

            playerHealthText.text = "Health: " + PlayerStats.Instance.health.ToString();
            playerDamageText.text = "Damage: " + PlayerStats.Instance.damage.ToString();
            playerDefenseText.text = "Defense: " + PlayerStats.Instance.defense.ToString();

            enemyHealthText.text = "Health: " + currentEnemy.health.ToString();
            enemyDamageText.text = "Damage: " + currentEnemy.damage.ToString();
            enemyDefenseText.text = "Defense: " + currentEnemy.defense.ToString();
            enemyName.text = currentEnemy.enemyName;

            attackResultText.text = fightController.attackResult;
        }
    }

    public void ShowFightPanel()
    {
        StopAllCoroutines(); // Stop any ongoing delayed switch
        fightPanel.SetActive(true);
        raidingPanel.SetActive(false);
    }

    public void ShowRaidingPanelDelayed(float delay)
    {
        StartCoroutine(SwitchToRaidingAfterDelay(delay));
    }

    private IEnumerator SwitchToRaidingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        fightPanel.SetActive(false);
        raidingPanel.SetActive(true);
    }

    public void UpdateRaidTimer(float time)
    {
        raidTimerText.text = $"Time in Raid: {time:F1}s...";
    }

    public void ResetPanels()
    {
        StopAllCoroutines();
        fightPanel.SetActive(false);
        raidingPanel.SetActive(true);
    }

    public void ForceUpdateUI()
    {
        var fightController = FightController.Instance;
        var currentEnemy = fightController.currentEnemy;

        playerIcon.sprite = PlayerStats.Instance.playerIcon;
        enemyIcon.sprite = currentEnemy.enemyIcon;

        playerHealthText.text = "Health: " + PlayerStats.Instance.health.ToString();
        playerDamageText.text = "Damage: " + PlayerStats.Instance.damage.ToString();
        playerDefenseText.text = "Defense: " + PlayerStats.Instance.defense.ToString();

        enemyHealthText.text = "Health: " + currentEnemy.health.ToString();
        enemyDamageText.text = "Damage: " + currentEnemy.damage.ToString();
        enemyDefenseText.text = "Defense: " + currentEnemy.defense.ToString();
        enemyName.text = currentEnemy.enemyName;

        attackResultText.text = fightController.attackResult;
    }
}
