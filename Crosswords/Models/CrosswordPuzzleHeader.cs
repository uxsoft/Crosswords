namespace Crosswords.Models
{
    public class CrosswordPuzzleHeader : ICrosswordPuzzleCell
    {
        public string Right { get; set; } = "";
        public string Down { get; set; } = "";
    }
    
}