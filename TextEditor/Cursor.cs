using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor
{
    public static class Cursor
    {
        const string ESC = "\x1b[";

        public static void MoveUp(int positions) => Console.Write($"{ESC}{positions}A");

        public static void MoveDown(int positions) => Console.Write($"{ESC}{positions}B");

        public static void MoveForward(int positions) => Console.Write($"{ESC}{positions}C");

        public static void MoveBackward(int positions) => Console.Write($"{ESC}{positions}D");

        public static void SavePosition(int positions) => Console.Write($"{ESC}7");

        public static void RestorePosition(int positions) => Console.Write($"{ESC}8");

        public static void PrintPosition() => Console.Write($"{ESC}6n");
    }
}
