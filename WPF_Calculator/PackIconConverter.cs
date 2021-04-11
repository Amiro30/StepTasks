using System.Collections.Generic;
using System.Linq;
using MaterialDesignThemes.Wpf;

namespace WPF_Calculator
{
    public static class PackIconConverter
    {
        private static Dictionary<PackIconKind, string> iconDictionary = new Dictionary<PackIconKind, string>
        {
            [PackIconKind.Number1] = "1",
            [PackIconKind.Number2] = "2",
            [PackIconKind.Number3] = "3",
            [PackIconKind.Number4] = "4",
            [PackIconKind.Number5] = "5",
            [PackIconKind.Number6] = "6",
            [PackIconKind.Number7] = "7",
            [PackIconKind.Number8] = "8",
            [PackIconKind.Number9] = "9",
            [PackIconKind.Number0] = "0",
            [PackIconKind.Division] = "/",
            [PackIconKind.Multiplication] = "*",
            [PackIconKind.Minus] = "-",
            [PackIconKind.Plus] = "+",
        };
        public static string GetIconValue(PackIconKind icon)
        {
             var result = iconDictionary.FirstOrDefault(x => x.Key == icon).Value;

             return result;
        }
    }
}
