using System.Data;

namespace IType
{
    public interface IPassangerDAL
    {
        DataTable SearchPassangerByName(string name1 , string name2, string name3);
    }
}
