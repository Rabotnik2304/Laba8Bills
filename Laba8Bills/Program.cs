using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Laba8Bills
{
    class Program
    {   
        static List<TransfersData> ListOfTransfers = new List<TransfersData>();
        static int InitialMoney = int.Parse(File.ReadAllLines(".Bills.txt")[0]);

        static void Main()
        {
            Insert();// - Для удобства проверки создаёт нужный файлик
            BillsDataRecording();
            MoneyOnTheBill();
        }

        private static void MoneyOnTheBill()
        {
            
            TransfersData[] sortedArrayOfTransfers = ListOfTransfers.ToArray();
            Array.Sort(sortedArrayOfTransfers, new TimeComparer());

            if (sortedArrayOfTransfers[0].Operation == "revert")
            {
                Console.WriteLine("Data is incorrect");
            }
            else
            {
                ResultMoneyOnTheBill(sortedArrayOfTransfers);
            }

        }

        private static void ResultMoneyOnTheBill(TransfersData[] sortedListOfTransfers)
        {
            int dateOfVerification = DateOfVerification(sortedListOfTransfers);

            int moneyOnBill = InitialMoney;
            int moneyOnBillStepBefore = InitialMoney;

            for (int i = 0; i < sortedListOfTransfers.Length; i++)
            {
                moneyOnBill = Operation(sortedListOfTransfers, moneyOnBill, moneyOnBillStepBefore, i);

                if (sortedListOfTransfers[i].Time > dateOfVerification)
                {
                    Console.WriteLine(moneyOnBillStepBefore);
                    break;
                }
                else if (sortedListOfTransfers[i].Time == dateOfVerification)
                {
                    Console.WriteLine(moneyOnBill);
                    break;
                }

                if (moneyOnBill < 0)
                {
                    Console.WriteLine("Data is incorrect");
                    break;
                }

                moneyOnBillStepBefore = moneyOnBill;
            }

            if (sortedListOfTransfers[sortedListOfTransfers.Length - 1].Time < dateOfVerification)
            {
                Console.WriteLine(moneyOnBill);
            }
        }

        private static int Operation(TransfersData[] sortedListOfTransfers, int moneyOnBill, int moneyOnBillStepBefore, int i)
        {
            if (sortedListOfTransfers[i].Operation == "in")
            {
                moneyOnBill += sortedListOfTransfers[i].Money;
            }
            else if (sortedListOfTransfers[i].Operation == "out")
            {
                moneyOnBill -= sortedListOfTransfers[i].Money;
            }
            else if ((sortedListOfTransfers[i].Operation == "revert"))
            {
                moneyOnBill = moneyOnBillStepBefore;
            }

            return moneyOnBill;
        }

        private static int DateOfVerification(TransfersData[] sortedListOfTransfers)
        {
            int dateOfVerification;
            if (sortedListOfTransfers[0].DateOfVerification != "nothing")
            {
                dateOfVerification = YearsToMin(sortedListOfTransfers[0].DateOfVerification);
            }
            else
            {
                dateOfVerification = sortedListOfTransfers[sortedListOfTransfers.Length - 1].Time + 1;
            }

            return dateOfVerification;
        }
       
        static void BillsDataRecording()
        {
            string[] strings = File.ReadAllLines(".Bills.txt");

            string dateOfVerification = File.ReadAllLines(".Bills.txt")[File.ReadAllLines(".Bills.txt").Length - 1];
            int s = 1;
            //если в конце нет даты проверки, то мы приравниваем её значение к "nothing"
            // и приравниваем счётчик s к 0, чтобы пройти до конца файла
            if (dateOfVerification.Split('|').Length > 1) 
            {
                dateOfVerification = "nothing";
                s = 0;
            }

            for (int i = 1; i < strings.Length-s; i++)
            {
                TransfersData transfer = new TransfersData();

                string[] arrayOfTransfersData = strings[i].Split("|");


                transfer.Time = YearsToMin(arrayOfTransfersData[0]);
                transfer.Money = int.Parse(arrayOfTransfersData[1]);
                transfer.Operation = arrayOfTransfersData[2].Trim(' ');
                transfer.DateOfVerification = dateOfVerification;

                ListOfTransfers.Add(transfer);
            }
        }

        private static int YearsToMin(string arrayOfTransfersData)
        {
            string date = arrayOfTransfersData.Split(' ')[0];
            string time = arrayOfTransfersData.Split(' ')[1];

            int timeInMinutes = (int.Parse(date.Split('-')[0]) - 2000) * 365 * 24 * 60 +
                                int.Parse(date.Split('-')[1]) * 31 * 24 * 60 +
                                int.Parse(date.Split('-')[2]) * 24 * 60;

            timeInMinutes += int.Parse(time.Split(':')[0]) * 60 +
                             int.Parse(time.Split(':')[1]);
            return timeInMinutes;
        }

        static void Insert()
        {
            string[] array0 = new string[] {
            "1000",
            "2021-06-01 12:00 | 500 | in",
            "2021-06-07 12:05 | 7500 | in",
            "2021-06-03 12:10 | 1000 | in",
            "2021-06-05 10:00 | 5500 | out",
            "2021-06-03 12:00"
            };
            File.WriteAllLines(".Bills.txt", array0);
        }
    }
}