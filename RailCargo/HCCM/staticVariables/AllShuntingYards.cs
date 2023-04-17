using System;
using System.Collections.Generic;
using RailCargo.HCCM.ControlUnits;

namespace RailCargo.HCCM.staticVariables
{
    public class AllShuntingYards
    {
        private static AllShuntingYards _instance;

        public static AllShuntingYards Instance
        {
            get {
                if (_instance == null)
                {
                    return new AllShuntingYards();
                }

                return _instance;
            }
        }

        private readonly Dictionary<string, ShuntingYard> s_yards = new Dictionary<string, ShuntingYard>();

        public ShuntingYard GetYards(string key)
        {
            return s_yards[key];
        }

        public void SetYards(string key, ShuntingYard shuntingYard)
        {
            s_yards[key] = shuntingYard;
        }
    }
}
