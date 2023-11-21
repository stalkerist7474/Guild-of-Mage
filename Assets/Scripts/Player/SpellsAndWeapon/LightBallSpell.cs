using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBallSpell : Spell
{
    public override void CastSpell(Transform shootPoint)
    {

        Instantiate(SpellBullet, shootPoint);

    }

    public override void StartCooldown()
    {



    }
}
