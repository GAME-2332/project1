using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;


public class Util {
    public static string Prettify(string s) {
        return Regex.Replace(s, @"(\B[A-Z]+?(?=[A-Z][^A-Z])|\B[A-Z]+?(?=[^A-Z]))", " $1");
    }

    public static Dictionary<string, T> LoadResources<T>(string path) where T : ScriptableObject {
        Dictionary<string, T> ret = new Dictionary<string, T>();
        T[] objs = Resources.LoadAll(path, typeof(T)) as T[];
        foreach (T obj in objs) {
            ret[obj.name] = obj;
        }

        return ret;
    }
}