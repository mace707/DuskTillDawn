using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCharacterCollision : MonoBehaviour
{
    public CapsuleCollider CharacterCollider;
    public CapsuleCollider CharacterBlockingCollider;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(CharacterCollider, CharacterBlockingCollider, true);
    }
}
