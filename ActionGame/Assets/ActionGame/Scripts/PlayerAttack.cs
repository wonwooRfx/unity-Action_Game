using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int NormalDamage = 10;
    public int SkillDamage = 30;
    public int DashDamage = 30;

    //ĳ���� ���ݰ���
    //Ÿ���� Ʈ���ŷ� � ���Ͱ� ���� �ݰ� �ȿ� ���Դ��� �����ϱ� ���� ��ũ��Ʈ ������Ʈ ����
    public NormalTarget normalTarget;
    public SkillTarget skillTarget;

    public void NormalAttack()
    {
        // normaltarget�� �پ��ִ� Ʈ���� �ݶ��̴��� ����ִ� ���� ����Ʈ�� ��ȸ
        // ���� ����Ʈ ���� �����ϰ� ���⿡ ����ִ� ����Ʈ�� �ٽ� ����
        List<Collider> tList = new List<Collider>(normalTarget.targetList);

        foreach(Collider one in tList)
        {
            EnemyHealth enemy = one.GetComponent<EnemyHealth>();
            if(enemy != null)
            {
                // ���Ϳ��� �������� �󸶳� ����, �󸶳� �ڷ� �з����� ����
                StartCoroutine(enemy.StartDamage(NormalDamage, transform.position, 0.5f, 0.5f));
            }
        }
    }

    public void DashAttack()
    {
        List<Collider> tList = new List<Collider>(skillTarget.targetList);

        foreach (Collider one in tList)
        {
            // ���ʸ��ｺ ��ũ��Ʈ�� �����´�
            EnemyHealth enemy = one.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                // ���Ϳ��� �������� �󸶳� ����, �󸶳� �ڷ� �з����� ����
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
                // ���Ϳ��� �������� �󸶳� ����, �󸶳� �ڷ� �з����� ����
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
