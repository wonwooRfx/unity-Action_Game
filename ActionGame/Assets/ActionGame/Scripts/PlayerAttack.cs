using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int NormalDamage = 10;
    public int SkillDamage = 30;
    public int DashDamage = 30;

    //캐릭터 공격가능
    //타겟의 트리거로 어떤 몬스터가 공격 반경 안에 들어왔는지 판정하기 위해 스크립트 컴포넌트 접근
    public NormalTarget normalTarget;
    public SkillTarget skillTarget;

    public void NormalAttack()
    {
        // normaltarget에 붙어있는 트리거 콜라이더에 들어있는 몬스터 리스트를 조회
        // 새로 리스트 변수 선언하고 여기에 들어있는 리스트를 다시 담음
        List<Collider> tList = new List<Collider>(normalTarget.targetList);

        foreach(Collider one in tList)
        {
            EnemyHealth enemy = one.GetComponent<EnemyHealth>();
            if(enemy != null)
            {
                // 몬스터에게 데미지를 얼마나 줄지, 얼마나 뒤로 밀려나게 할지
                StartCoroutine(enemy.StartDamage(NormalDamage, transform.position, 0.5f, 0.5f));
            }
        }
    }

    public void DashAttack()
    {
        List<Collider> tList = new List<Collider>(skillTarget.targetList);

        foreach (Collider one in tList)
        {
            // 에너마헬스 스크립트를 가져온다
            EnemyHealth enemy = one.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                // 몬스터에게 데미지를 얼마나 줄지, 얼마나 뒤로 밀려나게 할지
                //IEnumerator StartDamage(int damage, Vector3 playerPosition, float delay, float pushBack)
                StartCoroutine(enemy.StartDamage(DashDamage, transform.position, 1f, 2f));
            }
        }
    }

    public void SkillAttackt()
    {
        List<Collider> tList = new List<Collider>(skillTarget.targetList);

        foreach (Collider one in tList)
        {
            EnemyHealth enemy = one.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                // 몬스터에게 데미지를 얼마나 줄지, 얼마나 뒤로 밀려나게 할지
                StartCoroutine(enemy.StartDamage(SkillDamage, transform.position, 1f, 2f));
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
