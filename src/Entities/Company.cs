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

using System.Data.Common;

namespace Enbrea.BbsPlanung.Db
{
    /// <summary>
    /// A company (Betrieb) in BBS-Planung (database table "BETRIEBE")
    /// </summary>
    public class Company
    {
        public uint CompanyNo { get; set; }
        public string Contact { get; set; }
        public int Id { get; set; }
        public string Locality { get; set; }
        public string Location { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Online { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public string Salutation { get; set; }
        public string Street { get; set; }
        public string Supplement { get; set; }
        public string Telefax { get; set; }

        public static Company FromDb(DbDataReader reader)
        {
            return new Company
            {
                Id = reader.GetValue<int>("id"),
                CompanyNo = reader.GetValue<uint>("BETRIEB_NR"),
                Location = reader.GetValue<string>("BETRSORT"),
                Salutation = reader.GetValue<string>("BETRANR"),
                Supplement = reader.GetValue<string>("BETRZUSATZ"),
                Name1 = reader.GetValue<string>("BETRNAM1"),
                Name2 = reader.GetValue<string>("BETRNAM2"),
                Street = reader.GetValue<string>("BETRSTR"),
                PostalCode = reader.GetValue<string>("BETRPLZ"),
                Locality = reader.GetValue<string>("BETRORT"),
                Phone = reader.GetValue<string>("BETRTEL"),
                Contact = reader.GetValue<string>("BETRANSPR"),
                Telefax = reader.GetValue<string>("BETRFAX"),
                Online = reader.GetValue<string>("BETRONLINE")
            };
        }
    }
}