using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksInterface;
using TasksInterface.TermalTask;

namespace Hardness_lib
{
    public class HardnessKurkinCalc : IHardnessCalculator
    {
        //var austeniteHard = 150;
        //F = 0,  Ferrite
        //B = 1,  Beinate
        //M = 2,  Martensite
        //A = 3,  Austenite
        //TB = 4,  Tempered Beinate
        //TM = 5, Tempered Martensite
        enum ChemElement : int { C, Mn, Mo, Si, Cr, Ni, V, W, Al, Cu };

        public float Calc(DataTable phaseTable, DataTable chemTable, List<float> phaseValues)
        {
            var realHard = 0.0f;
            var phasesNames = phaseTable.AsEnumerable().Select(r => r.Field<string>("Фаза"));
            //var elNumbers = resultsTable.AsEnumerable().Select(r => r.Field<int>("Индекс")).ToList();
            //var objIndex = elNumbers.BinarySearch(objNumber);
            var chemDic = new Dictionary<string, float>();

            foreach (DataRow row in chemTable.Rows)
            {
                if (!chemDic.ContainsKey((string)row[0]))
                    chemDic.Add((string)row[0], (float)row[1]);
            }

            var counter = 0;
            foreach (var phaseName in phasesNames)
            {
                var pureHard = CalcPureHardness(chemDic, phaseName);

                //var phaseNumber = "p" + (counter + 1).ToString();
                //var phaseValue = (float)resultsTable.Rows[objIndex][phaseNumber];

                realHard += phaseValues[counter] * (pureHard / 100);
                counter++;
            }
            return realHard;
        }

        public float CalcPureHardness(Dictionary<string, float> chemValue, string phaseName)
        {
            if (phaseName == "F" || phaseName == "TB" || phaseName == "TM" || phaseName == "P")
                return 105 + (310 * chemValue[ChemElement.C.ToString()]) + (16 * chemValue[ChemElement.Mn.ToString()]) - (140 * chemValue[ChemElement.Mo.ToString()]); // чистый Феррит
            else if (phaseName == "B")
                return 195 + (136 * chemValue[ChemElement.C.ToString()]) + (29 * chemValue[ChemElement.Si.ToString()]) + (35 * chemValue[ChemElement.Cr.ToString()]) + (29 * chemValue[ChemElement.Ni.ToString()]) + (132 * chemValue[ChemElement.V.ToString()]) + (10 * chemValue[ChemElement.W.ToString()]) + (173 * chemValue[ChemElement.Al.ToString()]); // чистый Бейнит
            else if (phaseName == "A")
                return 150;
            else
                return 289 + (792 * chemValue[ChemElement.C.ToString()]) + (37 * chemValue[ChemElement.Si.ToString()]) + (15 * chemValue[ChemElement.W.ToString()]); // чистый Мартенсит
        }
    }
    
}
