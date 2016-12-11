using System.Collections;
using CodeMash.Net;
using System.Collections.Generic;
using System.Linq;

namespace PresentConnection.Internship7.Iot.Domain
{
    [CollectionName(Statics.Collections.ClientDevices)]
    public class ClientDevice : EntityBase, IEntityWithSensitiveData
    {
        public ClientDevice()
        {
            Rules = new Dictionary<string, object>();
            Commands = new Dictionary<string, object>();
            Components = new Dictionary<string, object>();
            PowerResource = new PowerResource();
            SimulationType = SimulationType.NotSet;
            DeviceStatuses = new List<DeviceStatus>(); // { DeviceStatus.Registered };
        }

        public string ClientId { get; set; }
        public string DeviceId { get; set; }
        public string DeviceDisplayId { get; set; }

        // when user registers device but doesn't do real action yet
        public bool IsEnabled
        {
            get { return DeviceStatuses.Last() != DeviceStatus.Unregistered; }
        }

        // when user establishes connection from device
        public DeviceStatus IsConnected
        {
            get { return DeviceStatuses.Last(); }
        }
        public List<DeviceStatus> DeviceStatuses { get; set; }
        public PowerResource PowerResource { get; set; }// see below description
        public string SerialNumber { get; set; }
        public string FirmwareVersion { get; set; }// GetDefaultVersion From Device
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Dictionary<string, object> Rules { get; set; } // e.g. component unique name, json
        public Dictionary<string, object> Commands { get; set; } // e.g. component unique name, json
        public Dictionary<string, object> Components { get; set; } // e.g. component unique name, json conf
        public string AuthKey1 { get; set; }
        public string AuthKey2 { get; set; }
        public SimulationType SimulationType { get; set; }
        public bool IsSimulationDevice { get; set; }
        public string CreatedBy { get; set; }
    }

    //    public class DeviceStatusList<T> : List<T> where T : IDeviceStatus
    //    {
    //        private T data;
    //
    //        public new T this[int index]
    //        {
    //            get { return data; }
    //            set
    //            {
    ////                throw new System.NotImplementedException(); 
    //                
    //                if (value == DeviceStatus.Registered)
    //                {
    //                    
    //                }
    //            }
    //        }
    //
    //    }

    public class DeviceStatusList<T> : List<T>
    {
        // The nested class is also generic on T.
        private class Node
        {
            // T used in non-generic constructor.
            public Node(T t)
            {
                next = null;
                data = t;
            }

//            public T this[int index] { get { return data; } set { value = data; } }

            private Node next;
            public Node Next
            {
                get { return next; }
                set { next = value; }
            }

            // T as private member data type.
            private T data;

            // T as return type of property.
            public T Data
            {
                get { return data; }
                set { data = value; }
            }
        }

        private Node head;

        // constructor
        public DeviceStatusList()
        {
            head = null;
        }

        // T as method parameter type:
        public new void Add(T t)
        {
            Node n = new Node(t);
            n.Next = head;
            head = n;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node current = head;

            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
    }
}
