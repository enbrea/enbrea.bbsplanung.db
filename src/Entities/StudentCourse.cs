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
    /// A course association for a <see cref="Student"/>
    /// </summary>
    public class StudentCourse
    {
        public string CourseName { get; set; }
        public int? CourseNo { get; set; }

        public static StudentCourse FromDb(DbDataReader reader, uint fieldPostfix)
        {
            return new StudentCourse()
            {
                CourseName = reader.GetValue<string>($"K{fieldPostfix}"),
                CourseNo = reader.GetValue<int?>($"K_NR{fieldPostfix}")
            };
        }

    }
}
