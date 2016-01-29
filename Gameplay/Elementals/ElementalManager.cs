using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MelSpaceHunter.Gameplay.Elementals
{
    class ElementalManager
    {
        private List<Elemental> visibleElementals, hiddenElementals;

        public ElementalManager()
        {
            this.visibleElementals = new List<Elemental>();
            this.hiddenElementals = new List<Elemental>();
        }
    }
}
