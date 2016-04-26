using IType;
using DAL;

namespace DALFactory
{
    public class PassangerDALFactory
    {
        public static IPassangerDAL getPassangerDALObject()
        {
            IPassangerDAL objMitchellClaimDAL = new PassangerDAL();
            return objMitchellClaimDAL;
        }
    }
}
