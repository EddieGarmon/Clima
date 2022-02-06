﻿using System;
using Meadow.Units;
using SQLite;

namespace MeadowClimaHackKit.Database
{
    public class TemperatureReading
    {
        [PrimaryKey, AutoIncrement]
        public int? ID { get; set; }

        public DateTime DateTime { get; set; }

        public double? TemperatureCelcius
        {
            get => TemperatureValue?.Celsius;
            set => TemperatureValue = new Temperature(value.Value,
                Temperature.UnitType.Celsius);
        }

        [Ignore]
        public Temperature? TemperatureValue { get; set; }
    }
}