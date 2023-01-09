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
    bool isDead; // �÷��̾� �������
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
        //damaged �÷��׷� damaged�� true�� �� ȭ���� ���ϰ� ����� ����� �� �ѹ��� �����ϰ� �� �� �ְ� ��
        damaged = false;
    }

    public void Takedamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = (float)currentHealth/(float)startingHealth;

        //���� ü���� 0���ϰ� �Ǿ��ٸ� �׾��ٴ� �Լ��� ȣ��
        if (currentHealth <=0 && !isDead)
        {
            Death();
        }
        else
        {
            
            // ������ �ƴϸ�, �������� �Ծ��ٴ� Ʈ���� �ߵ�
            anim.SetTrigger("Damage");
            //damageImage.color = flashColor;
            
            //Color.Lerp(damageImage.color, Color.clear, flashSpeed);
        }
    }
   

    void Death()
    {
        isDead = true;
        anim.SetTrigger("Die");
        //�÷��̾��� �������� �����ϴ� PlayerMovement ��ũ��Ʈ ��Ȱ��ȭ
        playerMovement.enabled = false;
    }

}
