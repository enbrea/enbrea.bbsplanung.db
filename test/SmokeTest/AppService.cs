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

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Enbrea.BbsPlanung.Db.SmokeTest
{
    public class AppService
    {
        private readonly AppConfig _appConfig;
        
        public AppService(AppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("BBS-PLANUNG Export Test");
            Console.WriteLine("-----------------------\n");

            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine("\t1  - Export Companies (Betriebe)");
            Console.WriteLine("\t2  - Export Coordination Areas (Koordinierungsbereiche)");
            Console.WriteLine("\t3  - Export Courses (Kurse)");
            Console.WriteLine("\t4  - Export Denominations (Konfessionen)");
            Console.WriteLine("\t5  - Export Educational Programs (Bildungsgänge)");
            Console.WriteLine("\t6  - Export Fields Of Profession (Berufsfelder)");
            Console.WriteLine("\t7  - Export Groups Of Profession (Berufsgruppen)");
            Console.WriteLine("\t8  - Export Nationalities (Staatsangehörigkeiten)");
            Console.WriteLine("\t9  - Export Native Languages (Muttersprachen)");
            Console.WriteLine("\t10 - Export Organizational Forms (Organisationsformen)");
            Console.WriteLine("\t11 - Export Regions (Landkreise)");
            Console.WriteLine("\t12 - Export School Classes (Klassen)");
            Console.WriteLine("\t13 - Export School Types (Schulformen)");
            Console.WriteLine("\t14 - Export Students (Schüler)");
            Console.WriteLine("\t15 - Export Teachers (Lehrer)");
            Console.WriteLine("\t16 - Export Teacher Official Titles (Amts-/Dienstbezeichnungen)");
            Console.WriteLine("\t17 - Export Teacher Types  (Lehrerarten)");
            Console.Write("Your selection? ");

            switch (Console.ReadLine())
            {
                case "1":
                    await ExportCompanies(cancellationToken);
                    break;
                case "5":
                    await ExportEducationalPrograms(cancellationToken);
                    break;
                case "12":
                    await ExportSchoolClasses(cancellationToken);
                    break;
                case "14":
                    await ExportStudents(cancellationToken);
                    break;
                case "15":
                    await ExportTeachers(cancellationToken);
                    break;
            }
        }

        private BbsPlanungDbReader CreateDbReader()
        {
            return new BbsPlanungDbReader(_appConfig.DbConnection);
        }

        private async Task ExportCompanies(CancellationToken cancellationToken)
        {
            await using var dbReader = CreateDbReader();

            await dbReader.ConnectAsync(cancellationToken);

            Console.WriteLine();
            Console.WriteLine("Companies:");
            Console.WriteLine("---------");

            await foreach (var company in dbReader.CompaniesAsync(_appConfig.SchoolNo, cancellationToken))
            {
                Console.WriteLine(@"{0}, {1}, {2}, {3}, {4}", 
                    company.CompanyNo, 
                    company.Name1, 
                    company.Name2, 
                    company.Locality, 
                    company.Contact);
            }

            await dbReader.DisconnectAsync();
        }

        private async Task ExportEducationalPrograms(CancellationToken cancellationToken)
        {
            await using var dbReader = CreateDbReader();

            await dbReader.ConnectAsync(cancellationToken);

            Console.WriteLine();
            Console.WriteLine("Educational Programs:");
            Console.WriteLine("---------------------");

            await foreach (var educationalProgram in dbReader.EducationalProgramsAsync(cancellationToken))
            {
                Console.WriteLine(@"{0}, {1}, {2}, {3}, {4}, {5}, {6}", 
                    educationalProgram.Code, 
                    educationalProgram.GroupOfProfession, 
                    educationalProgram.OrganisationalForm,
                    educationalProgram.SchoolType,
                    educationalProgram.SchoolClassLevel,
                    educationalProgram.ProfessionFeminine, 
                    educationalProgram.ProfessionMasculine);
            }

            await dbReader.DisconnectAsync();
        }

        private async Task ExportSchoolClasses(CancellationToken cancellationToken)
        {
            await using var dbReader = CreateDbReader();

            await dbReader.ConnectAsync(cancellationToken);

            Console.WriteLine();
            Console.WriteLine("School Classes:");
            Console.WriteLine("---------------");

            await foreach (var schoolClass in dbReader.SchoolClassesAsync(_appConfig.SchoolNo, cancellationToken))
            {
                Console.WriteLine(@"{0}, {1}, {2}, {3}", 
                    schoolClass.Code, 
                    schoolClass.Teacher, 
                    schoolClass.CoordinationArea, 
                    schoolClass.Notes);
            }

            await dbReader.DisconnectAsync();
        }

        private async Task ExportStudents(CancellationToken cancellationToken)
        {
            await using var dbReader = CreateDbReader();

            await dbReader.ConnectAsync(cancellationToken);

            Console.WriteLine();
            Console.WriteLine("Students:");
            Console.WriteLine("---------");

            await foreach (var student in dbReader.StudentsAsync(_appConfig.SchoolNo, cancellationToken))
            {
                Console.WriteLine(@"{0}, {1}, {2}, {3}, {4}, {5}", 
                    student.Lastname, 
                    student.Firstname,
                    student.Gender.ToString(),
                    student.Birthdate?.ToString("dd.MM.yyyy"), 
                    student.Locality, 
                    student.SchoolClass);
            }

            await dbReader.DisconnectAsync();
        }

        private async Task ExportTeachers(CancellationToken cancellationToken)
        {
            await using var dbReader = CreateDbReader();

            await dbReader.ConnectAsync(cancellationToken);

            Console.WriteLine();
            Console.WriteLine("Teachers:");
            Console.WriteLine("---------");

            await foreach (var teacher in dbReader.TeachersAsync(_appConfig.SchoolNo, cancellationToken))
            {
                Console.WriteLine(@"{0}, {1}, {2}, {3}, {4}", 
                    teacher.Lastname, 
                    teacher.Firstname,
                    teacher.Gender.ToString(),
                    teacher.Birthdate?.ToString("dd.MM.yyyy"), 
                    teacher.Locality);
            }

            await dbReader.DisconnectAsync();
        }
    }
}
