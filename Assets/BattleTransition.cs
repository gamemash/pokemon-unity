using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class BattleTransition : MonoBehaviour
{

    private readonly string[] transitions =
    {
        "angular_pattern",
        "saw_tooth_pattern",
        "criss_cross_pattern",
        "simple_patten",
        "diagonal_distort_pattern",
        "spiral_pattern",
        "noise_pattern",
        "topbottom_pattern",
        "poke_pattern"
    };

    // Use this for initialization
    void Start()
    {
        StartCoroutine(CutOff(RandomTransition()));
    }

    Texture2D RandomTransition()
    {
        Random r = new Random();
        int id = r.Next(0, transitions.Length - 1);
        var texture = "BattleTransitionTextures/" + transitions[id];
        return (Texture2D)Resources.Load(texture);
    }

    public IEnumerator CutOff(Texture2D texture)
    {
        var cutOff = 0.0f;
        var velocity = 2.0f;
        GetComponent<RawImage>().material.SetTexture("_TransitionTex", texture);
        while (cutOff < 1.0f)
        {
            cutOff += velocity * Time.fixedDeltaTime;
            GetComponent<RawImage>().material.SetFloat("_Cutoff",cutOff);
            yield return new WaitForFixedUpdate();
        }
    }
}