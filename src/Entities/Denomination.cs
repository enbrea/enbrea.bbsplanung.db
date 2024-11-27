#region Enbrea - Copyright (C) STÜBER SYSTEMS GmbH
/*    
 *    Enbrea
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
    /// Denomination (Konfession) in BBS-Planung (database table "KONFESSION")
    /// </summary>
    public class Denomination
    {
        public string Code { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public static Denomination FromDb(DbDataReader reader)
        {
            return new Denomination
            {
                Id = reader.GetValue<int>("id"),
                Code = reader.GetValue<string>("KONF"),
                Name = reader.GetValue<string>("Text") 
            };
        }
    }
}
