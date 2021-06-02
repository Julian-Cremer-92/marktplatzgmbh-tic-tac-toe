using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeLib.Tests
{
    class FakeUserInput : IUserInput
    {
        private int step = -1;
        private int[] _fieldsPlayer;

        public int[] FieldsPlayer { set { _fieldsPlayer = value; step = -1; } }        
       
               
        public int GetField(Player player)
        {
            try
            {
                step++;
                return _fieldsPlayer[step];
            }
            catch (Exception ex)
            {
                throw new ArgumentOutOfRangeException("Es werden mehr Feldeingaben abgerufen als hinterlegt wurden");                
            }
        }
    }
}
