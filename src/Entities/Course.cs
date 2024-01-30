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
    /// A course (Kurs) in BBS-Planung (database table "KURSE")
    /// </summary>
    public class Course
    {
        public string CoordinationArea { get; set; }
        public int CourseNo { get; set; }
        public string Name { get; set; }
        public string Teacher { get; set; }
        public string Topic { get; set; }

        public static Course FromDb(DbDataReader reader)
        {
            return new Course
            {
                Name = reader.GetValue<string>("K_NAME"),
                CourseNo = reader.GetValue<int>("K_NR"),
                Teacher = reader.GetValue<string>("K_LEHRER"),
                Topic = reader.GetValue<string>("KT1"),
                CoordinationArea = reader.GetValue<string>("KO")
            };
        }
    }
}
