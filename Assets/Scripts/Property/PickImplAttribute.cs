using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Similar to TypeSelectorAttribute, but allows selecting the type directly on a parent class field.
/// </summary>
public class PickImplAttribute : PropertyAttribute {
    public Type baseType;
    public PickImplAttribute(Type baseType) {
        this.baseType = baseType;
    }
}