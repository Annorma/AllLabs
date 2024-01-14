using Lab_10.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Lab_10.DAL
{
    public class Repository<T> : IRepository<T> where T : new()
    {
        private readonly string _connectionString;
        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<T> Select(params Tuple<string, string, object, string>[] filters)
        {
            var list = new List<T>();
            Type type = typeof(T);
            if (type.GetCustomAttribute<DbTabAttribute>() == null)
            {
                return null;
            }

            var colProps = type.GetProperties().Where(p => p.GetCustomAttribute<DbColAttribute>() != null).ToList();
            var colPropsNames = colProps.Select(p => $"[{p.GetCustomAttribute<DbColAttribute>()?.Name ?? p.Name}]").ToList();
            var sqlQuery = $"SELECT {string.Join(",", colPropsNames)} FROM [dbo].[{type.Name}]";

            if (filters != null && filters.Length > 0)
            {
                sqlQuery += " WHERE ";
                foreach (var filter in filters)
                {
                    sqlQuery += $"{filter.Item1} {filter.Item2} @{filter.Item1} {filter.Item4}";
                }
            }

            using var dbConnection = new SqlConnection(_connectionString);
            var cmd = dbConnection.CreateCommand();
            cmd.CommandText = sqlQuery;
            cmd.CommandType = CommandType.Text;
            if (filters != null && filters.Length > 0)
            { 
                foreach (var filter in filters)
                {
                    cmd.Parameters.AddWithValue($"@{filter.Item1}", filter.Item3);
                }
            }

            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }

            var res = cmd.ExecuteReader();
            while (res.Read())
            {
                var obj = new T();
                int i = 0;
                colProps.ForEach(p => p.SetValue(obj, res[i++]));
                list.Add(obj);
            }
            return list;
        }

        public bool Insert(T entity)
        {
            try
            {
                Type type = typeof(T);
                if (type.GetCustomAttribute<DbTabAttribute>() == null)
                {
                    throw new Exception("Given type is not an entity");
                }
                var colProps = type.GetProperties().Where(p => p.GetCustomAttribute<DbColAttribute>() != null).ToList();
                var colsData = new Dictionary<string, object>();

                colProps.ForEach(col =>
                {
                    colsData.Add($"{col.GetCustomAttribute<DbColAttribute>()?.Name ?? col.Name}",
                    col.GetValue(entity));
                });

                using var dbConnection = new SqlConnection(_connectionString);
                var cmd = dbConnection.CreateCommand();
                cmd.CommandText = $"INSERT INTO [dbo].[{type.Name}] ({string.Join(",", colsData.Keys.Select(c => $"[{c}]"))}) " + $"VALUES ({string.Join(",", colsData.Keys.Select(c => $"@{c}"))})";
                cmd.CommandType = CommandType.Text;

                foreach (var kvpCol in colsData)
                {
                    cmd.Parameters.AddWithValue($"@{kvpCol.Key}", kvpCol.Value);
                }

                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }
                cmd.ExecuteNonQuery();
                
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }
        public bool Update(T entity)
        {
            try
            {
                Type type = typeof(T);
                if (type.GetCustomAttribute<DbTabAttribute>() == null)
                {
                    throw new Exception("Given type is not an entity");
                }
                var colProps = type.GetProperties().Where(p => p.GetCustomAttribute<DbColAttribute>() != null).ToList();
                var primaryKeyPropertyInfo = type.GetProperties().FirstOrDefault(p => p.GetCustomAttribute<DbPrimaryKeyAttribute>() != null);
                var colsData = new Dictionary<string, object>();

                colProps.ForEach(col =>
                {
                    colsData.Add($"{col.GetCustomAttribute<DbColAttribute>()?.Name ?? col.Name}",
                    col.GetValue(entity));
                });

                using var dbConnection = new SqlConnection(_connectionString);
                var cmd = dbConnection.CreateCommand();
                cmd.CommandText = $"UPDATE [dbo].[{type.Name}] SET ";

                foreach (var col in colsData)
                {
                    cmd.CommandText += $" {col.Key} = @{col.Key} ";
                    if (!Equals(colsData.Last(), col))
                    {
                        cmd.CommandText += ",";
                    }
                }

                cmd.CommandText += $" WHERE {primaryKeyPropertyInfo?.Name} = @{primaryKeyPropertyInfo?.Name}";
                cmd.CommandType = CommandType.Text;
                foreach (var kvpCol in colsData)
                {
                    cmd.Parameters.AddWithValue($"@{kvpCol.Key}", kvpCol.Value);
                }
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }
        public bool Delete(T entity)
        {
            try
            {
                Type type = typeof(T);
                if (type.GetCustomAttribute<DbTabAttribute>() == null)
                {
                    throw new Exception("Given type is not an entity");
                }
                var colProps = type.GetProperties().Where(p => p.GetCustomAttribute<DbColAttribute>() != null).ToList();
                var colsData = new Dictionary<string, object>();

                colProps.ForEach(col =>
                {
                    colsData.Add($"{col.GetCustomAttribute<DbColAttribute>()?.Name ?? col.Name}",
                    col.GetValue(entity));
                });

                using var dbConnection = new SqlConnection(_connectionString);
                var cmd = dbConnection.CreateCommand();
                cmd.CommandText = $"DELETE FROM [dbo].[{type.Name}] WHERE ";
                cmd.CommandType = CommandType.Text;
                int i = 0;

                foreach (var kvpCol in colsData)
                {
                    cmd.CommandText += $"{kvpCol.Key} = @{kvpCol.Key}";
                    if (i <= colsData.Count - 2)
                    {
                        cmd.CommandText += " AND ";
                    }
                    cmd.Parameters.AddWithValue($"@{kvpCol.Key}", kvpCol.Value);
                    ++i;
                }
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }
        public bool IsEntityExitsInDb(T entity)
        {
            try
            {
                Type type = entity.GetType();
                var primaryKeyPropertyInfo = type.GetProperties().FirstOrDefault(p => p.GetCustomAttribute<DbPrimaryKeyAttribute>() != null);
                var primaryKeyPropertyValue = primaryKeyPropertyInfo?.GetValue(entity);
                var sqlQuery = $"SELECT {primaryKeyPropertyInfo?.Name} FROM [dbo].[{type.Name}] " + $"WHERE {primaryKeyPropertyInfo?.Name} = @{primaryKeyPropertyInfo?.Name}";
                
                using var dbConnection = new SqlConnection(_connectionString);
                var cmd = dbConnection.CreateCommand();
                cmd.CommandText = sqlQuery;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue($"@{primaryKeyPropertyInfo?.Name}", primaryKeyPropertyValue);

                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }
                var res = cmd.ExecuteReader();
                return res.Read() && res.HasRows;
            }
            catch (SqlException)
            {
                return false;
            }
        }
    }
}
