using SmartDormitary.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDormitory.Tests.HelpersMethods
{
    public static class TestHelpers
    {
        public static User TestUser1()
        {
            return new User() { Id = "00000000-0000-0000-0000-000000000001", Sensors = new List<Sensor>() };
        }

        public static User TestUser2()
        {
            return new User() { Id = "00000000-0000-0000-0000-000000000002", Sensors = new List<Sensor>() };
        }

        public static Guid TestGuid()
        {
            return Guid.Parse("00000000-0000-0000-0000-000000000000");
        }

        public static Guid TestGuid1()
        {
            return Guid.Parse("00000000-0000-0000-0000-000000000001");
        }

        public static Guid TestGuid2()
        {
            return Guid.Parse("00000000-0000-0000-0000-000000000002");
        }

        public static Guid TestGuid3()
        {
            return Guid.Parse("00000000-0000-0000-0000-000000000003");
        }

        public static SensorType TestSensorType()
        {
            return new SensorType() { Id = TestGuid() };
        }

        public static Sensor TestPublicSensor()
        {
            return new Sensor()
            {
                Name = "test",
                Description = "test",
                MaxAcceptableValue = 1,
                MinAcceptableValue = 0,
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsPublic = true,
                TickOff = true,
                SensorTypeId = TestGuid(),
                SensorType = TestSensorType(),
                Latitude = 41.1231,
                Longitude = 41.1231,
                RefreshTime = 40,
                SensorData = new SensorData() { Id = 1, Value = "123", Timestamp = new DateTime() },
                User = TestUser1()
            };
        }

        public static Sensor TestPrivateSensor()
        {
            return new Sensor()
            {
                Name = "test2",
                Description = "test2",
                MaxAcceptableValue = 1,
                MinAcceptableValue = 0,
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                IsPublic = false,
                TickOff = true,
                SensorTypeId = TestGuid(),
                SensorType = TestSensorType(),
                Latitude = 41.1231,
                Longitude = 41.1231,
                RefreshTime = 40,
                SensorData = new SensorData() { Id = 2, Value = "123", Timestamp = new DateTime() },
                User = TestUser1()
            };
        }
    }
}
