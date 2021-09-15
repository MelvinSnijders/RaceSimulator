using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Section
    {
        public SectionTypes SectionType;
    }

    public enum SectionTypes { 
        Straight,
        LeftCorner,
        RightCorner,
        StartGrid,
        Finish
    }

}
