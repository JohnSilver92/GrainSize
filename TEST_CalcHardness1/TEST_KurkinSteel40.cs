using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Hardness_lib;
using System.Data;
using System.Collections.Generic;

namespace Test_Hardness.TESTS_Kurkin
{
    [TestClass]
    public class TEST_KurkinSteel40
    {
        [TestMethod]
        public void Test_Kurkin_Steel40()
        {
            List<float> list = new List<float>();
            DataTable phaseTable = new DataTable();
            DataTable chemTable = new DataTable();

            list.Add(3);
            list.Add(70);
            list.Add(17);
            list.Add(10);
            list.Add(0);
            list.Add(0);

            chemTable.Columns.Add(new DataColumn("Элемент", typeof(string)));
            chemTable.Columns.Add(new DataColumn("%", typeof(float)));
            DataRow chemRow1 = chemTable.NewRow();
            chemRow1["Элемент"] = "C";
            chemRow1["%"] = "0,44";
            chemTable.Rows.Add(chemRow1);

            DataRow chemRow2 = chemTable.NewRow();
            chemRow2["Элемент"] = "Si";
            chemRow2["%"] = "0,22";
            chemTable.Rows.Add(chemRow2);

            DataRow chemRow3 = chemTable.NewRow();
            chemRow3["Элемент"] = "Mn";
            chemRow3["%"] = "0,66";
            chemTable.Rows.Add(chemRow3);

            DataRow chemRow4 = chemTable.NewRow();
            chemRow4["Элемент"] = "Cr";
            chemRow4["%"] = "0,15";
            chemTable.Rows.Add(chemRow4);

            DataRow chemRow5 = chemTable.NewRow();
            chemRow5["Элемент"] = "Cu";
            chemRow5["%"] = "0,02";
            chemTable.Rows.Add(chemRow5);

            DataRow chemRow6 = chemTable.NewRow();
            chemRow6["Элемент"] = "Mo";
            chemRow6["%"] = "0,00";
            chemTable.Rows.Add(chemRow6);

            DataRow chemRow8 = chemTable.NewRow();
            chemRow8["Элемент"] = "Ni";
            chemRow8["%"] = "0,00";
            chemTable.Rows.Add(chemRow8);

            DataRow chemRow9 = chemTable.NewRow();
            chemRow9["Элемент"] = "V";
            chemRow9["%"] = "0,00";
            chemTable.Rows.Add(chemRow9);

            DataRow chemRow10 = chemTable.NewRow();
            chemRow10["Элемент"] = "W";
            chemRow10["%"] = "0,00";
            chemTable.Rows.Add(chemRow10);

            DataRow chemRow11 = chemTable.NewRow();
            chemRow11["Элемент"] = "Al";
            chemRow11["%"] = "0,00";
            chemTable.Rows.Add(chemRow11);


            phaseTable.Columns.Add(new DataColumn("Фаза"));
            phaseTable.Columns.Add(new DataColumn("Доля %"));

            DataRow phaseRow1 = phaseTable.NewRow();
            phaseRow1["Фаза"] = "F";
            phaseRow1["Доля %"] = "5";
            phaseTable.Rows.Add(phaseRow1);

            DataRow phaseRow2 = phaseTable.NewRow();
            phaseRow2["Фаза"] = "P";
            phaseRow2["Доля %"] = "70";
            phaseTable.Rows.Add(phaseRow2);

            DataRow phaseRow3 = phaseTable.NewRow();
            phaseRow3["Фаза"] = "B";
            phaseRow3["Доля %"] = "17";
            phaseTable.Rows.Add(phaseRow3);

            DataRow phaseRow4 = phaseTable.NewRow();
            phaseRow4["Фаза"] = "M";
            phaseRow4["Доля %"] = "10";
            phaseTable.Rows.Add(phaseRow4);

            var testCalc = new HardnessKurkinCalc();

            var result = testCalc.Calc(phaseTable, chemTable,list);

            Assert.AreEqual(Math.Round(result, 2), 293.79);
        }
    }
}
