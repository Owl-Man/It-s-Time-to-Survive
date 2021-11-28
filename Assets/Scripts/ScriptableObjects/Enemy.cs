using UnityEngine;

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