using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Talent", menuName = "Talent", order = 51)]
public class TalentObject : ScriptableObject
{
    [SerializeField]
    private Sprite icon;
    
    [SerializeField]
    private string _name;

    [SerializeField]
    private Vector2 pos;

    [SerializeField]
    private TalentObject prereq;

    [SerializeField]
    private int rank;

    [SerializeField]
    private int maxRank;

    public Sprite Icon
    {
        get
        {
            return icon;
        }
    }

    public string Name
    {
        get
        {
            return _name;
        }
    }

    public Vector2 Pos
    {
        get
        {
            return pos;
        }
    }

    public TalentObject Prereq
    {
        get
        {
            return prereq;
        }
    }

    public int Rank
    {
        get
        {
            return rank;
        }
    }

    public int MaxRank
    {
        get
        {
            return maxRank;
        }
    }

    public bool incRank()
    {
        if (rank < maxRank)
        {
            rank++;
            return true;
        }
        return false;
    }
    
    public void ResetRank()
    {
        rank = 0;
    }

    public void SetRank(int i)
    {
        rank = i;
    }

    public bool canIncRank => rank < maxRank;
}
