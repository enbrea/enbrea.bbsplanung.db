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
    /// Group of profession (Berufsfeld) in BBS-Planung (database table "BERUFSGRUPPEN")
    /// </summary>
    public class GroupOfProfession
    {
        public string Code { get; set; }
        public int Duration { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SchoolType { get; set; }

        public static GroupOfProfession FromDb(DbDataReader reader)
        {
            return new GroupOfProfession
            {
                Id = reader.GetValue<int>("id"),
                SchoolType = reader.GetValue<string>("SFO"),
                Code = reader.GetValue<string>("TAKURZ"),
                Name = reader.GetValue<string>("TALANG"),
                Duration = reader.GetValue<int>("Dauer")
            };
        }
    }
}
