﻿#region ENBREA - Copyright (C) 2023 STÜBER SYSTEMS GmbH
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
    /// School type (Schulform) in BBS-Planung (database table "SFO")
    /// </summary>
    public class SchoolType
    {
        public string Code { get; set; }
        public string LongName { get; set; }
        public string ShortName { get; set; }

        public static SchoolType FromDb(DbDataReader reader)
        {
            return new SchoolType
            {
                Code = reader.GetValue<string>("SFO"),
                ShortName = reader.GetValue<string>("SFOKURZ"),
                LongName = reader.GetValue<string>("SFOTEXT")
            };
        }
    }
}
