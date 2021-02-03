#region ENBREA - Copyright (C) 2021 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA
 *    
 *    Copyright (C) 2021 STÜBER SYSTEMS GmbH
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
using System.Data.Odbc;
using System.Threading.Tasks;

namespace Enbrea.BbsPlanung.Db
{
    /// <summary>
    /// A reader class for BBS-Planung databases
    /// </summary>
    public sealed class BbsPlanungDbReader : IAsyncDisposable
    {
        private readonly DbConnection _dbConnection;
        private DbTransaction _dbTransaction;

        /// <summary>
        /// Initializes a new instance of the <see cref="BbsPlanungDbReader<T>"/> class.
        /// </summary>
        public BbsPlanungDbReader(string dbConnectionString)
        {
            _dbConnection = new OdbcConnection(dbConnectionString);
        }

        /// <summary>
        /// Returns back all companies (Betriebe) for a given school number
        /// </summary>
        /// <param name="schoolNo">School number</param>
        /// <returns>An async enumerator of <see cref="Company<T>"/> instances</returns>
        public async IAsyncEnumerable<Company> CompaniesAsync(int schoolNo)
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => Company.FromDb(reader)))
            {
                yield return entity;
            }

            void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    $"select id,  BETRIEB_NR, BETRSORT, BETRANR, BETRZUSATZ, BETRNAM1, BETRNAM2, BETRSTR, BETRPLZ, BETRORT, BETRTEL, BETRANSPR, " +
                    $"BETRFAX, BETRONLINE " +
                    $"from BETRIEBE where SNR=?";

                var dbParameter = dbCommand.CreateParameter();
                dbParameter.ParameterName = "@SNR";
                dbParameter.Value = schoolNo;

                dbCommand.Parameters.Add(dbParameter);
            }
        }

        /// <summary>
        /// Connects to the database and starts a transaction
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task ConnectAsync()
        {
            await _dbConnection.OpenAsync();
            _dbTransaction = await _dbConnection.BeginTransactionAsync();
        }

        /// <summary>
        /// Returns back all coordination areas (Koordinationsbereiche) for a given school number
        /// </summary>
        /// <param name="schoolNo">School number</param>
        /// <returns>An async enumerator of <see cref="CoordinationArea<T>"/> instances</returns>
        public async IAsyncEnumerable<CoordinationArea> CoordinationAreasAsync(int schoolNo)
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => CoordinationArea.FromDb(reader)))
            {
                yield return entity;
            }

            void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    $"select id, KO, Koordinator " +
                    $"from Koordinationsbereich where SNR=?";

                var dbParameter = dbCommand.CreateParameter();
                dbParameter.ParameterName = "@SNR";
                dbParameter.Value = schoolNo;

                dbCommand.Parameters.Add(dbParameter);
            }
        }

        /// <summary>
        /// Returns back all courses (Kurse) for a given school number
        /// </summary>
        /// <param name="schoolNo">School number</param>
        /// <returns>An async enumerator of <see cref="Course<T>"/> instances</returns>
        public async IAsyncEnumerable<Course> CoursesAsync(int schoolNo)
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => Course.FromDb(reader)))
            {
                yield return entity;
            }

            void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    $"select K_NAME, K_NR, K_LEHRER, KT1, KO " +
                    $"from KURSE where SNR=?";

                var dbParameter = dbCommand.CreateParameter();
                dbParameter.ParameterName = "@SNR";
                dbParameter.Value = schoolNo;

                dbCommand.Parameters.Add(dbParameter);
            }
        }

        /// <summary>
        /// Returns back all denominations (Konfessionen)
        /// </summary>
        /// <returns>An async enumerator of <see cref="Denomination<T>"/> instances</returns>
        public async IAsyncEnumerable<Denomination> DenominationsAsync()
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => Denomination.FromDb(reader)))
            {
                yield return entity;
            }

            void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    $"select id, KONF, Text " +
                    $"from KONFESSION";
            };
        }

        /// <summary>
        /// Closes the database connection
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task DisconnectAsync()
        {
            await _dbTransaction.CommitAsync();
            await _dbConnection.CloseAsync();
        }

        /// <summary>
        /// Asynchronously disposes the database connection.
        /// </summary>
        /// <returns>A task that represents the asynchronous dispose operation.</returns>
        public async ValueTask DisposeAsync()
        {
            await _dbConnection.DisposeAsync();
        }

        /// <summary>
        /// Returns back all educational programs (Bildungsgänge)
        /// </summary>
        /// <returns>An async enumerator of <see cref="EducationalProgram<T>"/> instances</returns>
        public async IAsyncEnumerable<EducationalProgram> EducationalProgramsAsync()
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => EducationalProgram.FromDb(reader)))
            {
                yield return entity;
            }

            static void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    $"select id, TAKLSTORG, SFO, TAKURZ, KLST, ORG, BERUF_M, BERUF_W " +
                    $"from BG";
            };
        }

        /// <summary>
        /// Returns back all fields of profession (Berufsfelder)
        /// </summary>
        /// <returns>An async enumerator of <see cref="FieldOfProfession<T>"/> instances</returns>
        public async IAsyncEnumerable<FieldOfProfession> FieldsOfProfessionAsync()
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => FieldOfProfession.FromDb(reader)))
            {
                yield return entity;
            }

            static void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    $"select BFELD, BFELD_TEXT " +
                    $"from BFELD";
            };
        }

        /// <summary>
        /// Returns back all groups of profession (Berufsgruppen) for a given school number
        /// </summary>
        /// <param name="schoolNo">School number</param>
        /// <returns>An async enumerator of <see cref="GroupOfProfession<T>"/> instances</returns>
        public async IAsyncEnumerable<GroupOfProfession> GroupsOfProfessionAsync(int schoolNo)
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => GroupOfProfession.FromDb(reader)))
            {
                yield return entity;
            }

            void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    $"select id, SFO, TAKURZ, TALANG, Dauer " +
                    $"from BERUFSGRUPPEN where SNR=?";

                var dbParameter = dbCommand.CreateParameter();
                dbParameter.ParameterName = "@SNR";
                dbParameter.Value = schoolNo;

                dbCommand.Parameters.Add(dbParameter);
            };
        }

        /// <summary>
        /// Returns back all nationalities (Staatsangehörigkeiten)
        /// </summary>
        /// <returns>An async enumerator of <see cref="Nationality<T>"/> instances</returns>
        public async IAsyncEnumerable<Nationality> NationalitiesAsync()
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => Nationality.FromDb(reader)))
            {
                yield return entity;
            }

            static void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    $"select id, Staat, Text " +
                    $"from STAATEN";
            };
        }

        /// <summary>
        /// Returns back all native languages (Muttersprachen) 
        /// </summary>
        /// <returns>An async enumerator of <see cref="NativeLanguage<T>"/> instances</returns>
        public async IAsyncEnumerable<NativeLanguage> NativeLanguagesAsync()
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => NativeLanguage.FromDb(reader)))
            {
                yield return entity;
            }

            static void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    $"select id, SCHL, TEXT1 " +
                    $"from SCHL_MUSPR";
            };
        }
        /// <summary>
        /// Returns back all organizational forms (Organistationsformen)
        /// </summary>
        /// <returns>An async enumerator of <see cref="OrganizationalForm<T>"/> instances</returns>
        public async IAsyncEnumerable<OrganizationalForm> OrganizationalFormsAsync()
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => OrganizationalForm.FromDb(reader)))
            {
                yield return entity;
            }

            static void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    $"select id, ORG, ART " +
                    $"from ORG";
            };
        }

        /// <summary>
        /// Returns back all regions (Landkreise)
        /// </summary>
        /// <returns>An async enumerator of <see cref="Region<T>"/> instances</returns>
        public async IAsyncEnumerable<Region> RegionsAsync()
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => Region.FromDb(reader)))
            {
                yield return entity;
            }

            static void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    $"select id, LANDKREIS_NR, LANDKREIS " +
                    $"from LANDKREIS";
            };
        }

        /// <summary>
        /// Returns back all classes (Klassen) for a given school number
        /// </summary>
        /// <param name="schoolNo">School number</param>
        /// <returns>An async enumerator of <see cref="SchoolClass<T>"/> instances</returns>
        public async IAsyncEnumerable<SchoolClass> SchoolClassesAsync(int schoolNo)
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => SchoolClass.FromDb(reader)))
            {
                yield return entity;
            }

            void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    $"select id, KL_NAME, KL_LEHRER, BEM, KO " +
                    $"from KUL where SNR=?";

                var dbParameter = dbCommand.CreateParameter();
                dbParameter.ParameterName = "@SNR";
                dbParameter.Value = schoolNo;

                dbCommand.Parameters.Add(dbParameter);
            }
        }

        /// <summary>
        /// Returns back all school types (Schulformen)
        /// </summary>
        /// <returns>An async enumerator of <see cref="SchoolType<T>"/> instances</returns>
        public async IAsyncEnumerable<SchoolType> SchoolTypesAsync()
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => SchoolType.FromDb(reader)))
            {
                yield return entity;
            }

            static void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    $"select SFO, SFOKURZ, SFOTEXT " +
                    $"from SFO";
            }
        }

        /// <summary>
        /// Returns back all students (Schüler) for a given school number
        /// </summary>
        /// <param name="schoolNo">School number</param>
        /// <returns>An async enumerator of <see cref="Student<T>"/> instances</returns>
        public async IAsyncEnumerable<Student> StudentsAsync(int schoolNo)
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => Student.FromDb(reader)))
            {
                yield return entity;
            }

            void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    $"select id, KL_NAME, STATUS, NNAME, VNAME, GEBDAT, GEBORT, STR, PLZ, ORT, TEL, TEL_HANDY, LANDKREIS, EMAIL, " +
                    $"GESCHLECHT, KONF, STAAT, FAMSTAND, N_DE, NR_SCHÜLER, BEMERK, TAKLSTORG, EINTR_DAT, AUSB_BEGDAT, A_ENDEDAT, " +
                    $"A_DAUER, WIEDERHOL, SCHULPFLICHT, BAFOEG, E_INSTITUTION, E_NNAME as E_NNAME1, E_VNAME as E_VNAME1, " +
                    $"E_ANREDE as E_ANREDE1, E_STR as E_STR1, E_PLZ as E_PLZ1, E_ORT as E_ORT1, E_LDK as E_LDK1, E_TEL as E_TEL1, " +
                    $"E_TEL_HANDY1, E_FAX as E_FAX1, E_EMAIL as E_EMAIL1, E_NNAME2, E_VNAME2, E_ANREDE2, E_STR2, E_PLZ2, E_ORT2, E_LDK2," +
                    $"E_TEL2, E_TEL_HANDY2, E_FAX2, E_EMAIL2, E_BEM, K1, K_NR1, K2, K_NR2, K3, K_NR3, K4, K_NR4, K5, K_NR5, K6, " +
                    $"K_NR6, K7, K_NR7, K8, K_NR8, K9, K_NR9, K10, K_NR10, K11, K_NR11, K12, K_NR12, K13, K_NR13, K14, K_NR14, " +
                    $"K15, K_NR15, BETRIEB_NR, BETRIEB_NR2, BETRIEB_NR3, BETRIEB_NR4 " +
                    $"from SIL where SNR=?";

                var dbParameter = dbCommand.CreateParameter();
                dbParameter.ParameterName = "@SNR";
                dbParameter.Value = schoolNo;

                dbCommand.Parameters.Add(dbParameter);
            }
        }

        /// <summary>
        /// Returns back all official titles for a teacher (Amts-/Dienstbezeichnung)
        /// </summary>
        /// <returns>An async enumerator of <see cref="TeacherOfficialTitle<T>"/> instances</returns>
        public async IAsyncEnumerable<TeacherOfficialTitle> TeacherOfficialTitlesAsync()
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => TeacherOfficialTitle.FromDb(reader)))
            {
                yield return entity;
            }

            static void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    $"select SFO, SFOKURZ, SFOTEXT " +
                    $"from ADBEZ";
            }
        }

        /// <summary>
        /// Returns back all teachers (Lehrer) for a given school number
        /// </summary>
        /// <param name="schoolNo">School number</param>
        /// <returns>An async enumerator of <see cref="Teacher<T>"/> instances</returns>
        public async IAsyncEnumerable<Teacher> TeachersAsync(int schoolNo)
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => Teacher.FromDb(reader)))
            {
                yield return entity;
            }

            void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText = 
                    $"select id, NNAME, VNAME, NKURZ, GEBDAT, GESCHLECHT, STAAT, KONF, STR, PLZ, ORT, TEL1, TEL2, FAX, ONLINE, KFZ, " +
                    $"BEGINN, ENDE, L_ART, BEM, ADBEZ " +
                    $"from LVUEL where SNR=?";

                var dbParameter = dbCommand.CreateParameter();
                dbParameter.ParameterName = "@SNR";
                dbParameter.Value = schoolNo;

                dbCommand.Parameters.Add(dbParameter);
            }
        }

        /// <summary>
        /// Returns back all teacher types (Lehrerarten)
        /// </summary>
        /// <returns>An async enumerator of <see cref="TeacherType<T>"/> instances</returns>
        public async IAsyncEnumerable<TeacherType> TeacherTypesAsync()
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => TeacherType.FromDb(reader)))
            {
                yield return entity;
            }

            static void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    $"select L_ART, L_ART_TEXT " +
                    $"from LEHRART";
            }
        }

        /// <summary>
        /// Opens the internal database connection, executes an SQL query and iterates over the result set.
        /// </summary>
        /// <typeparam name="TEntity">Enttiy type to be created</typeparam>
        /// <param name="setCommand">Action for initializing the sql command</param>
        /// <param name="createEntity">Action for creating a new TEntity instance</param>
        /// <returns>An async enumerator of TEntity instances</returns>
        private async IAsyncEnumerable<TEntity> EntitiesAsync<TEntity>(Action<DbCommand> setCommand, Func<DbDataReader, TEntity> createEntity)
        {
            using var dbCommand = _dbConnection.CreateCommand();

            dbCommand.Transaction = _dbTransaction;
            setCommand(dbCommand);

            using var reader = await dbCommand.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                yield return createEntity(reader);
            }
        }
    }
}
