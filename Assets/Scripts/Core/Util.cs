using System.Text.RegularExpressions;

public class Util {
    public static string Prettify(string s) {
        return Regex.Replace(s, @"(\B[A-Z]+?(?=[A-Z][^A-Z])|\B[A-Z]+?(?=[^A-Z]))", " $1");
    }
}