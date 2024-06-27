using System;
using System.Collections.Generic;
using System.Linq;

namespace ChipSecuritySystem
{
    // See FindLargestSolution() for algorithm
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            List<ColorChip> chips = new List<ColorChip>() { 
                new ColorChip(Color.Orange, Color.Yellow),
                new ColorChip(Color.Yellow, Color.Green),
                new ColorChip(Color.Blue, Color.Orange),
                new ColorChip(Color.Orange, Color.Blue),
                new ColorChip(Color.Blue, Color.Orange),
                new ColorChip(Color.Blue, Color.Orange),
                new ColorChip(Color.Orange, Color.Yellow),
                new ColorChip(Color.Orange, Color.Red),
                new ColorChip(Color.Red, Color.Yellow),
                new ColorChip(Color.Blue, Color.Orange),
                new ColorChip(Color.Blue, Color.Orange),
                new ColorChip(Color.Yellow, Color.Green),
                new ColorChip(Color.Yellow, Color.Green),
            };

            Console.WriteLine($"Available chips ({chips.Count} chips)");
            Console.WriteLine();
            foreach (var chip in chips)
                Console.WriteLine(chip);
            Console.WriteLine();

            ChipSolution solution = program.FindLargestSolution(chips);
            if (solution.IsASolution)
            {
                Console.WriteLine($"Unlocking master panel with following solution ({solution.Chips.Count} chips)");
                Console.WriteLine();
                Console.WriteLine(solution);
            }
            else
            {
                Console.WriteLine("No solutions found");
                Console.WriteLine(Constants.ErrorMessage);
            }
            var _ = Console.ReadLine();
        }

        /// <summary>
        /// Considers all valid solutions and returns the largest possible solution of chips
        /// </summary>
        /// <param name="chips">The chips to find a solution in</param>
        /// <returns>The largest possible solution</returns>
        public ChipSolution FindLargestSolution(List<ColorChip> chips, ChipSolution largestSolution = null, ChipSolution currentSolution = null)
        {
            // Initialize internal recursive variables
            if (currentSolution is null)
                currentSolution = new ChipSolution();
            
            if (largestSolution == null)
                largestSolution = new ChipSolution();

            // Corner case: No chips containing blue or green on the 1st iteration
            if (currentSolution.Chips.Count == 0 && (!HasValidStartChip(chips) || !HasValidEndChip(chips)))
            {
                return largestSolution;
            }

            // Find current pairable chips
            Color currentEndColor;
            if (currentSolution.Chips.Count == 0)
                currentEndColor = ChipSolution.START_COLOR;
            else
                currentEndColor = currentSolution.Chips[currentSolution.Chips.Count-1].EndColor;

            List<ColorChip> pairableChips = GetPairableChips(chips, currentEndColor);

            // Base Case
            bool unsolvableInput = pairableChips.Count == 0 || !HasValidEndChip(chips);
            if (unsolvableInput)
            {
                // Did we find a larger solution?
                if (currentSolution.IsASolution && currentSolution.Chips.Count > largestSolution.Chips.Count)
                    // We did! Deep copy it
                    largestSolution = new ChipSolution(currentSolution);
            }
            // Recursive case
            else 
            {
                foreach (var chip in pairableChips)
                {
                    // Add a chip on to the current solution and evaluate new choices
                    chips.Remove(chip);
                    currentSolution.Chips.Add(chip);
                    largestSolution = FindLargestSolution(chips, largestSolution, currentSolution);
                    // Remove the chip and try the next one
                    currentSolution.Chips.Remove(chip);
                    chips.Add(chip);
                }
            }
            return largestSolution;
        }

        /// <summary>
        /// Does the list of chips contain a chip whose colors 
        /// start with <see cref="ChipSolution.START_COLOR">ChipSolution.START_COLOR</see>?
        /// </summary>
        public bool HasValidStartChip(List<ColorChip> chips)
        {
            return GetChipWithColor(chips, ChipSolution.START_COLOR) != null;
        }

        /// <summary>
        /// Does the list of chips contain a chip whose colors 
        /// end with <see cref="ChipSolution.START_COLOR">ChipSolution.END_COLOR</see>?
        /// </summary>
        public bool HasValidEndChip(List<ColorChip> chips)
        {
            return GetChipWithColor(chips, ChipSolution.END_COLOR) != null;
        }

        /// <summary>Find a chip that has the specified color on its start or end</summary>
        /// <returns>A chip, or null</returns>
        public ColorChip GetChipWithColor(List<ColorChip> chips, Color color)
        {
            return chips.FirstOrDefault(chip => chip.HasColor(color));
        }

        /// <summary>Find all chips that have the provided color as their start color</summary>
        /// <returns>A list of chips</returns>
        public List<ColorChip> GetPairableChips(List<ColorChip> chips, Color pairColor)
        {
            return chips.Where(chip => chip.StartColor == pairColor).ToList();
        }
    }
}
