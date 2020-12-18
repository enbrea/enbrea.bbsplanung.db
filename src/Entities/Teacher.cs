#region ENBREA - Copyright (C) 2020 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA
 *    
 *    Copyright (C) 2020 STÜBER SYSTEMS GmbH
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

using System;
using System.Data.Common;

namespace Enbrea.BbsPlanung.Db
{
    /// <summary>
    /// A teacher (Lehrer) in BBS-Planung (database table "LVUEV")
    /// </summary>
    public class Teacher
    {
        public DateTime? Birthdate { get; set; }
        public string Code { get; set; }
        public string Country { get; set; }
        public string Denomination { get; set; }
        public string OfficialTitle { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public Gender? Gender { get; set; }
        public int Id { get; set; }
        public string Lastname { get; set; }
        public DateTime? LeaveDate { get; set; }
        public string Locality { get; set; }
        public string Nationality { get; set; }
        public string Notes { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string PostalCode { get; set; }
        public DateTime? StartDate { get; set; }
        public string Street { get; set; }
        public string Type { get; set; }

        public static Teacher FromDb(DbDataReader reader)
        {
            return new Teacher
            {
                Id = reader.GetValue<int>("id"),
                Code = reader.GetValue<string>("NKURZ"),
                Lastname = reader.GetValue<string>("NNAME"),
                Firstname = reader.GetValue<string>("VNAME"),
                Birthdate = reader.GetValue<DateTime?>("GEBDAT"),
                Gender = reader.GetGenderValue("GESCHLECHT"),
                Street = reader.GetValue<string>("STR"),
                PostalCode = reader.GetValue<string>("PLZ"),
                Locality = reader.GetValue<string>("ORT"),
                Email = reader.GetValue<string>("ONLINE"),
                StartDate = reader.GetValue<DateTime?>("BEGINN"),
                LeaveDate = reader.GetValue<DateTime?>("ENDE"),
                Phone1 = reader.GetValue<string>("TEL1"),
                Phone2 = reader.GetValue<string>("TEL2"),
                Nationality = reader.GetValue<string>("STAAT"),
                Denomination = reader.GetValue<string>("KONF"),
                Type = reader.GetValue<string>("L_ART"),
                Notes = reader.GetValue<string>("BEM"),
                OfficialTitle = reader.GetValue<string>("ADBEZ")
            };
        }
    }
}
