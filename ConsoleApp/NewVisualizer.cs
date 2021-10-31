using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    /// <summary>
    /// Class for visualuzing a race in the console
    /// </summary>
    class NewVisualizer
    {
        private Track Track;
        private Direction CurrentDirection = Direction.West;

        // Values for calculating start cursor position.
        private int MinX = 0;
        private int MaxX = 0;
        private int MinY = 0;
        private int MaxY = 0;

        public NewVisualizer(Track track)
        {
            this.Track = track;
        }

        /// <summary>
        /// Enum like class for all graphics that can be displayed in the console.
        /// </summary>
        #region graphics
        static class RaceGraphics
        {
            public static string[] StartHorizontal = {
            "────",
            ":1  ",
            "  :2",
            "────"
        };

            public static string[] StartVertical = {
            "│\" │",
            "│ \"│",
            "│\" │",
            "│ \"│"
        };

            public static string[] FinishHorizontal = {
            "────",
            "  # ",
            "  # ",
            "────",
        };

            public static string[] FinishVertical = {
            "|  |",
            "|  |",
            "|##|",
            "|  |"
        };

            public static string[] LeftCornerVertical = {
            "───╮",
            "   │",
            "   │",
            "╮  │"
        };

            public static string[] RightCornerVertical = {
            "╭───",
            "│   ",
            "│   ",
            "│  ╭"
        };

            public static string[] RightCornerHorizontal = {
            "│  ╰",
            "│   ",
            "│   ",
            "╰───"
        };

            public static string[] LeftCornerHorizontal = {
            "╯  │",
            "   │",
            "   │",
            "───╯"
        };

            public static string[] StraightHorizontal = {
            "────",
            "    ",
            "    ",
            "────",
        };

            public static string[] StraightVertical = {
            "│  │",
            "│  │",
            "│  │",
            "│  │"
        };

        }
        #endregion

        /// <summary>
        /// Enum with all possible directions
        /// </summary>
        enum Direction
        {
            North = 0,
            East = 1,
            South = 2,
            West = 3
        }

        /// <summary>
        /// Draws the track in the console.
        /// </summary>
        public void DrawTrack()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            CalculateSize();
            SetCursorToStart();

            foreach (Section section in Track.Sections)
            {
                SectionData data = Data.CurrentRace.GetSectionData(section);

                switch (section.SectionType)
                {
                    case SectionTypes.StartGrid:
                    case SectionTypes.Finish:
                    case SectionTypes.Straight:
                        DrawStraightTrack(section.SectionType, data);
                        break;
                    default:
                        DrawCornerTrack(section.SectionType, data);
                        break;
                }

                MoveCursorInDirection(CurrentDirection);

            }
        }

        /// <summary>
        /// Change the cursor position to the start of the calculated track.
        /// Prevents printing outside the boundaries of the window.
        /// </summary>
        private void SetCursorToStart()
        {
            Console.SetCursorPosition(Math.Abs(MinX * 4), Math.Abs(MaxY * 4));
        }

        /// <summary>
        /// Calculate the outer boundaries of the track.
        /// </summary>
        public void CalculateSize()
        {
            int currentX = 0;
            int currentY = 0;

            foreach (Section section in Track.Sections)
            {
                if (currentX > MaxX)
                {
                    MaxX = currentX;
                }
                if (currentX < MinX)
                {
                    MinX = currentX;
                }
                if (currentY > MaxY)
                {
                    MaxY = currentY;
                }
                if (currentY < MinY)
                {
                    MinY = currentY;
                }
                switch (CurrentDirection)
                {
                    case Direction.North:
                        currentY++;
                        break;
                    case Direction.East:
                        currentX++;
                        break;
                    case Direction.South:
                        currentY--;
                        break;
                    case Direction.West:
                        currentX--;
                        break;
                }

                CurrentDirection = CalculateFutureDirection(section.SectionType, CurrentDirection);
            }
        }

        /// <summary>
        /// Calculate the next direction that the track will have to go.
        /// </summary>
        /// <param name="section">The current section.</param>
        /// <param name="currentDirection">The current direction.</param>
        /// <returns>The new direction of the track.</returns>
        private Direction CalculateFutureDirection(SectionTypes section, Direction currentDirection)
        {
            switch (section)
            {
                // A corner to the left means the previous direction in the enum.
                case SectionTypes.LeftCorner:
                    return currentDirection.Previous();
                // A corner to the left means the next direction in the enum.
                case SectionTypes.RightCorner:
                    return currentDirection.Next();
                // No corner means no change in direction.
                default:
                    return currentDirection;
            }
        }

        /// <summary>
        /// Check if a direction is horizontal.
        /// </summary>
        /// <param name="direction">The direction to check.</param>
        /// <returns>Whether the given direction is horizontal or vertical.</returns>
        private bool DirectionIsHorizontal(Direction direction)
        {
            return direction == Direction.East || direction == Direction.West;
        }

        /// <summary>
        /// Replace the numbers in a graphic with the first letter of the participants.
        /// </summary>
        /// <param name="placeholder">The original placeholder string array.</param>
        /// <param name="participant1">The first participant to replace.</param>
        /// <param name="participant2">The second participant to replace.</param>
        /// <returns>The replaced string with the first letters of the participants.</returns>
        public string[] ReplaceParticipant(string[] placeholder, IParticipant participant1, IParticipant participant2)
        {
            string firstLetter1 = "1";
            string firstLetter2 = "2";

            if (participant1 != null)
            {
                firstLetter1 = participant1.Name.Substring(0, 1);
            }

            if (participant2 != null)
            {
                firstLetter2 = participant2.Name.Substring(0, 1);
            }

            string[] replaced = new string[4];
            for (int i = 0; i < placeholder.Length; i++)
            {
                replaced[i] = placeholder[i].Replace("1", firstLetter1).Replace("2", firstLetter2) ;
            }
            return replaced;
        }

        /// <summary>
        /// Print a track section array to the console, and move the cursor accordingly.
        /// </summary>
        /// <param name="sectionArray">The section array graphics to print.</param>
        public void PrintSectionArray(string[] sectionArray)
        {
            foreach (string section in sectionArray)
            {
                // Print each line and move one down.
                MoveCursor(-4, 1);
                Console.Write(section);
            }
        }

        /// <summary>
        /// Move the cursor based on the direction.
        /// </summary>
        /// <param name="direction">The direction to move the cursor to.</param>
        private void MoveCursorInDirection(Direction direction)
        {
            // A section is 4x4, so check the direction and move the cursor.
            switch (direction)
            {
                case Direction.North:
                    MoveCursor(0, -8);
                    break;
                case Direction.East:
                    MoveCursor(4, -4);
                    break;
                case Direction.West:
                    MoveCursor(-4, -4);
                    break;
            }
        }

        /// <summary>
        /// Move the cursor in the console with defined values.
        /// </summary>
        /// <param name="x">Amount to move in the horizontal direction.</param>
        /// <param name="y">Amount to move in the vertical direction.</param>
        private void MoveCursor(int x, int y)
        {
            Console.SetCursorPosition(Console.CursorLeft + x, Console.CursorTop + y);
        }

        /// <summary>
        /// Draw a straight track secion.
        /// </summary>
        /// <param name="type">The type of section to draw.</param>
        /// <param name="data">The data of the section.</param>
        private void DrawStraightTrack(SectionTypes type, SectionData data)
        {
            string[] horizontal = { };
            string[] vertical = { };

            // Determine what graphics we should use.
            switch (type)
            {
                case SectionTypes.Straight:
                    horizontal = RaceGraphics.StraightHorizontal;
                    vertical = RaceGraphics.StraightVertical;
                    break;
                case SectionTypes.StartGrid:
                    // Replace participants in the start grid.
                    horizontal = ReplaceParticipant(RaceGraphics.StartHorizontal, data.Left, data.Right);
                    vertical = ReplaceParticipant(RaceGraphics.StartVertical, data.Left, data.Right);
                    break;
                case SectionTypes.Finish:
                    horizontal = RaceGraphics.FinishHorizontal;
                    vertical = RaceGraphics.FinishVertical;
                    break;
            }

            Draw(horizontal, vertical);

        }

        /// <summary>
        /// Draw a corner track section.
        /// </summary>
        /// <param name="type">The type of section to draw.</param>
        /// <param name="data">The data of the section.</param>
        private void DrawCornerTrack(SectionTypes type, SectionData data)
        {
            string[] horizontal = { };
            string[] vertical = { };

            // Determine what graphics we should use.
            switch (type)
            {
                case SectionTypes.LeftCorner:
                    // If the facing is different, we can't use the same track.
                    // When the direction is North or West, switch up the type of track.
                    if (CurrentDirection == Direction.West || CurrentDirection == Direction.North)
                    {
                        horizontal = RaceGraphics.RightCornerVertical;
                        vertical = RaceGraphics.RightCornerHorizontal;
                    }
                    else
                    {
                        horizontal = RaceGraphics.LeftCornerHorizontal;
                        vertical = RaceGraphics.LeftCornerVertical;
                    }
                    break;
                case SectionTypes.RightCorner:
                    // If the facing is different, we can't use the same track.
                    // When the direction is East or South, switch up the type of track.
                    if (CurrentDirection == Direction.East || CurrentDirection == Direction.South)
                    {
                        horizontal = RaceGraphics.LeftCornerVertical;
                        vertical = RaceGraphics.LeftCornerHorizontal;
                    }
                    else
                    {
                        horizontal = RaceGraphics.RightCornerHorizontal;
                        vertical = RaceGraphics.RightCornerVertical;
                    }
                    break;
            }

            // Actually draw the graphic in the console
            Draw(horizontal, vertical);

            // Update the direction after the corner.
            CurrentDirection = CalculateFutureDirection(type, CurrentDirection);

        }

        /// <summary>
        /// Actually draw the section to the console based on horizontal or vertical direction.
        /// </summary>
        /// <param name="horizontal">The horizontal string that might get printed.</param>
        /// <param name="vertical">The vertical string that might get printed.</param>
        private void Draw(string[] horizontal, string[] vertical)
        {
            if (DirectionIsHorizontal(CurrentDirection))
            {
                PrintSectionArray(horizontal);
            }
            else
            {
                PrintSectionArray(vertical);
            }
        }


        public static void DriversChangedListener(object sender, DriversChangedEventArgs args)
        {
            NewVisualizer visualizer = new NewVisualizer(args.Track);
            visualizer.DrawTrack();
        }

    }



}
