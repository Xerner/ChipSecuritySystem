namespace ChipSecuritySystem
{
    public static class ColorChipExtensions
    {
        /// <summary>Compares one chips colors to another</summary>
        /// <returns>True if the start and end colors both match</returns>
        public static bool IsPairable(this ColorChip chip, ColorChip other)
        {
            return chip.EndColor == other.StartColor;
        }

        /// <summary>
        /// Checks if a chips start or end color matches the provided color
        /// </summary>
        public static bool HasColor(this ColorChip chip, Color color)
        {
            return chip.StartColor == color || chip.EndColor == color;
        }
    }
}
