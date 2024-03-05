using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AInteractValidator : AInteractableComponent
{
    public abstract bool OnValidateInteract(PlayerController player);
}
