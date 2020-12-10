using System.Collections.Generic;
using System.Text.RegularExpressions;

public class ArgParser
{
    private Dictionary<string, string> _args = new Dictionary<string, string>();

    public ArgParser()
    {
        string[] args = System.Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            if (Regex.IsMatch(args[i], "^--.*$") && i < args.Length - 1)
                _args.Add(args[i], args[i + 1]);
        }
    }

    public string GetArg(string arg)
    {
        if (!_args.ContainsKey(arg)) return null;
        return _args[arg];
    }
}