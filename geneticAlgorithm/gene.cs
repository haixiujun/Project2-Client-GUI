using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_Client_GUI.geneticAlgorithm
{
    class gene
    {
        private int character_Selection;
        public gene()
        {

        }

        public gene(int expression)
        {
            character_Selection = expression;
        }

        public int get_Character_Selection()
        {
            return character_Selection;
        }

        public void set_Character_Selection(int expression)
        {
            character_Selection = expression;
        }
    }
}
