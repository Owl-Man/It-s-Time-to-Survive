using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public new string name;
    public string description;

    public int damage;
    public int health;
    public int attackSpeed;
    public int EXP;

    public Sprite sprite;
}