using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireballSpell : Spell
{


    public override void CastSpell(Transform shootPoint)
    {
         
        Instantiate(SpellBullet, shootPoint);

    }

    public override void StartCooldown()
    {
        

                
    }
}
