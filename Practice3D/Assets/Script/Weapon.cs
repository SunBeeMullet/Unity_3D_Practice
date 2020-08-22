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
    public Transform bulletCasePos;
    public GameObject bulletCase;

    public void Use()
    {
        if(type == Type.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }else if (type == Type.Range)
        {
            StopCoroutine("Shot");
            StartCoroutine("Shot");
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

    IEnumerator Shot()
    {
        // 1 총알발사
        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;

        yield return null;
        // 2 탄피배출
        GameObject instantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
        Rigidbody caseRigid = instantCase.GetComponent<Rigidbody>();
        Vector3 caseVec = bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        caseRigid.AddForce(caseVec, ForceMode.Impulse);
        caseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
    }

    // Use() 메인루틴 -> Swing() 서브루틴 -> Use() 메인루틴
    // Use() 메인루틴 + Swing() 코루틴 (Co-routine)
}
