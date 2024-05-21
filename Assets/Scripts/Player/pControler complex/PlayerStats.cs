using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{
    [Header("Movement")]
     public float moveSpeed = 5;

    [Space(10)]

    [Header("Jump")]
     public float jumpMoveSpeed = 7f;
     public float jumpValue = 0;
     public float maxJump = 5;
     public float jumpPerF = 0.1f;

    [Space(10)]

    [Header("Ground Check")]
     public LayerMask groundMask;
     public Vector2 boxSize;
     public float castDistance;

    [Space(10)]

    [Header("Fall")]
     public float maxFall;
     public float moveDesaceleration = 1;
     public float maxFallToStomp = 5;

    [Space(10)]

    [Header("PhysicMaterails")]
     public PhysicsMaterial2D normalMat;
     public PhysicsMaterial2D bounceMat;
}
