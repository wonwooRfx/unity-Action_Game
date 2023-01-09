using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;
    public int startingHealth = 100;
    public int currentHealth;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    public AudioClip deathClip;

    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    bool isDead; // 플레이어 사망여부
    public bool damaged;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        currentHealth = startingHealth;
       
    }

    public void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        //damaged 플래그로 damaged가 true일 때 화면을 빩하게 만드는 명령을 딱 한번만 수행하게 할 수 있게 함
        damaged = false;
    }

    public void Takedamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = (float)currentHealth/(float)startingHealth;

        //현재 체력이 0이하가 되었다면 죽었다는 함수를 호출
        if (currentHealth <=0 && !isDead)
        {
            Death();
        }
        else
        {
            
            // 죽은게 아니면, 데미지를 입었다는 트리거 발동
            anim.SetTrigger("Damage");
            //damageImage.color = flashColor;
            
            //Color.Lerp(damageImage.color, Color.clear, flashSpeed);
        }
    }
   

    void Death()
    {
        isDead = true;
        anim.SetTrigger("Die");
        //플레이어의 움직임을 관리하는 PlayerMovement 스크립트 비활성화
        playerMovement.enabled = false;
    }

}
