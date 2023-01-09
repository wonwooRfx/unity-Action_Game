using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTarget : MonoBehaviour
{
    // 공격 대상에 있는 적들의 리스트
    public List<Collider> targetList;

    private void Awake()
    {
        targetList = new List<Collider>();
    }

    // 적 개체가 공격 반경을 벗어나면, 해당 개체를 추가
    private void OnTriggerEnter(Collider other)
    {
        targetList.Add(other);
    }
    private void OnTriggerExit(Collider other)
    {
        targetList.Remove(other);
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
