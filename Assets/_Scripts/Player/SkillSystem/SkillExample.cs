using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillExample : Skill
{
    protected override void UseSkill()
    {
        base.UseSkill();
        Debug.Log("Used Example Skill");
    }
}
