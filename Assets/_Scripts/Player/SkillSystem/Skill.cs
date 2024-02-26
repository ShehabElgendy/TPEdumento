using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField]
    protected float coolDownTime;

    [SerializeField] 
    protected float coolDownCounter;

    protected virtual void Update()
    {
        coolDownCounter -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        if (coolDownCounter < 0)
        {
            UseSkill();
            coolDownCounter = coolDownTime;
            return true;
        }

        return false;
    }

    protected virtual void UseSkill()
    {
        //do some skill specific function
    }
}
