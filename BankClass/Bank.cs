using System.Globalization;

namespace bank;
using System;
using System.Collections;
using System.Collections.Generic;
public class Bank 
{
    private int _cshot;
    //ФИО владельца счета (строка)
    private string _name;
    //Сумма на счету (вещественное)
    private double _sum;
    //Список счетов
    private List list = new List();
    // Данный аканут
    private int accaunt = 0;

    public Bank(int cshot, string name, double sum)
    {
        _cshot = cshot;
        _name = name;
        _sum = sum;
    }

    public Bank()
    {
    }

    internal class List
    {
        public List<Bank> listBank = new List<Bank>();
        public void AddBank(Bank bank) => listBank.Add(bank);

    }


    public void Menu()
    {

        //CreateBank();
        list.AddBank(new Bank(123, "David", 777));
        list.AddBank(new Bank(456, "Dimasik", 1));
        while (true)
        {
            Console.WriteLine($"-=-=-=-=-=-=-=-=-=-=-=-=Акаунт-=-=-=-=-=-=-=-=-=-=-=-=" +
                              $"\n Ваш аккаунт Номер счета {list.listBank[accaunt]._cshot} | Имя {list.listBank[accaunt]._name} | Сумма {Math.Round(list.listBank[accaunt]._sum, 2)}");
            Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=Интерфейс-=-=-=-=-=-=-=-=-=-=-=-=" +
                              "\n 1)Информация по счетам" +
                              "\n 2)Создание нового акаунта" +
                              "\n 3)Выбор нового акаунта" +
                              "\n 4)Пополнение счета" +
                              "\n 5)Снять со счета" +
                              "\n 6)Снять все со счета" +
                              "\n 7)Перевод на другой счет");

            switch (Console.ReadLine())
            {
                case "1":
                    Info();
                    break;
                case "2":
                    CreateBank();
                    break;
                case "3":
                    Change();
                    break;
                case "4":
                    Plus();
                    break;
                case "5":
                    Minus();
                    break;
                case "6":
                    AllMinus();
                    break;
                case "7":
                    Tranzit();
                    break;
                default:
                    Console.WriteLine("Такого действия нету");
                    break;
            }
        }
    }







    private void CreateBank()
    {
        bool work = true;
        Console.WriteLine("=-=-=--=-=-=-=-=-=-=-=-Создание cчета-=-=-=-=-=-=-=-=-=-=-=-");
        Console.WriteLine($"Ввидите (счет,имя,сумма): ");
        while (work)
        {
            string[] subject = Console.ReadLine().Split(' ', '/', '.', '-');
            try
            {
                bool tring = true;
                foreach (var item in list.listBank)
                    if (int.Parse(subject[0]) == item._cshot)
                    {
                        Console.WriteLine("Такой счет уже есть");
                        tring = false;
                    }

                if (int.Parse(subject[0]) > 0 && double.Parse(subject[2]) >= 0 && tring)
                {
                    list.AddBank(new Bank(int.Parse(subject[0]), subject[1], double.Parse(subject[2])));
                    work = false;
                }
                else
                    Console.WriteLine("Нельзя указать отрицательные значение");
            }
            catch
            {
                Console.WriteLine("Попробуйте еще раз...");
            }
        }
    }




    private void Plus()
    {
        Console.Write("На сколько хочешь пополнить счет: ");
        while (true)
            try
            {
                double plus = Double.Parse(Console.ReadLine());
                if (plus > 0)
                {
                    Math.Round(list.listBank[accaunt]._sum += plus, 2);
                    break;
                }
                else
                    Console.WriteLine("Нельзя прибавить минус");
            }
            catch
            {
                Console.WriteLine("Попробуй еще раз");
            }
    }
    private void AllMinus() => list.listBank[accaunt]._sum = 0;

    private void Minus()
    {
        while (true)
            try
            {
                if (list.listBank[accaunt]._sum == 0)
                {
                    Console.WriteLine("На карте и так ноль, что ты еще хочешь от карты");
                    break;
                }
                Console.Write("На сколько хочешь опустошить свой счет: ");
                double min = Double.Parse(Console.ReadLine());
                if (min > list.listBank[accaunt]._sum)
                {
                    Console.WriteLine($"Нельзя получилось снять {min - list.listBank[accaunt]._sum}");
                    list.listBank[accaunt]._sum = 0;
                }
                else
                {
                    if (min > 0)
                    {
                        Math.Round(list.listBank[accaunt]._sum -= min, 2);
                        break;
                    }
                    else
                        Console.WriteLine("Нельзя отнять минус");
                }
            }
            catch
            {
                Console.WriteLine("Попробуй еще раз");
            }
    }
    private void Info()
    {
        Console.WriteLine("=-=-=--=-=-=-=-=-=-=-=-Счета банка-=-=-=-=-=-=-=-=-=-=-=-");
        foreach (var item in list.listBank)
            Console.WriteLine($"Номер счета {item._cshot} | Имя {item._name} | Сумма {Math.Round(item._sum, 2)}");
    }
    private void Change()
    {
        Console.Write("Ввидите номер счета на который хотите поменять: ");
        while (true)
            try
            {
                int acc = Convert.ToInt32(Console.ReadLine());
                if (list.listBank.Exists(x => x._cshot == acc))
                {
                    accaunt = list.listBank.IndexOf(list.listBank.Find(x => x._cshot.Equals(acc)));
                    break;
                }
                else
                    Console.WriteLine("Такого счета нету");
            }
            catch
            {
                Console.WriteLine("Попробуй еще раз");
            }
    }
    private void Tranzit()
    {
        bool work = true;
        while (work)
        {
            try
            {
                if (list.listBank[accaunt]._sum < 0.01)
                {
                    Console.WriteLine("На карте и так ноль, что ты еще хочешь от карты");
                    break;
                }
                Console.Write("Ввидите номер счета на который хотите перевести: ");
                int number = int.Parse(Console.ReadLine());
                if (list.listBank.Exists(x => x._cshot == number))
                {
                    while (work)
                    {
                        Console.Write("Какную сумму вы хотите превести: ");
                        float sum = float.Parse(Console.ReadLine());
                        if (Math.Round(sum, 2) > list.listBank[accaunt]._sum)
                        {
                            Console.WriteLine("У вас нету столько мани, попробуйте еще раз" +
                                              "\nили хотите перевести все(да/нет)");
                            string choose = Console.ReadLine();
                            if (choose == "да")
                            {
                                Math.Round(list.listBank.Find(x => x._cshot.Equals(number))._sum += list.listBank[accaunt]._sum, 2);
                                list.listBank[accaunt]._sum = 0;
                                work = false;
                            }
                        }
                        else
                        {
                            list.listBank[accaunt]._sum = Convert.ToDouble(float.Parse(Convert.ToString(list.listBank[accaunt]._sum)) - sum);
                            list.listBank.Find(x => x._cshot.Equals(number))._sum += Convert.ToDouble(sum);
                            work = false;
                        }

                    }
                }
                else
                    Console.WriteLine("Такого акаунта нету");
            }
            catch
            {
                Console.WriteLine("Попробуйте еще раз");
            }
        }
    }














}

















































