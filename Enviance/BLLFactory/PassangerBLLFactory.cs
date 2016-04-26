using IType;
using BLL;

namespace BLLFactory
{
    public class PassangerBLLFactory
    {
        public static IPassangerBLL getPassangerBLLObject()
        {
            IPassangerBLL objPassangerBLL = new PassangerBLL();
            return objPassangerBLL;
        }
    }
}
