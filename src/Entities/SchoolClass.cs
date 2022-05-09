#region ENBREA - Copyright (C) 2022 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA
 *    
 *    Copyright (C) 2022 STÜBER SYSTEMS GmbH
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
    /// A school class (Klasse) in BBS-Planung (database table "KUL")
    /// </summary>
    public class SchoolClass
    {
        public string Code { get; set; }
        public string CoordinationArea { get; set; }
        public int Id { get; set; }
        public string Notes { get; set; }
        public string Teacher { get; set; }

        public static SchoolClass FromDb(DbDataReader reader)
        {
            return new SchoolClass
            {
                Id = reader.GetValue<int>("id"),
                Code = reader.GetValue<string>("KL_NAME"),
                Teacher = reader.GetValue<string>("KL_LEHRER"),
                Notes = reader.GetValue<string>("BEM"),
                CoordinationArea = reader.GetValue<string>("KO") 
            };
        }
    }
}
