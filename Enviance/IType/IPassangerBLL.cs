using System.Collections.Generic;
using DTOs;

namespace IType
{
    public interface IPassangerBLL
    {
        List<PassangerDTO> SearchPassangerByName(string passangerName);
    }
}
