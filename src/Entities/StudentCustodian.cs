#region ENBREA - Copyright (C) STÜBER SYSTEMS GmbH
/*    
 *    ENBREA
 *    
 *    Copyright (C) STÜBER SYSTEMS GmbH
 *
 *    This program is free software: you can redistribute it and/or modify
 *    it under the terms of the GNU Affero General Public License, version 3,
 *    as published by the Free Software Foundation.
 *
 *    This program is distributed in the hope that it will be useful,
 *    but WITHOUT ANY WARRANTY; without even the implied warranty of
 *    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *    GNU Affero General Public License for more details.
 *
 *    You should have received a copy of the GNU Affero General Public License
 *    along with this program. If not, see <http://www.gnu.org/licenses/>.
 */
#endregion

using System.Data.Common;

namespace Enbrea.BbsPlanung.Db
{
    /// <summary>
    /// A custodian (Sorgeberechtigte) association for a <see cref="Student"/>
    /// </summary>
    public class StudentCustodian
    {
        public string Email { get; set; }
        public string Fax { get; set; }
        public string FirstName { get; set; }
        public string Institution { get; set; }
        public string LastName { get; set; }
        public string Locality { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public string Region { get; set; }
        public string Salutation { get; set; }
        public string Street { get; set; }

        public static StudentCustodian FromDb(DbDataReader reader, uint fieldPostfix)
        {
            return new StudentCustodian()
            {
                Institution = fieldPostfix == 1 ? reader.GetValue<string>($"E_INSTITUTION") : null,
                LastName = reader.GetValue<string>($"E_NNAME{fieldPostfix}"),
                FirstName = reader.GetValue<string>($"E_VNAME{fieldPostfix}"),
                Salutation = reader.GetValue<string>($"E_ANREDE{fieldPostfix}"),
                Street = reader.GetValue<string>($"E_STR{fieldPostfix}"),
                PostalCode = reader.GetValue<string>($"E_PLZ{fieldPostfix}"),
                Locality = reader.GetValue<string>($"E_ORT{fieldPostfix}"),
                Region = reader.GetValue<string>($"E_LDK{fieldPostfix}"),
                Phone = reader.GetValue<string>($"E_TEL{fieldPostfix}"),
                Mobile = reader.GetValue<string>($"E_TEL_HANDY{fieldPostfix}"),
                Fax = reader.GetValue<string>($"E_FAX{fieldPostfix}"),
                Email = reader.GetValue<string>($"E_EMAIL{fieldPostfix}")
            };
        }
    }
}
