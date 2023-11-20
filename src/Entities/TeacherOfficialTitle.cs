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
    /// Official title of a teacher (Amts-/Dienstbezeichnung) in BBS-Planung (database table "ADBEZ")
    /// </summary>
    public class TeacherOfficialTitle
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public static TeacherOfficialTitle FromDb(DbDataReader reader)
        {
            return new TeacherOfficialTitle
            {
                Code = reader.GetValue<string>("ADBEZ"),
                Name = reader.GetValue<string>("ADLANG")
            };
        }
    }
}
