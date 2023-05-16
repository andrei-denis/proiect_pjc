using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    public static Save instance { set; get; }

    public long coins;

    void Awake()
    {
        Time.timeScale = 0;

        instance = this;

        coins = 4;
    }

}
