using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    public Transform follow;
    public DamageableEntity damageableEntity;
    RectTransform rect;
    Slider slider;
    void Start()
    {
        rect = GetComponent<RectTransform>();
        slider = GetComponentInChildren<Slider>();
        if (slider) slider.maxValue = damageableEntity.MaxHP;
    }

    void Update()
    {
        if(follow)
        {
            rect.anchoredPosition = new Vector3(follow.localPosition.x, follow.localPosition.y + 2f, follow.localPosition.z);
        }
        if (slider)
        {
            slider.value = damageableEntity.HP;
        }
    }
}
