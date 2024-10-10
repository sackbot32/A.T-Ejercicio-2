using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health")]
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float currentHealth;
    private bool canTakeDamage;
    [Header("Blink Settings")]
    
    [SerializeField] private float blinkSpeed = 0.15f;
    [SerializeField] private Color blinkColor = Color.white;
    [SerializeField] private float blinkDuration;
    private SpriteRenderer sP;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        GameManager.instance.hudController.UpdateHealth(currentHealth);
        canTakeDamage = true;
        sP = GetComponentInChildren<SpriteRenderer>();
    }
    /// <summary>
    /// Method to make the player take damage, reducing its health
    /// </summary>
    public void TakeDamage(float damage)
    {
        if(canTakeDamage)
        {
            print("Damage taken");
            currentHealth -= damage;
            GameManager.instance.hudController.UpdateHealth(currentHealth);
            if(currentHealth <= 0)
            {
                GameManager.instance.EndState(false);
            } else
            {
                StartCoroutine(Blink(blinkDuration));
            }
        }
    }




    private IEnumerator Blink(float blinkDuration)
    {
        canTakeDamage = false;
        int howManyBlinks = Mathf.CeilToInt(blinkDuration/blinkSpeed);
        for(int i = 0; i < howManyBlinks; i++)
        {
            sP.color = new Color(1.5f, 1.5f, 1.5f, 0.15f);
            yield return new WaitForSeconds(blinkSpeed/2f);
            sP.color = new Color(1.5f, 1.5f, 1.5f, 0.9f);
            yield return new WaitForSeconds(blinkSpeed/2f);
        }
        sP.color = new Color(1, 1, 1, 1f);
        canTakeDamage = true;
    }

}
