
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : Photon.MonoBehaviour {

    public float maxHealth;
    public float currentHealth;
    public Image hpBar;

    public Movement playerPos;
    
    public void InitHealth(float hp)
    {
        currentHealth = hp;
        maxHealth = currentHealth;
    }

    public void UpdateHealth()
    {
        hpBar.fillAmount = currentHealth / maxHealth;
        photonView.RPC("UpdtHp", PhotonTargets.Others, currentHealth, maxHealth);
        
    }

    public void takeDamage(float dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {      
        playerPos.transform.position = playerPos.startPos;
        InitHealth(maxHealth);
    }

    [PunRPC]
    public void UpdtHp (float currentHealth,  float maxHealth)
    {
        hpBar.fillAmount = currentHealth / maxHealth;
    }

}
