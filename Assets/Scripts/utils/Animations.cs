using System;
using UnityEngine;
using System.Collections;
using TMPro;

public static class Animations{

    private static readonly float flashDelay = .1f;

    public static IEnumerator SpriteFlash(SpriteRenderer spriteRenderer, Color flashColor, int times){
        Color originalColor = spriteRenderer.color;
        for(int i=0; i<times; i++){
            spriteRenderer.color = flashColor;
            yield return new WaitForSeconds(flashDelay);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(flashDelay);
        }
        spriteRenderer.color = originalColor;
    }

    public static IEnumerator TextFlash(TMP_Text text, Color flashColor, int times){
        Debug.Log("HMM");
        Color originalColor = text.color;
        for(int i=0; i<times; i++){
            text.color = flashColor;
            yield return new WaitForSeconds(flashDelay);
            text.color = originalColor;
            yield return new WaitForSeconds(flashDelay);
        }
        text.color = originalColor;
    }

}