using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace _46_TestAnny
{
    public class DBConfigurationProvider : ConfigurationProvider, IDisposable
    {

        private bool isDisposed = false;
        private DBConfigOptions dBConfigOptions;
        private ReaderWriterLockSlim rwLocak = new ReaderWriterLockSlim();

        public DBConfigurationProvider(DBConfigOptions dBConfigOptions) {
            this.dBConfigOptions = dBConfigOptions;
            TimeSpan span = TimeSpan.FromSeconds(3);
            if (dBConfigOptions.ReloadInteral.HasValue)
                span = dBConfigOptions.ReloadInteral.Value;
            if (dBConfigOptions.ReloadOnChange)
            {
                ThreadPool.QueueUserWorkItem((obj) =>
                {
                    while (!isDisposed) {
                        Load();
                        Thread.Sleep(span);
                    }

                });
            }
        }

        public void Dispose()
        {
            this.isDisposed = true;
        }


        public override IEnumerable<string> GetChildKeys(IEnumerable<string> earlierKeys, string parentPath)
        {
            rwLocak.EnterReadLock();
            try
            {
                return base.GetChildKeys(earlierKeys, parentPath);
            }
            finally
            {
                rwLocak.ExitReadLock();
            }
        }

        public override void Load()
        {
            IDictionary<string, string> clonedData = null;
            try
            {
                rwLocak.EnterWriteLock();

                clonedData = this.Data.Clone();
                var tableName = dBConfigOptions.TableName;
                using (var conn = dBConfigOptions.CreateDBConnection()) {
                    conn.Open();
                    DoLoad(conn, tableName);
                }
            }
            catch
            {
                this.Data = clonedData;
                throw;
            }
            finally {
                rwLocak.ExitWriteLock();
            }
        }

        public void DoLoad(IDbConnection conn, string tableName)
        {
            using (var comm = conn.CreateCommand())
            {
                comm.CommandText = $"select Name,Value from {tableName} where ID in (select max(id) from {tableName} group by Name)";
                using (var reader = comm.ExecuteReader()) {
                    while (reader.Read()) {
                        string name = reader.GetString(0);
                        string value = reader.GetString(1);
                        if (value == null) {
                            this.Data[name] = value;
                            continue;
                        }
                        value = value.Trim();
                        if ((value.StartsWith("[") && value.EndsWith("]")) || (value.StartsWith("{") && value.EndsWith("}")))
                        {
                            TryLoadAsJson(name, value);

                        }
                        else {
                            this.Data[name] = value;
                        }
                    }

                }
            }
        }

        public void TryLoadAsJson(string name,string value) {
            var jsonOptions = new JsonDocumentOptions { AllowTrailingCommas=true, CommentHandling = JsonCommentHandling.Skip };//允许注释和尾随逗号
            try {
                var jsonEl = JsonDocument.Parse(value, jsonOptions).RootElement;
                LoadJsonElement(name, jsonEl);
            } catch {

                this.Data[name] = value;
            }
        
        }

        public void LoadJsonElement(string name,JsonElement jsonEl) {
            if (jsonEl.ValueKind == JsonValueKind.Array)
            {
                int index = 0;
                foreach (var el in  jsonEl.EnumerateArray()) {
                    name = name + ConfigurationPath.KeyDelimiter + index;
                    LoadJsonElement(name, el);
                    index++;
                }


            } else if (jsonEl.ValueKind == JsonValueKind.Object) {
                foreach (var el in jsonEl.EnumerateObject()) {
                    name = name + ConfigurationPath.KeyDelimiter + el.Name;
                    LoadJsonElement(name, el.Value);

                }

            }
            else {
                this.Data[name] = jsonEl.GetVaueForConfig();
            }
        
        }

        public override bool TryGet(string key, out string value)
        {
            rwLocak.EnterReadLock();
            try
            {
                return base.TryGet(key, out value);
            }
            finally
            {
                rwLocak.ExitReadLock();
            }
        }
    }
}
