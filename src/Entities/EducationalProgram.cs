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
    /// Educational program (Bildungsgang) in BBS-Planung (database table "BG")
    /// </summary>
    public class EducationalProgram
    {
        /// <summary>
        /// Code (Kürzel)
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Field of profession (Berufsfeld)
        /// </summary>
        public string GroupOfProfession { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Organisational form (Organistaionsform)
        /// </summary>
        public string OrganisationalForm { get; set; }

        /// <summary>
        /// Profession feminine (Berufsbezeichnung feminin)
        /// </summary>
        public string ProfessionFeminine { get; set; }

        /// <summary>
        /// Profession masculine (Berufsbezeichnung maskulin)
        /// </summary>
        public string ProfessionMasculine { get; set; }

        /// <summary>
        /// Class level (Klassenstufe)
        /// </summary>
        public string SchoolClassLevel { get; set; }

        /// <summary>
        /// School type (Schulform)
        /// </summary>
        public string SchoolType { get; set; }

        public static EducationalProgram FromDb(DbDataReader reader)
        {
            return new EducationalProgram
            {
                Id = reader.GetValue<int>("id"),
                Code = reader.GetValue<string>("TAKLSTORG"),
                SchoolType = reader.GetValue<string>("SFO"),
                GroupOfProfession = reader.GetValue<string>("TAKURZ"),
                SchoolClassLevel = reader.GetValue<string>("KLST"),
                OrganisationalForm = reader.GetValue<string>("ORG"),
                ProfessionMasculine = reader.GetValue<string>("BERUF_M"),
                ProfessionFeminine = reader.GetValue<string>("BERUF_W")
            };
        }
    }
}
