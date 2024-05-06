using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    [Header("Abilities 1")]
    public Image ability1Image;
    public float cooldown1 = 5;
    bool isCooldown1 = false;
    public KeyCode ability1;
    public GameObject skill1Effect;

    [Header("Abilities 2")]
    public Image ability2Image;
    public float cooldown2 = 5;
    bool isCooldown2 = false;
    public KeyCode ability2;
    public GameObject skill2Effect;
    [SerializeField] public TMP_Text lockSkill2;

    [Header("Abilities 3")]
    public Image ability3Image;
    public float cooldown3 = 5;
    bool isCooldown3 = false;
    public KeyCode ability3;
    public GameObject skill3Effect;
    [SerializeField] public TMP_Text lockSkill3;

    [Header("Abilities 4")]
    public Image ability4Image;
    public float cooldown4 = 5;
    bool isCooldown4 = false;
    public KeyCode ability4;
    public GameObject skill4Effect;
    [SerializeField] public TMP_Text lockSkill4;

    [SerializeField] protected PlayerController player;
    [SerializeField] protected Player playerStats;
    private bool ability1KeyPressed = false;
    private bool ability2KeyPressed = false;
    private bool ability3KeyPressed = false;
    public GameObject warningCircle;
    public bool isSkill1 = false;
    public bool isSkill2 = false;
    public bool isSkill3 = false;
    public bool isSkill4 = false;
    public Vector2 mousePosition;

    private void Start()
    {
        ability1Image.fillAmount = 0;
        ability2Image.fillAmount = 0;
        ability3Image.fillAmount = 0;
        ability4Image.fillAmount = 0;
        warningCircle.SetActive(false);
        skill1Effect.SetActive(false);
        skill2Effect.SetActive(false);
        skill3Effect.SetActive(false);
        skill4Effect.SetActive(false);
    }

    public void HandleUpdate()
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (ability1KeyPressed || ability2KeyPressed || ability3KeyPressed)
        {
            warningCircle.transform.position = new Vector3(mouse.x, mouse.y, 0);
        }
        if (isSkill1)
        {
            Skill1(mousePosition);
        }
        if (isSkill2)
        {
            Skill2(mousePosition);
        }
        if (isSkill4)
        {
            Skill4();
        }

        Abilities1();
        if (player.currentLevel >= 10)
        {
            lockSkill2.gameObject.SetActive(false);
            Abilities2();
        }
        if (player.currentLevel >= 20)
        {
            lockSkill3.gameObject.SetActive(false);
            Abilities3();
        }
        if (player.currentLevel >= 40)
        {
            lockSkill4.gameObject.SetActive(false);
            Abilities4();
        }
    }


    private void Abilities1()
    {
        if (Input.GetKeyDown(ability1) && !isCooldown1)
        {
            player.isSkill = true;
            ability1KeyPressed = true;
            ability2KeyPressed = false;
            ability3KeyPressed = false;
            warningCircle.SetActive(true);
            warningCircle.transform.localScale = new Vector3(1f, 1f, 0f);
        }
        if (Input.GetMouseButtonDown(0) && ability1KeyPressed && !isCooldown1 && !ability2KeyPressed && !ability3KeyPressed)
        {
            skill1Effect.SetActive(true);
            skill1Effect.GetComponent<AudioSource>().Play();
            if (isSkill4) skill1Effect.GetComponent<Animator>().SetBool("isSuper", true);
            else skill1Effect.GetComponent<Animator>().SetBool("isSuper", false);
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            skill1Effect.transform.position = player.transform.position;
            isSkill1 = true;
            isCooldown1 = true;
            ability1Image.fillAmount = 1;
            ability1KeyPressed = false;
            warningCircle.SetActive(false);
            DeleteSkill(skill1Effect, isSkill1);
            StartCoroutine(IsSkill());
            Invoke("OffSkill1", skill1Effect.GetComponent<SkillPlayer>().timeD);
        }

        if (isCooldown1)
        {
            ability1Image.fillAmount -= 1 / cooldown1 * Time.deltaTime;

            if (ability1Image.fillAmount <= 0)
            {
                ability1Image.fillAmount = 0;
                isCooldown1 = false;
            }
        }
    }
    public void OffSkill1()
    {
        isSkill1 = false;
    }
    public void Skill1(Vector2 mousePosition)
    {
        Vector3 direction = ((Vector3)mousePosition - skill1Effect.transform.position).normalized;
        skill1Effect.transform.position += direction * 15f * Time.deltaTime;
    }

    private void Abilities2()
    {
        if (Input.GetKeyDown(ability2) && !isCooldown2)
        {
            player.isSkill = true;
            ability2KeyPressed = true;
            ability1KeyPressed = false;
            ability3KeyPressed = false;
            warningCircle.SetActive(true);
            warningCircle.transform.localScale = new Vector3(1f, 1f, 0f);
        }
        if (Input.GetMouseButtonDown(0) && ability2KeyPressed && !isCooldown2 && !ability1KeyPressed && !ability3KeyPressed)
        {
            skill2Effect.SetActive(true);
            skill2Effect.GetComponent<AudioSource>().Play();
            if (isSkill4) skill2Effect.GetComponent<Animator>().SetBool("isSuper", true);
            else skill2Effect.GetComponent<Animator>().SetBool("isSuper", false);
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            skill2Effect.transform.position = player.transform.position;
            isSkill2 = true;
            isCooldown2 = true;
            ability2Image.fillAmount = 1;
            ability2KeyPressed = false;
            warningCircle.SetActive(false);
            DeleteSkill(skill2Effect, isSkill2);
            StartCoroutine(IsSkill());
            Invoke("OffSkill2", skill2Effect.GetComponent<SkillPlayer>().timeD);
        }
        if (isCooldown2)
        {
            ability2Image.fillAmount -= 1 / cooldown2 * Time.deltaTime;

            if (ability2Image.fillAmount <= 0)
            {
                ability2Image.fillAmount = 0;
                isCooldown2 = false;
            }
        }
    }
    public void OffSkill2()
    {
        isSkill2 = false;
    }
    protected void Skill2(Vector2 mousePosition)
    {
        Vector3 direction = ((Vector3)mousePosition - skill2Effect.transform.position).normalized;
        skill2Effect.transform.position += direction * 10f * Time.deltaTime;
    }
    private void Abilities3()
    {
        if (Input.GetKeyDown(ability3) && !isCooldown3)
        {
            player.isSkill = true;
            ability3KeyPressed = true;
            ability2KeyPressed = false;
            ability1KeyPressed = false;
            warningCircle.SetActive(true);
            warningCircle.transform.localScale = new Vector3(3f, 3f, 0f);
        }
        if (Input.GetMouseButtonDown(0) && !ability2KeyPressed && !isCooldown3 && !ability1KeyPressed && ability3KeyPressed)
        {
            skill3Effect.SetActive(true);
            skill3Effect.GetComponent<AudioSource>().Play();
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            skill3Effect.transform.position = mousePosition;
            isSkill3 = true;
            isCooldown3 = true;
            ability3Image.fillAmount = 1;
            ability3KeyPressed = false;
            warningCircle.SetActive(false);
            DeleteSkill(skill3Effect, isSkill3);
            StartCoroutine(IsSkill());
            Invoke("OffSkill3", skill3Effect.GetComponent<SkillPlayer>().timeD);
        }
        if (isCooldown3)
        {
            ability3Image.fillAmount -= 1 / cooldown3 * Time.deltaTime;

            if (ability3Image.fillAmount <= 0)
            {
                ability3Image.fillAmount = 0;
                isCooldown3 = false;
            }
        }
    }
    public void OffSkill3()
    {
        isSkill3 = false;
    }
    private void Abilities4()
    {
        if (Input.GetKeyDown(ability4) && !isCooldown4)
        {
            skill4Effect.SetActive(true);
            skill4Effect.GetComponent<AudioSource>().Play();
            playerStats.baseAttack *= skill4Effect.GetComponent<SkillPlayer>().buffDame;
            isSkill4 = true;
            isCooldown4 = true;
            ability4Image.fillAmount = 1;
            DeleteSkill(skill4Effect, isSkill4);
            Invoke("OffSkill4", skill4Effect.GetComponent<SkillPlayer>().timeD);
        }
        if (isCooldown4)
        {
            ability4Image.fillAmount -= 1 / cooldown4 * Time.deltaTime;

            if (ability4Image.fillAmount <= 0)
            {
                ability4Image.fillAmount = 0;
                isCooldown4 = false;
            }
        }
    }
    public void OffSkill4()
    {
        isSkill4 = false;
    }
    protected void Skill4()
    {
        skill4Effect.transform.position = player.transform.position - new Vector3(0, 0.2f, 0);
    }
    public void DeleteSkill(GameObject skillEffect, bool isSkill)
    {
        if (skillEffect.activeSelf)
            StartCoroutine(skillEffect.GetComponent<SkillPlayer>().DestroySkill());
    }
    public IEnumerator IsSkill(){
        yield return new WaitForSeconds(0.5f);
        player.isSkill = false;
    }
}
