using SimulationCore.HCCMElements;
using SimulationCore.MathTool;
using SimulationCore.MathTool.Distributions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RailCargo.HCCM.Input
{
    public class InputTimeTable
    {
        public InputTimeTable()
        {
            using (StreamReader reader = new StreamReader(@"\\Mac\\Home\\Desktop\\masterarbeit könig\\hcdes\\RailCargo\\HCCM\\Data\\TimeTable.csv"))
            {
                string line; 

                while ((line = reader.ReadLine()) != null)
                {
                    //Define pattern
                    Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

                    //Separating columns to array
                    string[] X = CSVParser.Split(line);

                    /* Do something with X */
                }
            }
        }
    }
}