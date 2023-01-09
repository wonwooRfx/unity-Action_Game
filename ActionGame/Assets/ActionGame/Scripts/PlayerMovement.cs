using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 클래스 선언 위에 작성하면 해당 컴포넌트가 없으면 안된다고 명시하는 역할
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    // 게임 오브젝트에 붙어있는 Animator 컴포넌트를 가져온다
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

    // 방향 컨트롤러에서 컨트롤러에 변경이 일어나면 호출되는 함수
    public void OnStickChanged(Vector2 stickPos)
    {
        h = stickPos.x;
        v = stickPos.y;
    }
   
    // Update is called once per frame
    void Update()
    {
        //아바타가 있어야만 실행
        if (avatar)
        {
            float back = 1f;
            
            if (v < 0f)
            {
                back = -1f;
            }

            // 애니메이터에 전달하는 값은 속도뿐
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
                    //캐릭터의 방향 전환은 즉시 이루어짐
                    //애니메이터에 전달되지 않고 자체적으로 해결
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
