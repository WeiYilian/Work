using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    //实例化血条的位置
    private GameObject barPoint;
    //判断血条是否长久可见
    public bool alwayVisible;
    //控制血条可视化时间
    public float visibleTime;
    //计时器
    private float timeLeft;
    
    //血条滑动条
    private Image healthSlider;
    //用来接创建出来的血条位置，方便与barPoint保持一致
    private GameObject UIbar;
    //拿到摄像机的位置，因为需要将血条面向摄像机
    private Camera cam;

    private CharacterStats currentStats;
    
    private void Awake()
    {
        currentStats = GetComponent<CharacterStats>();
        barPoint = transform.Find("HealthBarPoint").gameObject;
        currentStats.UpdateHealthBarOnAttack += UpdateHealthBar;
    }

    private void OnEnable()
    {
        cam = Camera.main;
        
        if (barPoint.GetComponent<Canvas>().renderMode == RenderMode.WorldSpace && barPoint.transform.childCount == 0)/*判断Canvas的模式是否是WorldSpace模式（世界模式）*/
        {
            UIbar = ObjectPool.Instance.Get("Health",barPoint.transform);
            UIbar.transform.position = barPoint.transform.position;
            healthSlider = UIbar.transform.GetChild(0).GetComponent<Image>();
            UIbar.gameObject.SetActive(alwayVisible);
        }
        
    }

    /// <summary>
    /// 更新血条,让血条随生命值变化
    /// </summary>
    /// <param name="currentHealth"></param>
    /// <param name="maxHealth"></param>
    private void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        if(currentHealth <= 0)
            ObjectPool.Instance.Remove("Health",UIbar);

        UIbar.gameObject.SetActive(true);
        timeLeft = visibleTime;

        float sliderPercent = currentHealth / maxHealth;
        healthSlider.fillAmount = sliderPercent;
    }

    /// <summary>
    /// 血条跟随怪物
    /// 使用LateUpdate目的是让怪物先移动，血条随后跟上
    /// </summary>
    private void LateUpdate()
    {
        if (UIbar != null)
        {
            UIbar.transform.position = barPoint.transform.position;
            UIbar.transform.forward = -cam.transform.forward;//血条面向摄像机

            if (timeLeft <= 0 && !alwayVisible)
                UIbar.gameObject.SetActive(false);
            else
                timeLeft -= Time.deltaTime;

        }
    }
}
