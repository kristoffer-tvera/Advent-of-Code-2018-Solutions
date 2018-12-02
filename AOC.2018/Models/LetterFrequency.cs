namespace AOC._2018.Models
{
    /// <summary>
    /// Basically the equivalent of a Tuple, but mutable.
    /// </summary>
    public class LetterFrequency
    {
        public LetterFrequency(char letter, int frequency)
        {
            Letter = letter;
            Frequency = frequency;
        }

        public char Letter { get; set; }
        public int Frequency { get; set; }
    }
}