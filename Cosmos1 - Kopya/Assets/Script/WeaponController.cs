using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    bool canAttack = true;
    bool isStafe = false;
    Animator anim;
    public GameObject handWeapon;
    public GameObject backWeapon;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetBool("iS", isStafe);

        if (Input.GetKeyDown(KeyCode.F))
        {
            isStafe = !isStafe;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && isStafe == true && canAttack == true)
        {
            anim.SetTrigger("Saldır");

        }
        if (isStafe == true)
        {
            GetComponent<SamMoveScript>().hareketTipi = SamMoveScript.MovementType.Strafe;
            GetComponent<IKLookScript>().azal();
        }
        if (isStafe == false)
        {
            GetComponent<SamMoveScript>().hareketTipi = SamMoveScript.MovementType.Directional;
            GetComponent<IKLookScript>().art();
        }
    }
    void equip()
    {
        backWeapon.SetActive(false);
        handWeapon.SetActive(true);
    }
    void unequip()
    {
        backWeapon.SetActive(true);
        handWeapon.SetActive(false);
    }
}
