﻿using System;

[Serializable]
public abstract class CheckGlobalsPredicate<T> : IStatePredicate {
    public string variableName;
    public T checkValue;

    public abstract T GetValue(string name);
    
    public bool Check() {
        return checkValue.Equals(GetValue(variableName));
    }
}