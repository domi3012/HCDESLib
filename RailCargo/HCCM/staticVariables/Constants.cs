using System;

namespace RailCargo.HCCM.staticVariables
{
    public class Constants
    {
        //---------------------- integer values
        public static readonly int TIME_TO_WAIT_FOR_SILO = 2;
        public static int CAPACITY_SILO = 2;
        
        //---------------------- requests
        public static readonly string REQUEST_FOR_SILO = "REQUEST_FOR_SILO";
        public static string REQUEST_FOR_DEPARTURE_AREA = "REQUEST_FOR_DEPARTURE_AREA";
        public static string REQUEST_FOR_DEPARTURE = "REQUEST_FOR_DEPARTURE";
        public static string REQUEST_FOR_SORTING = "REQUEST_FOR_SORTING";
        
        //---------------------- activity
        public static readonly string ACTIVITY_WAITING_FOR_SILO = "ACTIVITY_WAITING_FOR_SILO";
        public static string ACTIVITY_DRIVE_TO_DEPARTURE_AREA = "ACTIVITY_DRIVE_TO_DEPARTURE_AREA";
        public static string ACTIVITY_WAGON_COLLECTION = "ACTIVITY_WAGON_COLLECTION";
        public static string ACTIVITY_WAITING_FOR_DEPARTURE = "ACTIVITY_WAITING_FOR_DEPARTURE";
        public static string ACTIVITY_TRAIN_DRIVE = "ACTIVITY_TRAIN_DRIVE";
        public static string ACTIVITY_WAITING_FOR_TRAIN_SELECTION_SILO = "ACTIVITY_WAITING_FOR_TRAIN_SELECTION_SILO";
        public static string ACTIVITY_WAITING_IN_SILO = "ACTIVITY_WAITING_IN_SILO";
    }
}