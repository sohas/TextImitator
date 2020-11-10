namespace Generator
{
    public static class StringExtensions
    {
        public static bool IsInputNum(this string input)
        //проверяет ввод, чтобы парсить число
        {
            if (input.Length == 0) return false;
            else if (input[0] == '0') return false;
            else foreach (var e in input)
                if (!char.IsDigit(e)) return false;
            return true;
        }
    }
}
