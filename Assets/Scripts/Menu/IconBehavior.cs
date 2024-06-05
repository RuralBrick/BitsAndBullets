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
        
        if (iconCount == 0)
        {
            image.enabled = false;
        }

        if (countText != null)
        {
            countText.enabled = false;
        }
    }

    public void StartCooldown()
    {
        currentCooldown = totalCooldown;
    }

    public void addIcon()
    {
        Debug.Log("ADDING ICON");
        iconCount++;
        if (countText != null && iconCount > 1) {
            countText.enabled = true;
            countText.text = iconCount.ToString();
        }
        image.enabled = true;
    }

    public void removeIcon() {
        iconCount = 0;
        // This is how it should work in general but made the change for shield icon
        // If we let them have muliple shield icons we'll set iconCount to -=1;
        if (countText != null && iconCount < 1)
        {
            countText.enabled = false;
        }
        else
        {
            countText.text = iconCount.ToString();
        }

        if (iconCount == 0)
        {
            image.enabled = false;
        }
    }

    public void Reset()
    {
        currentCooldown = 0f;
        iconCount = 0;
    }
}
