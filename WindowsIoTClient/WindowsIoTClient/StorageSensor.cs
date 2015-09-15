﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;

namespace WindowsIoTClient
{
    public class SensorAccess
    {

        public class SensorData : TableEntity
        {
            public DateTime MeasurementTime { get; set; }
            public Double AccelerometerX { get; set; }
            public Double AccelerometerY { get; set; }
            public Double AccelerometerZ { get; set; }
            public Double GyroscopeX { get; set; }
            public Double GyroscopeY { get; set; }
            public Double GyroscopeZ { get; set; }
            public Double MagnetometerX { get; set; }
            public Double MagnetometerY { get; set; }
            public Double MagnetometerZ { get; set; }
            public Double HumidityRelHum { get; set; }
            public Double BarometerAmbPres { get; set; }
            public Double HumidityRelTemp { get; set; }
        }

        private string _accountName = ""; //Add your azure storage account name
        private string _key = "";	//Add your storage account primary access key
        private StorageCredentials credentials;
        private CloudStorageAccount storageAccount;
        private CloudTableClient tableClient;
        private CloudTable table;
        public SensorAccess()
        {
            credentials = new StorageCredentials(_accountName, _key);
            storageAccount = new CloudStorageAccount(credentials, true);
            tableClient = storageAccount.CreateCloudTableClient();
            table = tableClient.GetTableReference("SensorTagTable");
        }
        public async Task<SensorData> ReceiveData()
        {
            TableOperation retrieveSensorData = TableOperation.Retrieve<SensorData>("latest", "latest"); //"mygalileo", "2015-05-19T21:48:10.0000000Z")
            // Execute the retrieve operation.
            TableResult retrievedResult = await table.ExecuteAsync(retrieveSensorData);
            return (SensorData)retrievedResult.Result;

        }

    }
}
