using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
   [SerializeField] private Slider healthBar;
   [SerializeField] private TextMeshProUGUI score;
   [SerializeField] private Image damageEfect;
   [SerializeField] private float damageTime;

   private Coroutine disappearCoroutine;
   public static HUDController instance;

    private void Awake()
    {
        instance = this;
        healthBar.maxValue = GameObject.Find("Player").GetComponent<PlayerController>().MaxLives;
        healthBar.value = GameObject.Find("Player").GetComponent<PlayerController>().MaxLives;
    }

    public void UpdateHealthBar(int currentHealth,bool damage)
    { 
        healthBar.value = currentHealth;
        if (damage)
        {
            ShowDamageFlash();
        }
       
    }

    public void ShowDamageFlash()
    {
        if(disappearCoroutine != null)
        {
            StopCoroutine(disappearCoroutine);
        }
        damageEfect.color = Color.white;
        disappearCoroutine = StartCoroutine(DamageDisappear());
    }

    private IEnumerator DamageDisappear()
    {
        float alpha = 1.0f;
        while (alpha >0.0f)
        {
            alpha -= (1.0f/damageTime) * Time.deltaTime;
            damageEfect.color=new Color(1.0f,1.0f,1.0f, alpha); 
            yield return null;
        }
    }

    public void UpdateScoreHUD(int scorePoints)
    {
        score.GetComponent<TMP_Text>().text = "Score: "+scorePoints.ToString("00000");
    }
}
