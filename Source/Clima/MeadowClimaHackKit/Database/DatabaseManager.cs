﻿using Meadow;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;

namespace MeadowClimaHackKit.Database
{
    public class DatabaseManager
    {
        bool isConfigured = false;

        SQLiteConnection Database { get; set; }

        private static readonly Lazy<DatabaseManager> instance =
            new Lazy<DatabaseManager>(() => new DatabaseManager());
        public static DatabaseManager Instance => instance.Value;

        private DatabaseManager()
        {
            Initialize();
        }

        protected void Initialize()
        {
            var databasePath = Path.Combine(MeadowOS.FileSystem.DataDirectory, "ClimateReadings.db");
            Database = new SQLiteConnection(databasePath);

            Database.DropTable<TemperatureReading>();
            Console.WriteLine("ConfigureDatabase");
            Database.CreateTable<TemperatureReading>();
            Console.WriteLine("Table created");
            isConfigured = true;
        }

        public bool SaveReading(TemperatureReading temperature)
        {
            if (isConfigured == false)
            {
                Console.WriteLine("SaveUpdateReading: DB not ready");
                return false;
            }

            if (temperature == null)
            {
                Console.WriteLine("SaveUpdateReading: Conditions is null");
                return false;
            }

            Console.WriteLine("Saving climate reading to DB");

            Database.Insert(temperature);

            Console.WriteLine($"Successfully saved to database");

            return true;
        }

        public List<TemperatureReading> GetTemperatureReadings()
        {
            return Database.Table<TemperatureReading>().ToList();
        }
    }
}