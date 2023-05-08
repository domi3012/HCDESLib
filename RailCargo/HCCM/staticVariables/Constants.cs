using System;
using System.Collections.Generic;

namespace RailCargo.HCCM.staticVariables
{
    public class Constants
    {
        //---------------------- integer values
        public static readonly int TIME_TO_WAIT_FOR_SILO = 2;

        public static Dictionary<string, int> trainCapacity = new Dictionary<string, int>
            { { "Fast", 5 }, { "Slow", 2 } };

        //---------------------- requests
        public static readonly string REQUEST_FOR_SILO = "REQUEST_FOR_SILO";
        public static string REQUEST_FOR_DEPARTURE_AREA = "REQUEST_FOR_DEPARTURE_AREA";
        public static string REQUEST_FOR_DEPARTURE = "REQUEST_FOR_DEPARTURE";
        public static string REQUEST_FOR_SORTING = "REQUEST_FOR_SORTING";
        public static string REQUEST_FOR_SILO_STATUS = "REQUEST_FOR_SILO_STATUS";

        //---------------------- activity
        public static readonly string ACTIVITY_WAITING_FOR_SILO = "ACTIVITY_WAITING_FOR_SILO";
        public static string ACTIVITY_DRIVE_TO_DEPARTURE_AREA = "ACTIVITY_DRIVE_TO_DEPARTURE_AREA";
        public static string ACTIVITY_WAGON_COLLECTION = "ACTIVITY_WAGON_COLLECTION";
        public static string ACTIVITY_WAITING_FOR_DEPARTURE = "ACTIVITY_WAITING_FOR_DEPARTURE";
        public static string ACTIVITY_TRAIN_DRIVE = "ACTIVITY_TRAIN_DRIVE";
        public static string ACTIVITY_WAITING_FOR_TRAIN_SELECTION_SILO = "ACTIVITY_WAITING_FOR_TRAIN_SELECTION_SILO";
        public static string ACTIVITY_WAITING_IN_SILO = "ACTIVITY_WAITING_IN_SILO";
        public static string ACTIVITY_SHUNTING_WAGON = "ACTIVITY_SHUNTING_WAGON";
        public static string ACTIVITY_WAITING_FOR_TRAIN_SELECTION_WAGON = "ACTIVITY_WAITING_FOR_TRAIN_SELECTION_WAGON";
        public static string ACTIVITY_WAITING_FOR_ALLOWANCE = "ACTIVITY_WAITING_FOR_ALLOWANCE";
        public static string ACTIVITY_TRAIN_PREPARATION = "ACTIVITY_TRAIN_PREPARATION";
        public static string ACTIVITY_SHUNTING_WAGONS = "ACTIVITY_SHUNTING_WAGONS";
        public static string ACTIVITY_TRAIN_WAITING_FOR_DEPARTURE = "ACTIVITY_TRAIN_WAITING_FOR_DEPARTURE";
    }
}