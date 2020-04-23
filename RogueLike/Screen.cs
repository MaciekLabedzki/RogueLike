/// SCREEN CLASS
/// Function of object of this class is to manage screen output. It puts all the required data into one string value and prints it.
/// Additionally it handles quick notifications (with whatever click confirmation)

using System;
using System.Collections.Generic;

namespace RogueLike.ScreenManager
{
    public class Screen
    {
        public List<string> ToPrint;
        public bool ToConfirm;
        public int ScreenWidth;

        private static string welcomeScreen = "START";
        private static string exitScreen =
            "======================================================== END ==========================================================" + '\n';
        private static string strip =
            "=======================================================================================================================" + '\n';

        public Screen() //Creates new object of Screen
        {
            ToPrint = new List<string>();
            ToPrint.Add(welcomeScreen);
            ToPrint = EmbedWithFrameFullScreen(ToPrint);
            ToConfirm = true;
            PrintScreen(); ///To be removed when std printing in place

            ScreenWidth = strip.Length;
        }

        public void endScreen()
        {
            ToPrint = new List<string>();
            ToPrint.Add(exitScreen);
            ToConfirm = true;
            PrintScreen();
        }

        public void PrintScreen()
        {
            foreach (string s in ToPrint)
                Console.WriteLine(s);

            if (ToConfirm)
                Console.ReadKey(false);
            ToPrint = new List<string>();
        }

        public List<string> EmbedWithFrameFullScreen(List<string> toBeFramed, int innerSpacing = 1)
        {
            List<string> ret = new List<string>();

            //determine width of frame (find longest row)
            int frameWidth = 0;
            foreach(string s in toBeFramed)
            {
                if (s.Length > frameWidth) frameWidth = s.Length;
            }
            frameWidth += innerSpacing * 2 + 2;
            string outerSpacing = AddStringsToString(" ", (ScreenWidth - frameWidth) / 2);
            string innerFrame = AddStringsToString("═", frameWidth - 2);

            string topFrame = outerSpacing +"╔" + innerFrame + "╗";
            string bottomFrame = outerSpacing + "╚" + innerFrame + "╝";
            string emptyFrame = outerSpacing + "║" + AddStringsToString(" ", frameWidth - 2)+ "║";

            ret.Add(topFrame);
            ret.Add(emptyFrame);

            foreach (string s in toBeFramed)
            {
                string tmpSpacing = AddStringsToString(" ", (frameWidth - 2 - s.Length) / 2);
                ret.Add(outerSpacing + "║" + tmpSpacing + s + tmpSpacing + "║");
            }

            ret.Add(emptyFrame);
            ret.Add(bottomFrame);

            return ret;
        }

        public string AddStringsToString(string c, int times)
        {
            string s = "";
            for (int i=0; i<times; i++)
                s += c;
            return s;
        }
    }
}
