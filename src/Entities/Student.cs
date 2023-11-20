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

using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Enbrea.BbsPlanung.Db
{
    /// <summary>
    /// A student (Schüler) in BBS-Planung (database table "SIL")
    /// </summary>
    public class Student
    {
        public uint? ApprenticeshipDuration { get; set; }
        public DateTime? ApprenticeshipEndDate { get; set; }
        public DateTime? ApprenticeshipStartDate { get; set; }
        public bool BAfoeG { get; set; }
        public DateTime? Birthdate { get; set; }
        public List<uint> Companies { get; set; } = new List<uint>();
        public List<StudentCourse> Courses { get; set; } = new List<StudentCourse>();
        public List<StudentCustodian> Custodians { get; set; } = new List<StudentCustodian>();
        public string Denomination { get; set; }
        public string EducationalProgram { get; set; }
        public string Email { get; set; }
        public DateTime? EnterDate { get; set; }
        public string Firstname { get; set; }
        public Gender? Gender { get; set; }
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Locality { get; set; }
        public string MaritalStatus { get; set; }
        public string Mobile { get; set; }
        public string Nationality { get; set; }
        public string NativeLanguage { get; set; }
        public string Notes { get; set; }
        public string NotesForCustodians { get; set; }
        public string Phone { get; set; }
        public string PlaceOfBirth { get; set; }
        public string PostalCode { get; set; }
        public string Region { get; set; }
        public bool Repeating { get; set; }
        public bool RequiredToAttendSchool { get; set; }
        public string SchoolClass { get; set; }
        public string Status { get; set; }
        public string Street { get; set; }
        public string StudentNo { get; set; }

        public static Student FromDb(DbDataReader reader)
        {
            // Read out student data
            var student = new Student
            {
                Id = reader.GetValue<int>("id"),
                SchoolClass = reader.GetValue<string>("KL_NAME"),
                Status = reader.GetValue<string>("STATUS"),
                Lastname = reader.GetValue<string>("NNAME"),
                Firstname = reader.GetValue<string>("VNAME"),
                Birthdate = reader.GetValue<DateTime?>("GEBDAT"),
                PlaceOfBirth = reader.GetValue<string>("GEBORT"),
                Street = reader.GetValue<string>("STR"),
                PostalCode = reader.GetValue<string>("PLZ"),
                Locality = reader.GetValue<string>("ORT"),
                Region = reader.GetValue<string>("LANDKREIS"),
                Phone = reader.GetValue<string>("TEL"),
                Mobile = reader.GetValue<string>("TEL_HANDY"),
                Email = reader.GetValue<string>("EMAIL"),
                Nationality = reader.GetValue<string>("STAAT"),
                Gender = reader.GetGenderValue("GESCHLECHT"),
                Denomination = reader.GetValue<string>("KONF"),
                MaritalStatus = reader.GetValue<string>("FAMSTAND"),
                StudentNo = reader.GetValue<string>("NR_SCHÜLER"),
                NativeLanguage = reader.GetValue<string>("N_DE"),
                Notes = reader.GetValue<string>("BEMERK"),
                EducationalProgram = reader.GetValue<string>("TAKLSTORG"),
                NotesForCustodians = reader.GetValue<string>("E_BEM"),
                EnterDate = reader.GetValue<DateTime?>("EINTR_DAT"),
                ApprenticeshipStartDate = reader.GetValue<DateTime?>("AUSB_BEGDAT"),
                ApprenticeshipEndDate = reader.GetValue<DateTime?>("A_ENDEDAT"),
                ApprenticeshipDuration = reader.GetValue<uint?>("A_DAUER"),
                Repeating = reader.GetBoolValue("WIEDERHOL"),
                RequiredToAttendSchool = reader.GetBoolValue("SCHULPFLICHT"),
                BAfoeG = reader.GetBoolValue("BAFOEG"),
            };

            // Add custodians
            for (uint i = 1; i < 1; i++)
            {
                if (!string.IsNullOrEmpty(reader.GetValue<string>($"E_NNAME{i}")))
                {
                    student.Custodians.Add(StudentCustodian.FromDb(reader, i));
                }
            }

            // Add company associations 
            for (uint i = 1; i < 3; i++)
            {
                var companyNo = reader.GetValue<uint?>((i == 1 ? $"BETRIEB_NR" : $"BETRIEB_NR{i}"));
                if (companyNo != null)
                {
                    student.Companies.Add((uint)companyNo);
                }
            }

            // Add course associations 
            for (uint i = 1; i < 14; i++)
            {
                if (!string.IsNullOrEmpty(reader.GetValue<string>($"K{i}")))
                {
                    student.Courses.Add(StudentCourse.FromDb(reader, i));
                }
            }

            return student;
        }
    }
}
