using System;
using System.IO;
using System.Text;

namespace TaskSerialize
{
    class Program
    {
        static void Main(string[] args)
        {
            bool serializeComputableFields = false;
            var bill = new Bill(paymentPerday: 200, days: 7, penaltyPerDayDelay: 20);

            //Saving to file
            Save(bill, "TaskSerialize.json", serializeComputableFields);

            //Reading from file
            var loadBill = ReadBill("TaskSerialize.json");

            Console.WriteLine($" cумма к оплате без штрафа: {loadBill.Sum} \n штраф: {loadBill.PenaltySum} \n общая сумма к оплате: {loadBill.PaymentSum}");
            Console.ReadKey();
        }

        public static void Save(Bill bill, string fileName, bool serializeComputableFields)
        {
            using (StreamWriter writer = File.CreateText(fileName))
            {
                bill.Serialize(writer, bill, serializeComputableFields);
            }
        }
        public static Bill ReadBill(string fileName)
        {
            using (Stream file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(file, Encoding.UTF8))
            {
                return Bill.Deserialize(reader);
            }
        }
    }
}


/*Разработать класс «Счет для оплаты». В классе предусмотреть следующие поля:
-оплата за день;
-количество дней;
-штраф за один день задержки оплаты;
-количество дней задержи оплаты;
-сумма к оплате без штрафа (вычисляемое поле);
-штраф(вычисляемое поле);
-общая сумма к оплате (вычисляемое поле).
В классе объявить статическое свойство типа bool,
значение которого влияет на процесс форматирования
объектов этого класса. Если значение этого свойства равно true, тогда сериализуются и десериализируются все
поля, если false — вычисляемые поля не сериализуются.
Разработать приложение, в котором необходимо продемонстрировать использование этого класса, результаты
должны записываться и считываться из файла.*/