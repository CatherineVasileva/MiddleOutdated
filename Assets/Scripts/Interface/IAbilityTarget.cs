using UnityEngine;
using System.Collections.Generic;

public interface IAbilityTarget : IAbility
{
    List<Collider> Targets { get; set; }
}
