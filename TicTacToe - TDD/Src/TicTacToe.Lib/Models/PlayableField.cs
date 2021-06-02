using System.Collections;
using System.Collections.Generic;

namespace TicTacToeLib.Models
{
    public class PlayableField
    {
        public int Dimension { get; private set; }
        public Entry[] Entries { get; private set; }
        
        private int LastOccupiedIndex { get; set; }

        public PlayableField(int dimension)
        {
            this.Dimension = dimension;
            this.Entries = new Entry[dimension * dimension];
        }

        public void SetEntry(Entry entry)
        {
            this.LastOccupiedIndex++;
            this.Entries[this.LastOccupiedIndex] = entry;
        }

        public int GetNextFreeIndex()
        {
            return this.LastOccupiedIndex + 1;
        }
    }
}
