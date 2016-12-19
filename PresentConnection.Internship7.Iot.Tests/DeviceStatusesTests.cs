using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PresentConnection.Internship7.Iot.Domain;

namespace PresentConnection.Internship7.Iot.Tests
{
    [TestFixture]
    public class DeviceStatusesTests
    {
        private List<DeviceStatus> deviceStatuses;

        [SetUp]
        public void SetUp()
        {
            deviceStatuses = new List<DeviceStatus>();
        }

        [Test]
        public void Can_add_registered_to_first_place()
        {
            var validator = new DeviceStatusValidator(DeviceStatus.Registered);
            var results = validator.Validate(deviceStatuses);
            bool validationSucceeded = results.IsValid;
            if (validationSucceeded)
            {
                deviceStatuses.Add(DeviceStatus.Registered);
            }

            deviceStatuses.Last().ShouldEqual(DeviceStatus.Registered);
            deviceStatuses.Count.ShouldEqual(1);
        }

        [Test]
        public void Cant_add_connected_to_first_place()
        {
            var validator = new DeviceStatusValidator(DeviceStatus.Connected);
            var results = validator.Validate(deviceStatuses);
            bool validationSucceeded = results.IsValid;
            if (validationSucceeded)
            {
                deviceStatuses.Add(DeviceStatus.Connected);
            }
            deviceStatuses.Count.ShouldEqual(0);
        }

        [Test]
        public void Cant_add_disconnected_to_first_place()
        {
            var validator = new DeviceStatusValidator(DeviceStatus.Disconnected);
            var results = validator.Validate(deviceStatuses);
            bool validationSucceeded = results.IsValid;
            if (validationSucceeded)
            {
                deviceStatuses.Add(DeviceStatus.Disconnected);
            }

            deviceStatuses.Count.ShouldEqual(0);
        }

        [Test]
        public void Cant_add_unregistered_to_first_place()
        {
            var validator = new DeviceStatusValidator(DeviceStatus.Unregistered);
            var results = validator.Validate(deviceStatuses);
            bool validationSucceeded = results.IsValid;
            if (validationSucceeded)
            {
                deviceStatuses.Add(DeviceStatus.Unregistered);
            }

            deviceStatuses.Count.ShouldEqual(0);
        }

        [Test]
        public void Cant_add_status_on_top_of_the_same_status()
        {
            deviceStatuses.Add(DeviceStatus.Registered);
            var validator = new DeviceStatusValidator(DeviceStatus.Registered);
            var results = validator.Validate(deviceStatuses);
            bool validationSucceeded = results.IsValid;
            if (validationSucceeded)
            {
                deviceStatuses.Add(DeviceStatus.Registered);
            }
            deviceStatuses.Count.ShouldEqual(1);
        }

        [Test]
        public void Cant_add_status_registered_on_top_of_unregistered()
        {
            deviceStatuses.Add(DeviceStatus.Registered);
            deviceStatuses.Add(DeviceStatus.Unregistered);
            var validator = new DeviceStatusValidator(DeviceStatus.Registered);
            var results = validator.Validate(deviceStatuses);
            bool validationSucceeded = results.IsValid;
            if (validationSucceeded)
            {
                deviceStatuses.Add(DeviceStatus.Registered);
            }
            deviceStatuses.Count.ShouldEqual(3);
            deviceStatuses.Last().ShouldEqual(DeviceStatus.Registered);
        }

        [Test]
        public void Cant_add_status_connected_on_top_of_unregistered()
        {
            deviceStatuses.Add(DeviceStatus.Registered);
            deviceStatuses.Add(DeviceStatus.Unregistered);
            var validator = new DeviceStatusValidator(DeviceStatus.Connected);
            var results = validator.Validate(deviceStatuses);
            bool validationSucceeded = results.IsValid;
            if (validationSucceeded)
            {
                deviceStatuses.Add(DeviceStatus.Connected);
            }
            deviceStatuses.Count.ShouldEqual(2);
            deviceStatuses.Last().ShouldEqual(DeviceStatus.Unregistered);
        }

        [Test]
        public void Cant_add_status_disconnected_on_top_of_unregistered()
        {
            deviceStatuses.Add(DeviceStatus.Registered);
            deviceStatuses.Add(DeviceStatus.Unregistered);
            var validator = new DeviceStatusValidator(DeviceStatus.Disconnected);
            var results = validator.Validate(deviceStatuses);
            bool validationSucceeded = results.IsValid;
            if (validationSucceeded)
            {
                deviceStatuses.Add(DeviceStatus.Disconnected);
            }
            deviceStatuses.Count.ShouldEqual(2);
            deviceStatuses.Last().ShouldEqual(DeviceStatus.Unregistered);
        }

    }
}
