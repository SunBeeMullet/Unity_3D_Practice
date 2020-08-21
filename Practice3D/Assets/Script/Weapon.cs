﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range };
    public Type type;
    public int damage;
    public float rate;
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;
    public Transform bulletPos;
    public GameObject bullet;

    public void Use()
    {
        if(type == Type.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
    }

    IEnumerator Swing()
    {
        // 1
        yield return new WaitForSeconds(0.1f); // 0.1 초 대기
        meleeArea.enabled = true;
        trailEffect.enabled = true;
        // 2
        yield return new WaitForSeconds(0.3f); // 0.3 초 대기
        meleeArea.enabled = false;
        // 3
        yield return new WaitForSeconds(0.3f); // 0.3 초 대기
        trailEffect.enabled = false;
        //yield return null; // 1 프레임 대기
        yield break;
        // 아래 비활성화
    }

    // Use() 메인루틴 -> Swing() 서브루틴 -> Use() 메인루틴
    // Use() 메인루틴 + Swing() 코루틴 (Co-routine)
}
