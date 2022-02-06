using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class LSystemGen : MonoBehaviour
{
    public Rule[] rules;
    public string axiom;
    public bool Radial;
    public bool Rectilinear;
    public bool PopBased;
    [Range(0,10)]
    public int noOfIterations = 1;
    public bool randIgnore = true;
    [Range(0, 1)]
    public float chanceToIgnore = 0.3f;

    private void Start()
    {
        Debug.Log(genSentence());
    }
    public string genSentence(string word = null)
    {
        if(word == null)
        {
            if (PopBased)
            {
                axiom = "(d)<(d)<(d)<(d)";
            }
            else if (Rectilinear)
            {
                axiom = "((dd(<dddddd)(>dddddd)dd(<dddddd)(>dddddd)dd(<dddd)(>dddd)))>>(dd(<dddddd)(>dddddd)dd(<dddddd)(>dddddd)dd(<dddd)(>dddd))))>(dd(<dddddd)(>dddddd)dd(<dddddd)(>dddddd)dd(<dddd)(>dddd))>>(dd(<dddddd)(>dddddd)dd(<dddddd)(>dddddd)dd(<dddd)(>dddd)))";
            }
            else if (Radial)
            {
                axiom = "(ddddddd)<(ddddddd)<(ddddddd)<(ddddddd)<(ddddddd)<(ddddddd)";

            }
            word = axiom;
        }
        return grow(word, 0);
    }

    private string grow(string word, int iteration)
    {
        if (iteration >= noOfIterations)
        {
            return word;
        }
        StringBuilder newWord = new StringBuilder();
        foreach (var c in word)
        {
            newWord.Append(c);
            processRules(newWord, c, iteration);
        }
        return newWord.ToString();
    }

    private void processRules(StringBuilder newWord, char c, int iteration)
    {
        foreach(var rule in rules)
        {
            if(rule.ruleLetter == c.ToString())
            {
                if (randIgnore&& iteration > 1)
                {
                    if(UnityEngine.Random.value < chanceToIgnore)
                    {
                        return;
                    }
                }
                newWord.Append(grow(rule.GetOutput(), iteration + 1));
            }
        }
    }
}
