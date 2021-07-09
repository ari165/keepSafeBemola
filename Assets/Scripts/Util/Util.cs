using System;

public static class Util
{
    public static T[] Merge<T>(this T[] first, params T[] second)
    {
        if (first == null) {
            return second;
        }
        if (second == null) {
            return first;
        }
 
        T[] result = new T[first.Length + second.Length];
        first.CopyTo(result, 0);
        second.CopyTo(result, first.Length);
 
        return result;
    }

    public static String ConvertToPersian(int number)
    {
        String num = number.ToString();
        char[] charArr = num.ToCharArray();
        Char[] outputArr = new char[charArr.Length];
        
        for (int i = 0; i < charArr.Length; i++)
        {
            outputArr[i] = charArr[i] switch
            {
                '1' => '۱',
                '2' => '۲',
                '3' => '۳',
                '4' => '۴',
                '5' => '۵',
                '6' => '۶',
                '7' => '۷',
                '8' => '۸',
                '9' => '۹',
                '0' => '۰',
                _ => outputArr[i]
            };
        }
        return new string(outputArr);
    }
    
    public static String ConvertToPersian(string number)
    {
        char[] charArr = number.ToCharArray();
        Char[] outputArr = new char[charArr.Length];
        
        for (int i = 0; i < charArr.Length; i++)
        {
            outputArr[i] = charArr[i] switch
            {
                '1' => '۱',
                '2' => '۲',
                '3' => '۳',
                '4' => '۴',
                '5' => '۵',
                '6' => '۶',
                '7' => '۷',
                '8' => '۸',
                '9' => '۹',
                '0' => '۰',
                '.' => '.',
                _ => outputArr[i]
            };
        }
        return new string(outputArr);
    }
    
    public static String ConvertToPersian(float number)
    {
        String num = number.ToString();
        char[] charArr = num.ToCharArray();
        Char[] outputArr = new char[charArr.Length];
        
        for (int i = 0; i < charArr.Length; i++)
        {
            outputArr[i] = charArr[i] switch
            {
                '1' => '۱',
                '2' => '۲',
                '3' => '۳',
                '4' => '۴',
                '5' => '۵',
                '6' => '۶',
                '7' => '۷',
                '8' => '۸',
                '9' => '۹',
                '0' => '۰',
                '.' => '.',
                _ => outputArr[i]
            };
        }
        return new string(outputArr);
    }
}
