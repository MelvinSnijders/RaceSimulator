using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    static class Visualizer
    {

        #region graphics
        private static string[] _startHorizontal = { "────", ":1  ", "  :2", "────" };
        private static string[] _startVertical = { "│\" │", "│ \"│", "│\" │", "│ \"│" };
        private static string[] _finishHorizontal = { "────", "  # ", "  # ", "────", };
        private static string[] _finishVertical = { "|  |", "|  |", "|##|", "|  |" };
        private static string[] _leftCornerVertical = { "───╮", "   │", "   │", "╮  │" };
        private static string[] _rightCornerVertical = { "╭───", "│   ", "│   ", "│  ╭" };
        private static string[] _rightCornerHorizontal = { "│  ╰", "│   ", "│   ", "╰───" };
        private static string[] _leftCornerHorizontal = { "╯  │", "   │", "   │", "───╯" };
        private static string[] _straightHorizontal = { "────", "    ", "    ", "────", };
        private static string[] _straightVertical = { "│  │", "│  │", "│  │", "│  │" };

        #endregion

        public static void Initialize()
        {

        }

        public static string ReplaceStringWithParticipant(string input, IParticipant participant1, IParticipant participant2)
        {
            string replaced = input;
            if (participant1 != null)
            {
                string par1 = participant1.Name.Substring(0, 1);
                replaced = replaced.Replace("2", par1);
            }
            if (participant2 != null)
            {
                string par2 = participant2.Name.Substring(0, 1);
                replaced = replaced.Replace("1", par2);
            }
            return replaced;
        }

        public static void DrawTrack(Race race, Track track)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            LinkedList <Section> sections = track.Sections;
            Console.SetCursorPosition(100, 100);
            int currentDirection = 3;
            foreach (Section section in sections)
            {
                SectionData sectionData = race.GetSectionData(section);
                switch (section.SectionType)
                {
                    case SectionTypes.StartGrid:
                        string[] replacedStartHorizontal = new string[4];
                        for(int i = 0; i < _startHorizontal.Length; i++)
                        {
                            replacedStartHorizontal[i] = ReplaceStringWithParticipant(_startHorizontal[i], sectionData.Left, sectionData.Right);
                        }
                        DrawSection(replacedStartHorizontal, _startVertical, currentDirection);
                        break;
                    case SectionTypes.Finish:
                        DrawSection(_finishHorizontal, _finishVertical, currentDirection);
                        break;
                    case SectionTypes.LeftCorner:
                        if (currentDirection == 3 || currentDirection == 4)
                        {
                            DrawSection(_rightCornerVertical, _rightCornerHorizontal, currentDirection);
                        }
                        else
                        {
                            DrawSection(_leftCornerHorizontal, _leftCornerVertical, currentDirection);
                        }
                        currentDirection = SwitchDirection(currentDirection, -1);
                        break;
                    case SectionTypes.RightCorner:
                        if (currentDirection == 1 || currentDirection == 2)
                        {
                            DrawSection(_leftCornerVertical, _leftCornerHorizontal, currentDirection);
                        }
                        else
                        {
                            DrawSection(_rightCornerHorizontal, _rightCornerVertical, currentDirection);
                        }
                        currentDirection = SwitchDirection(currentDirection, 1);
                        break;
                    case SectionTypes.Straight:
                        DrawSection(_straightHorizontal, _straightVertical, currentDirection);
                        break;

                }
            }


        }

        public static int SwitchDirection(int fromDirection, int add)
        {
            if (add == 1 && fromDirection == 3)
            {
                return 0;
            }
            else if (add == -1 && fromDirection == 0)
            {
                return 3;
            }
            else
            {
                return fromDirection + add;
            }
        }

        public static bool IsHorizontal(int direction)
        {
            return direction == 1 || direction == 3;
        }

        public static void DrawSection(string[] horizontalSection, string[] verticalSection, int direction)
        {
            switch (direction)
            {
                // UP
                case 0:
                    MoveCursor(0, -8);
                    break;
                // RIGHT
                case 1:
                    MoveCursor(4, -4);
                    break;
                // LEFT
                case 3:
                    MoveCursor(-4, -4);
                    break;
            }
            if (IsHorizontal(direction))
            {
                PrintSectionArray(horizontalSection);
            }
            else
            {
                PrintSectionArray(verticalSection);
            }
        }

        public static void PrintSectionArray(string[] sectionArray)
        {
            foreach (string section in sectionArray)
            {
                MoveCursor(-4, 1);
                Console.Write(section);
       
            }
        }

        public static void MoveCursor(int x, int y)
        {
            Console.SetCursorPosition(Console.CursorLeft + x, Console.CursorTop + y);
        }

    }
}
