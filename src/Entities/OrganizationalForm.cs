#region ENBREA - Copyright (C) 2023 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA
 *    
 *    Copyright (C) 2023 STÜBER SYSTEMS GmbH
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
    /// Organizational form (Organistationsform) in BBS-Planung (database table "ORG")
    /// </summary>
    public class OrganizationalForm
    {
        public string Code { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public static OrganizationalForm FromDb(DbDataReader reader)
        {
            return new OrganizationalForm
            {
                Id = reader.GetValue<int>("id"),
                Code = reader.GetValue<string>("ORG"),
                Name = reader.GetValue<string>("ART")
            };
        }
    }
}
