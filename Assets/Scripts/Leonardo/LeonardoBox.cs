using System;
using System.Collections;
using UnityEngine;

public class LeonardoBox : MonoBehaviour
{    
    public GameObject attack1Box;
    public GameObject attack2Box;
    public GameObject attack3Box;
    public GameObject runAttackBox;
    public Sprite attack1SpriteHitFrame;
    public Sprite attack2SpriteHitFrame;
    public Sprite attack3SpriteHitFrame;
    public Sprite runAttackSpriteHitFrame;
    private SpriteRenderer currentSprite;

    private void Awake()
    {     
        currentSprite = GetComponent<SpriteRenderer>();  
    }

    private void FixedUpdate()
    {
        ActivateAttackBox(currentSprite.sprite);        
    }

    private void ActivateAttackBox(Sprite curSprite)
    {
        attack1Box.gameObject.SetActive(attack1SpriteHitFrame == curSprite);
        attack2Box.gameObject.SetActive(attack2SpriteHitFrame == curSprite);
        attack3Box.gameObject.SetActive(attack3SpriteHitFrame == curSprite);
        runAttackBox.gameObject.SetActive(runAttackSpriteHitFrame == curSprite);    
    } 
}
