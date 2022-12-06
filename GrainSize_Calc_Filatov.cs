using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainSize_lib
{
    public class GrainSize_Calc_Filatov
    {
        enum ChemElement : int { C, Mn, Mo, Si, Cr, Ni, V, W, Al, Cu, P, As, Ti };
        
        public float accumulatedTime = 0;

        public float CalcGrainSize(int objNumber, float q, float currentTime, DataTable chemTable, DataTable materialTable, DataTable resultsTable)
        {
            var a = (float)materialTable.Rows[0]["Предэкспоненциальный множитель А"]; // предэкспоненциальный множитель
            var n = (float)materialTable.Rows[0]["Временная экспонента"]; // временная экспонента
            var r = 8.314; // универсальная газовая постоянная

            var chemDic = new Dictionary<string, float>();
            foreach (DataRow row in chemTable.Rows)
            {
                if (!chemDic.ContainsKey((string)row[0]))
                    chemDic.Add((string)row[0], (float)row[1]);
            }

            var elNumbers = resultsTable.AsEnumerable().Select(x => x.Field<int>("Индекс")).ToList();
            var objIndex = elNumbers.BinarySearch(objNumber);
            var currentTempreture = (float)resultsTable.Rows[objIndex]["Температура"];

            var activEnergy = (float)CalcActivationEnergy(chemDic);
            if (q == -1)
            {
                q = (float)activEnergy;
            }
            var austenTemp = CalcAustenizationTempreture(chemDic);
            if (currentTempreture >= austenTemp)
            {
               accumulatedTime += currentTime;
            }
            else accumulatedTime = 0;

            var grainSize = a * Math.Exp(-q / (r * austenTemp)) * Math.Pow(accumulatedTime, n);
            return (float)grainSize;
        }

        public float CalcActivationEnergy(Dictionary<string, float> chemValue)
        {
            return 89098 + (3581 * chemValue[ChemElement.C.ToString()]) + (1211 * chemValue[ChemElement.Ni.ToString()]) + (1443 * chemValue[ChemElement.Cr.ToString()]) +
                (4031 * chemValue[ChemElement.Mo.ToString()]);
        }

        public double CalcAustenizationTempreture(Dictionary<string, float> chemValue)
        {
            var austenTemp = 910 - (203 * Math.Sqrt(chemValue[ChemElement.C.ToString()])) + (44.7 * chemValue[ChemElement.Si.ToString()]) - (15.2 * chemValue[ChemElement.Ni.ToString()])
               + (31.5 * chemValue[ChemElement.Mo.ToString()]) + (104 * chemValue[ChemElement.V.ToString()]) + (13.1 * chemValue[ChemElement.W.ToString()]) - (30 * chemValue[ChemElement.Mn.ToString()])
              + (11 * chemValue[ChemElement.Cr.ToString()]) + (20 * chemValue[ChemElement.Cu.ToString()]) - (700 * chemValue[ChemElement.P.ToString()]) - (400 * chemValue[ChemElement.Al.ToString()])
               - (120 * chemValue[ChemElement.As.ToString()]) - (400 * chemValue[ChemElement.Ti.ToString()]);
            return (float)austenTemp;
        }
    }
}
