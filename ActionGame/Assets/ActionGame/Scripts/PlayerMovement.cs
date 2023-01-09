using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ŭ���� ���� ���� �ۼ��ϸ� �ش� ������Ʈ�� ������ �ȵȴٰ� ����ϴ� ����
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    // ���� ������Ʈ�� �پ��ִ� Animator ������Ʈ�� �����´�
    protected Animator avatar;
    float lastAttackTime, lastSkillTime, lastDashTime;
    public bool attacking = false;
    public bool dashing = false;

    protected PlayerAttack playerAtk;
   
    // Start is called before the first frame update
    void Start()
    {
        avatar = GetComponent<Animator>();
        playerAtk = GetComponent<PlayerAttack>();
    }

    float h, v;

    // ���� ��Ʈ�ѷ����� ��Ʈ�ѷ��� ������ �Ͼ�� ȣ��Ǵ� �Լ�
    public void OnStickChanged(Vector2 stickPos)
    {
        h = stickPos.x;
        v = stickPos.y;
    }
   
    // Update is called once per frame
    void Update()
    {
        //�ƹ�Ÿ�� �־�߸� ����
        if (avatar)
        {
            float back = 1f;
            
            if (v < 0f)
            {
                back = -1f;
            }

            // �ִϸ����Ϳ� �����ϴ� ���� �ӵ���
            avatar.SetFloat("Speed", (h * h + v * v));
            Rigidbody rigidbody = GetComponent<Rigidbody>();

            if (rigidbody)
            {
                Vector3 speed = rigidbody.velocity;
                speed.x = 4 * h;
                speed.z = 4 * v;

                rigidbody.velocity = speed;
                if(h != 0f && v != 0f)
                {
                    //ĳ������ ���� ��ȯ�� ��� �̷����
                    //�ִϸ����Ϳ� ���޵��� �ʰ� ��ü������ �ذ�
                    transform.rotation = Quaternion.LookRotation(new Vector3(h, 0f, v));
                }
            }
        }

    }

    public void OnAttackdown()
    {
        attacking = true;
        avatar.SetBool("Combo", true);
        StartCoroutine(StartAttack());
        
       
    }

    public void OnAttackUp()
    {
        avatar.SetBool("Combo", false);
        attacking = false;
    }

    IEnumerator StartAttack()
    {
        if(Time.time - lastAttackTime > 1f)
        {
            lastAttackTime = Time.time;

            while (attacking)
            {
                avatar.SetTrigger("AttackStart");

                playerAtk.NormalAttack();

                yield return new WaitForSeconds(1f);
            }
        }
    }

    public void OnSkillDown()
    {
        if(Time.time - lastSkillTime > 1f)
        {
            avatar.SetBool("Skill", true);
            lastSkillTime = Time.time;
            playerAtk.SkillAttackt();
        }
       
    }

    public void OnSkillUp()
    {
        avatar.SetBool("Skill", false);
    }

    public void OnDashDown()
    {
        if(Time.time - lastDashTime > 1f)
        {
            lastDashTime = Time.time;
           // dashing = true;
            avatar.SetTrigger("Dash");
            playerAtk.DashAttack();
        }
        
    }

   /* public void OnDashUp()
    {
        dashing = false;
    }*/
}
