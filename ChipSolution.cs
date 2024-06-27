using System;
using System.Collections.Generic;
using System.Text;

namespace ChipSecuritySystem
{
    public class ChipSolution
    {
        public static readonly Color START_COLOR = Color.Blue;
        public static readonly Color END_COLOR = Color.Green;
        
        public List<ColorChip> Chips;

        /// <summary>
        /// This solution has chips, and starts and ends with the correct colors
        /// </summary>
        public bool IsASolution { 
            get => Chips.Count > 0 
                && Chips[0].StartColor == START_COLOR 
                && Chips[Chips.Count - 1].EndColor == END_COLOR; 
        }

        public ChipSolution()
        {
            Chips = new List<ColorChip>();
        }

        public ChipSolution(ChipSolution other)
        {
            Chips = new List<ColorChip>();
            foreach (var chip in other.Chips)
            {
                Chips.Add(chip);
            }
        }

        public override string ToString()
        {
            if (Chips.Count == 0) return "No pairable chips found";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(START_COLOR.ToString());
            foreach (var chip in Chips)
            {
                stringBuilder.AppendLine(chip.ToString());
            }
            stringBuilder.AppendLine(END_COLOR.ToString());
            return stringBuilder.ToString();
        }
    }
}
