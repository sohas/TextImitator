using System.Text;

namespace Generator
{
    public static class StringBuilderExtensions
    {
        public static string FindSample(this StringBuilder text, int size)
        //из текста вытаскивает образец размера size с конца текста
        {
            var sample = new StringBuilder();
            for (int i = text.Length - size; i < text.Length; i++)
                sample.Append(text[i]);
            return sample.ToString();
        }
    }
}
