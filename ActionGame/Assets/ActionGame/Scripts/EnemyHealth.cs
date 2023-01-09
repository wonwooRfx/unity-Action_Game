using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;

    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    public float sinkSpeed = 1f;

    bool isDead;
    bool isSinking;
    bool damaged;

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
        else
        {
            
        }
    }

    void Death()
    {
        isDead = true;
        transform.GetChild(0).GetComponent<BoxCollider>().isTrigger = true;
        StartSinking();
    }

    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        Destroy(gameObject, 2f);
    }

    public IEnumerator StartDamage(int damage, Vector3 playerPosition, float delay, float pushBack)
    {
        yield return new WaitForSeconds(delay);

        try
        {
            Vector3 diff = playerPosition - transform.position;
            diff = diff / diff.sqrMagnitude; // 두 점간의 거리의 제곱에 루트를 한 값, Vector 3 dir = diff.nomalized;
            GetComponent<Rigidbody>().AddForce((transform.position - new Vector3(diff.x, diff.y, 0f)) * 50f * pushBack);
        }
        catch(MissingComponentException e)
        {
            Debug.Log(e.ToString());
        }
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            //아웃라인컬러 메테리얼에 있는 속성값 변경
            transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_OutlineColor", flashColor);
        }
        else
        {
            transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.Lerp(transform.GetChild(0).GetComponent<Renderer>().
                material.GetColor("_OutlineColor"), Color.black, flashSpeed * Time.deltaTime));
        }
        damaged = false;

        if (isSinking)
        {
            transform.Translate(Vector3.down * sinkSpeed * Time.deltaTime);
        }
    }
}
