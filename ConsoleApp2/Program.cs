string[] testInput = { "abba", "abcba" };

Dictionary<char, int> charDict = new Dictionary<char, int>();

foreach (string input in testInput)
{
    bool found = false;
    for (int i = 0; i < input.Length; i++)
    {
        char c = input[i];
        if(!charDict.ContainsKey(c)) charDict.Add(c, i);
        for (int j = i + 1; j < input.Length; j++)
        {

            if (c == input[j])
            {
                found = false;
                break;
            }
            else if (charDict[input[i]] > 1)
            {
                found = true;
            }
        }

        if (found)
        {
            Console.WriteLine(i);
            break;
        }
    }

    if (!found)
    {
        Console.WriteLine("-1");
    }
}