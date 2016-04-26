using System;
using System.Collections.Generic;
using DTOs;
using IType;
using System.Text.RegularExpressions;
using DALFactory;
using System.Data;

namespace BLL
{
    public class PassangerBLL : IPassangerBLL
    {
        private IPassangerDAL objPassangerDAL = PassangerDALFactory.getPassangerDALObject();
        public List<PassangerDTO> SearchPassangerByName(string passangerName)
        {
            List<PassangerDTO> result = new List<PassangerDTO>();
            //RegexOptions options = RegexOptions.None;
            //Regex regex = new Regex("[ ]{2,}", options);
            //passangerName = regex.Replace(Regex.Replace(passangerName, "[^0-9a-zA-Z]+", " "), " ");
            if (String.IsNullOrEmpty(passangerName))
            {
                return result;
            }
            passangerName = Regex.Replace(passangerName, "[^0-9a-zA-Z]+", " ");
            string[] paramters = passangerName.Trim().Split(' ');
            string name1 = "", name2 = "", name3 = "";
            if (paramters.Length > 3 || paramters.Length < 1)
            {
                return result;
            }
            else if (paramters.Length == 3)
            {
                name2 = paramters[1] + "*";
                name3 = paramters[2] + "*";
            }
            else if (paramters.Length == 2)
            {
                name2 = paramters[1] + "*";
            }
            name1 = paramters[0] + "*";
            DataTable dt = objPassangerDAL.SearchPassangerByName(name1, name2, name3);
            if (dt != null && dt.Rows.Count >= 1)
            {
                foreach (DataRow row in dt.Rows)
                {
                    PassangerDTO passangerDto = new PassangerDTO();
                    passangerDto.Id = Guid.Parse(row["Id"].ToString());
                    passangerDto.FirstName = row["FirstName"].ToString();
                    passangerDto.MiddleName = row["MiddleName"].ToString();
                    passangerDto.LastName = row["LastName"].ToString();
                    passangerDto.Street = row["Address"].ToString();
                    passangerDto.City = row["City"].ToString();
                    passangerDto.State = row["State"].ToString();
                    passangerDto.Country = row["Country"].ToString();
                    passangerDto.PostalCode = row["PostalCode"].ToString();
                    passangerDto.Gender = row["Gender"].ToString();
                    passangerDto.DateOfBirth = DateTime.Parse(row["DateOfBirth"].ToString());
                    passangerDto.LastUpdatedDateTime = DateTime.Parse(row["LastUpdatedDateTime"].ToString());
                    result.Add(passangerDto);
                }
            }        
            return result;
        }
    }
}
