using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using SmartDormitary.Data.Models;

namespace SmartDormitory.Tests.HelpersMethods
{
    public static class TestHelpers
    {
        public static User TestUser1()
        {
            return new User {Id = "00000000-0000-0000-0000-000000000001", Sensors = new List<Sensor>()};
        }

        public static User TestUser2()
        {
            return new User {Id = "00000000-0000-0000-0000-000000000002", Sensors = new List<Sensor>()};
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
            return new SensorType
            {
                Id = TestGuid(),
                Description = "TEst",
                MinRefreshTime = 60,
                MinAcceptableValue = 10,
                MaxAcceptableValue = 80,
                MeasurementType = "%",
                Tag = "Test"
            };
        }

        public static SensorData TestSensorData()
        {
            return new SensorData {Id = 1, Value = "123", Timestamp = new DateTime()};
        }

        public static Sensor TestPublicSensor()
        {
            return new Sensor
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
                SensorData = TestSensorData(),
                User = TestUser1()
            };
        }

        public static Sensor TestPublicSensorWithoutId()
        {
            return new Sensor
            {
                Name = "test",
                Description = "test",
                MaxAcceptableValue = 1,
                MinAcceptableValue = 0,
                IsPublic = true,
                TickOff = true,
                SensorTypeId = TestGuid(),
                SensorType = TestSensorType(),
                Latitude = 41.1231,
                Longitude = 41.1231,
                RefreshTime = 40,
                SensorData = new SensorData { Id = 1, Value = "123", Timestamp = new DateTime() },
                User = TestUser1()
            };
        }

        public static Sensor TestPrivateSensor()
        {
            return new Sensor
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
                SensorData = TestSensorData(),
                User = TestUser1()
            };
        }

        public static Mock<UserManager<User>> GetTestUserManager()
        {
            return new Mock<UserManager<User>>(
                new Mock<IUserStore<User>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<User>>().Object,
                new IUserValidator<User>[0],
                new IPasswordValidator<User>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<User>>>().Object);
        }
    }
}