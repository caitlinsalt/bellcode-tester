using System;
using System.Collections.Generic;
using System.Linq;

namespace BellTest.Codes
{
    /// <summary>
    /// Defines a bell signal
    /// </summary>
    public class BellCode : IEquatable<BellCode>
    {
        public string Name { get; set; }
        public List<BellGroup> BellGroups { get; set; }

        public BellCode()
        {
            BellGroups = new List<BellGroup>();
        }

        /// <summary>
        /// Property is true if the final stroke of the signal is held down.  Equal to IsTokenRelease || IsSwitchingRelease.
        /// </summary>
        public bool IsRelease
        {
            get
            {
                if (BellGroups == null || BellGroups.Count == 0)
                {
                    return false;
                }
                BellGroup finalGroup = BellGroups[BellGroups.Count - 1];
                if (finalGroup == null || finalGroup.Bells == null || finalGroup.Bells.Count == 0)
                {
                    return false;
                }
                return (finalGroup.Bells[finalGroup.Bells.Count - 1] == BellStroke.Hold);
            }
        }

        private bool _switchingRelease;

        /// <summary>
        /// Property is true if the final stroke of the signal is held down to enable the other signalman to operate a switching lever.  Equal to IsRelease &amp;&amp; !IsTokenRelease.
        /// </summary>
        public bool IsSwitchingRelease
        {
            get { return IsRelease && _switchingRelease; }
            set
            {
                if (IsRelease)
                {
                    _switchingRelease = value;
                }
            }
        }

        /// <summary>
        /// Property is true if the final stroke of the signal is held down to enable the other signalman to withdraw a token.  Equal to IsRelease &amp;&amp; !IsSwitchingRelease.
        /// </summary>
        public bool IsTokenRelease
        {
            get { return IsRelease && !_switchingRelease; }
        }

        /// <summary>
        /// Property is true if and only if the enteredCode parameter is the same signal as this code, apart from that the final bell stroke is missing from enteredCode.
        /// In other words, if this signal was 3-1 and enteredCode was 3, or if this signal was 3-3-3 and enteredCode was 3-3-2, it would be true.
        /// </summary>
        /// <param name="enteredCode"></param>
        /// <returns></returns>
        public bool FinalBellPending(BellCode enteredCode)
        {
            if (enteredCode == null)
            {
                return false;
            }
            if (BellGroups.Count > enteredCode.BellGroups.Count + 1)
            {
                return false;
            }
            for (int i = 0; i < BellGroups.Count - 1; ++i)
            {
                if (BellGroups[i] != enteredCode.BellGroups[i])
                {
                    return false;
                }
            }
            if (enteredCode.BellGroups.Count < BellGroups.Count)
            {
                return BellGroups[BellGroups.Count - 1].Bells.Count == 1;
            }
            if (BellGroups[BellGroups.Count - 1].Bells.Count != enteredCode.BellGroups[BellGroups.Count - 1].Bells.Count + 1)
            {
                return false;
            }
            for (int i = 0; i < BellGroups[BellGroups.Count - 1].Bells.Count - 1; ++i)
            {
                if (BellGroups[BellGroups.Count - 1].Bells[i] != enteredCode.BellGroups[BellGroups.Count - 1].Bells[i])
                {
                    return false;
                }
            }
            return true;
        }

        public bool Equals(BellCode other)
        {
            if (other == null)
            {
                return false;
            }
            if (BellGroups.Count != other.BellGroups.Count)
            {
                return false;
            }
            for (int i = 0; i < BellGroups.Count; ++i)
            {
                if (BellGroups[i] != other.BellGroups[i])
                {
                    return false;
                }
            }
            return true;
        }

        public override bool Equals(object other)
        {
            return Equals(other as BellCode);
        }

        public override int GetHashCode()
        {
            // There will generally only ever be a small number of groups in each code - I have seen four groups in use, and theoretically you could have five.
            // The "what if there's more than 15" code path is provided just in case.
            int hash = 0;
            if (BellGroups.Count < 16)
            {
                foreach (int groupHash in BellGroups.Select(g => g.GetHashCode()))
                {
                    hash *= 2;
                    hash += groupHash;
                }
            }
            else
            {
                foreach (int groupHash in BellGroups.Select(g => g.GetHashCode()))
                {
                    hash ^= groupHash;
                }
            }
            return hash ^ Name.GetHashCode();
        }

        public static bool operator ==(BellCode a, BellCode b)
        {
            return ReferenceEquals(a, b) || a.Equals(b);
        }

        public static bool operator !=(BellCode a, BellCode b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return string.Join("-", BellGroups.Select(g => g.ToString()).ToArray());
        }
    }
}
