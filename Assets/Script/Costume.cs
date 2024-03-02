using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
// public class Poses{
//     public Sprite Idle;
//     public Sprite Ready;
//     public Sprite Move;
//     public Sprite Attack;
//     public Sprite Damage;
// }

[CreateAssetMenu(menuName = "Costume")]
public class Costume : ScriptableObject
{  
    public List<Sprite> costumes = new List<Sprite>();
    // public Poses costumes = new Poses();
}
