using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolDown : MonoBehaviour
{
    public List<Skill> skills;


    void Update()
    {
        for(int i = 0; i < skills.Count; i++)
        {
            if(skills[i].currentCoolDown < skills[i].coolDown)
            {
                skills[i].currentCoolDown += Time.deltaTime;
                skills[i].icon.fillAmount = skills[i].currentCoolDown / skills[i].coolDown;
            }
        }
    }
}

[System.Serializable]
public class Skill
{
    public float coolDown;
    public Image icon;
    [HideInInspector]
    public float currentCoolDown = 1;
}
