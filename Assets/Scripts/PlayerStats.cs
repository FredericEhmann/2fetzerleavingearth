using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    private double health;

    [SerializeField] private Animator anim;
    [SerializeField] private Image healthFill;
    [SerializeField] private GameObject explosionPrefab;
    private TextMeshProUGUI healthTextComponent;
    private TextMeshProUGUI shieldTextComponent;
    [SerializeField] private Shield shield;
    public bool canTakeDmg = true;
    public static Dictionary<string,PlayerStats> instance = new Dictionary<string, PlayerStats>();
    private bool canPlayAnim = true;
    [SerializeField] private String playerName="Player";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        instance.Add(playerName,this);
        SetScale();
        Constants.setPlayer(gameObject);
        double maxHealth = Constants.getMaxHealth();
        health = maxHealth;
        if (healthFill != null)
        {
            healthFill.fillAmount = Convert.ToSingle(health / maxHealth);
        }
        EndGameManager.getInstance().gameOver = false;
        StartCoroutine(DamageProtection());
    }

    private void OnDisable()
    {
        instance.Remove(playerName);
    }

    private void OnDestroy()
    {
        instance.Remove(playerName);
    }
    public static PlayerStats GetInstance()
    {
        return GetInstance("Player");
    }

    public static PlayerStats GetInstance(string s)
    {
        if (instance.TryGetValue(s, out PlayerStats ps))
        {
            return ps;
        }
        return null;
    }

    public void SetScale()
    {
        float f = Constants.getUpgradeDecay(0.4f, 0.0001f, 0.002f);
        if ("Mom".Equals(playerName))
        {
            f = 0.6f;
        }
        if ("Dad".Equals(playerName))
        {
            f = 0.7f;
        }
        transform.localScale = new Vector3(f, f, f);
    }

    private void Start() {
        if ("Player".Equals(playerName))
        {
            EndGameManager.getInstance().RegisterPlayerStats(this);
            EndGameManager.getInstance().possibleWin = false;
        }
    }

    IEnumerator DamageProtection() {
        canTakeDmg = false;
        yield return new WaitForSeconds(1.5f);
        canTakeDmg = true;
    }

    public void AddHealth(float healAmount)
    {
        Constants.addMaxHealth(healAmount/10);
        Constants.addHealingBonus(0.002f);
        health += healAmount/10;
        if (healthFill != null)
        {
            healthFill.fillAmount = Convert.ToSingle(health / Constants.getMaxHealth());
        }
        healthTextComponent.text = health + "/" + Constants.getMaxHealth();
    }

    public virtual void PlayerTakeDamage(float damage, Transform WhoKilled)
    {
        if (!canTakeDmg) {
            return;
        }
        if (!EndGameManager.getInstance().gameOver) {
            if (shield!=null&&shield.protection) {
                if (shield.enabled && shieldTextComponent != null)
                {
                    shieldTextComponent.text = shield.getHitsToDestroy() + "";
                }
                return;
            }
            health -= damage;
            if (healthFill != null)
            {
                healthFill.fillAmount = Convert.ToSingle(health / Constants.getMaxHealth());
            }
            if (canPlayAnim)
            {
                anim.SetTrigger("DamageTrigger");
                StartCoroutine(AntiSpamAnimation(0.3f));
            }
            if (health <= 0)
            {
                health = 0;
                ZoomIn.GetInstance().Explosion=Instantiate(explosionPrefab, transform.position, transform.rotation);
                ZoomIn.GetInstance().Explosion.GetComponent<AudioSource>().volume = 3f;
                ZoomIn.GetInstance().Explosion.GetComponent<AudioSource>().pitch = 0.09f;
                ZoomIn.GetInstance().WhoKilled = WhoKilled;
                Time.timeScale = Constants.deathSpeed();
                ZoomIn.GetInstance().Show(gameObject.transform.position);
                //Destroy(gameObject);
                healthTextComponent.text = "0";
                EndGameManager.getInstance().SetGameOver(true);
                gameObject.SetActive(false);
            }
        }
        healthTextComponent.text = (int)health + "." + (int)(health * 10) % 10;
    }

    private void Update()
    {
        if (EndGameManager.getInstance().isGameOver()) {
            Color c = GetComponent<SpriteRenderer>().color;
            if (c.a > 0.4f) {
                c.a = 0.4f;
            }
            if (c.a < 0.01f)
            {
            }
            c.a -= 0.05f;
            GetComponent<SpriteRenderer>().color = c;
        }
        if (shieldTextComponent != null && shield != null)
        {
            shieldTextComponent.text = shield.getHitsToDestroy() + "";
        }
        if (health > 0) { 
            health += (Time.deltaTime / 10) * Constants.getHealingBonus();
        }
        else
        {
            health = 0;
        }
        if (healthFill != null)
        {
            healthFill.fillAmount = Convert.ToSingle(health / Constants.getMaxHealth());
        }
        healthTextComponent.text = (int)health+"."+(int)(health*10)%10;
    }

    public void RegisterHealthText(TextMeshProUGUI healthTextComponent)
    {

        this.healthTextComponent = healthTextComponent;
    }
    public void RegisterShieldText(TextMeshProUGUI shieldTextComponent)
    {

        this.shieldTextComponent = shieldTextComponent;
    }


    private IEnumerator AntiSpamAnimation(float time)
    {
        canPlayAnim = false;
        yield return new WaitForSeconds(time);
        canPlayAnim = true;
    }


    private IEnumerator WaitFori(float time)
    {
        yield return new WaitForSeconds(time);
    }

    
}
