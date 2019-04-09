
namespace BellTest.Codes
{
    /// <summary>
    /// A wrapper around the BellCode class which obscures the definition of the bell signal, making only the name of the signal visible.  
    /// Used in the UI where the user must recall what the signal is from memory.
    /// </summary>
    public class HiddenCode
    {
        public BellCode Code { get; set; }

        public HiddenCode(BellCode realCode)
        {
            Code = realCode;
        }

        public override string ToString()
        {
            return Code.Name;
        }
    }
}
