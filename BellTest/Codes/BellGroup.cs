using System;
using System.Collections.Generic;
using System.Linq;

namespace BellTest.Codes
{
    /// <summary>
    /// Defines a single group of strokes within a bell signal.  For example, the code 3-1 consists of two groups of three and one strokes respectively.
    /// </summary>
    public class BellGroup : IEquatable<BellGroup>
    {
        public List<BellStroke> Bells { get; set; }

        public BellGroup()
        {
            Bells = new List<BellStroke>();
        }

        public bool Equals(BellGroup other)
        {
            if (other == null)
            {
                return false;
            }
            if (Bells.Count != other.Bells.Count)
            {
                return false;
            }
            for (int i = 0; i < Bells.Count; ++i)
            {
                if (Bells[i] != other.Bells[i])
                {
                    return false;
                }
            }
            return true;
        }

        public override bool Equals(object other)
        {
            return Equals(other as BellGroup);
        }

        public override int GetHashCode()
        {
            // In use, in the vast majority of cases, there will be zero or one Hold strokes in the group.
            // It will be the last one, but the list is small enough that the order of checking doesn't matter that much.
            return Bells.Count * 2 + (Bells.Any(s => s == BellStroke.Hold) ? 1 : 0);
        }

        public static bool operator ==(BellGroup a, BellGroup b)
        {
            return ReferenceEquals(a, b) || a.Equals(b);
        }

        public static bool operator !=(BellGroup a, BellGroup b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return Bells.Count + (Bells[Bells.Count - 1] == BellStroke.Hold ? "*" : "");
        }
    }
}
