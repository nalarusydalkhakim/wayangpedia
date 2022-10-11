using System;

namespace SatriaKelana
{
    [Serializable]
    public class AreaRecord
    {
        public int Index { get; set; }
        public int PlantIndex { get; set; }
        public Area.TimeConstraint Constraint { get; set; }
    }
}