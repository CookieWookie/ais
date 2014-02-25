﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using AiS.Models;

namespace AiS.Repositories.Database.Sql
{
    public class StudyProgrammeRepository : BaseSqlDatabaseRepository<StudyProgramme>, IStudyProgrammeRepository
    {
        private const string SELECT = "SELECT [ID], [Name], [Length], [StudyType] FROM [StudyProgrammes]";
        private const string SELECT_SINGLE = SELECT + " WHERE [ID] = @id";
        private const string SELECT_BYTYPE = SELECT + " WHERE [StudyType] = @studyType;";
        private const string SAVE =
            "IF EXISTS(" + SELECT + " WHERE [ID] = @id) " +
            "UPDATE [StudyProgrammes] SET [Name] = @name, [Length] = @length, [StudyType] = @studyType WHERE [ID] = @id; " +
            "ELSE " +
            "INSERT INTO [StudyProgrammes] ([ID], [Name], [Length], [StudyType]) VALUES (@id, @name, @length, @studyType);";

        public StudyProgrammeRepository(string connectionString)
            : base(connectionString, SELECT_SINGLE, SELECT, SAVE)
        {
        }

        protected override StudyProgramme Create(System.Data.IDataReader reader)
        {
            return new StudyProgramme
            {
                ID = reader.GetString(0),
                Name = reader.GetString(1),
                Length = reader.GetInt32(2),
                StudyType = reader.GetInt32(3) == 0 ? StudyType.Bachelor : StudyType.Magister
            };
        }

        protected override void SetParameters(System.Data.IDbCommand command, StudyProgramme model)
        {
            command.Parameters.Add(CreateParameter("@id", model.ID));
            command.Parameters.Add(CreateParameter("@name", model.Name));
            command.Parameters.Add(CreateParameter("@length", model.Length));
            command.Parameters.Add(CreateParameter("@studyType", model.StudyType == StudyType.Bachelor ? 0 : 1));
        }

        public IEnumerable<StudyProgramme> GetByType(StudyType type)
        {
            return GetImpl(SELECT_BYTYPE, CreateParameter("@studyType", type == StudyType.Bachelor ? 0 : 1));
        }
    }
}
