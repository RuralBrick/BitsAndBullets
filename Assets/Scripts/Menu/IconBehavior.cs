using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IconBehavior : MonoBehaviour
{
    PlayerMovement myPlayer;
    float totalCooldown;

    Image image;
    float currentCooldown = 0;

    public float offset = 20f;
    public int iconCount = 0;
    public TMP_Text countText;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCooldown > 0)
        {
            image.fillAmount = 1f - (currentCooldown / totalCooldown);
            currentCooldown -= myPlayer.cooldownMutliplier * Time.deltaTime;
        }
        else
        {
            image.fillAmount = 1f;
        }
    }

    public void Initialize(PlayerMovement player, float totalCooldown)
    {
        myPlayer = player;
        image.color = player.GetComponent<SpriteRenderer>().color;
        this.totalCooldown = totalCooldown;
    }

    public void StartCooldown()
    {
        currentCooldown = totalCooldown;
    }

    public void addIcon()
    {
        iconCount++;
        if (countText != null) {
            countText.text = iconCount.ToString();
        }
    }

    public void Reset()
    {
        currentCooldown = 0f;
        iconCount = 0;
    }
}
