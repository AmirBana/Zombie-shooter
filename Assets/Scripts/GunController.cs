using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class GunController : MonoBehaviour
{
    public Transform rightHand, leftHand;
    public TwoBoneIKConstraint twoBoneIKConstraint_right;
    public TwoBoneIKConstraint twoBoneIKConstraint_left;
    public RigBuilder rig;
    public PlayerController playerController;
    public Transform firePos;
    public GameObject bullet;
    public float range;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        playerController.firePos = firePos;
        playerController.bullet = bullet;
        playerController.range = range;
        playerController.gunFire = firePos.GetComponentsInChildren<ParticleSystem>()[0];
        twoBoneIKConstraint_right.data.target = rightHand;
        twoBoneIKConstraint_left.data.target = leftHand;
        rig.Build();
    }
}
