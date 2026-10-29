using System;
using System.IO;
using System.Text;

// СТРИМРИДЕР ЭТО АНАЛОГ ФАЙЛА С ОТКРЫТИЕМ НА ЧТЕНИЕ, А СТРИМРАЙТЕР ЭТО АНАЛОГ ФАЙЛА С ОТКРЫТИЕМ НА ЗАПИСЬ

namespace Lab1_Cars
{
    class Program
    {
        const int MAX_CARS = 100;
        const string InputFileName = "input_3.txt";

        static void Main()
        {
            Encoding cp1251 = Encoding.GetEncoding(1251);
            Console.InputEncoding = cp1251;
            Console.OutputEncoding = cp1251;

            TCar[] cars = new TCar[MAX_CARS];
            int carCount = 0;
            int choice = -1;

            if (File.Exists(InputFileName)) // Проверяем, существует ли файл
            {
                StreamReader ft = new StreamReader(InputFileName, cp1251);
                CarUn.AddFromTextFile(cars, ref carCount, MAX_CARS, ft);
                ft.Close();
                Console.WriteLine("Данные успешно импортированы из файла " + InputFileName + " (Записей: " + carCount + ").");
            }
            else
            {
                Console.WriteLine("[Предупреждение] Файл " + InputFileName + " не найден.");
            }

            while (choice != 0)
            {
                Console.WriteLine("\n===== УПРАВЛЕНИЕ УЧЕТОМ АВТОМОБИЛЕЙ =====");
                Console.WriteLine("1. Просмотреть список автомобилей (таблица)");
                Console.WriteLine("2. Отсортировать список (по алфавиту)");
                Console.WriteLine("3. Поиск автомобилей марки без ТО");
                Console.WriteLine("4. Добавить новый автомобиль");
                Console.WriteLine("0. Выход из программы");
                Console.WriteLine("==========================================");
                Console.Write("Ваш выбор: ");

                choice = CarUn.ReadInt(0);

                if (choice == 1)
                {
                    Console.WriteLine("\n--- ТЕКУЩИЙ СПИСОК АВТОМОБИЛЕЙ ---");
                    CarUn.PrintAllCarsTable(cars, carCount);
                }
                else if (choice == 2)
                {
                    SortMenu(cars, carCount);
                }
                else if (choice == 3)
                {
                    SearchMenu(cars, carCount);
                }
                else if (choice == 4)
                {
                    AddCarMenu(cars, ref carCount);
                }
            }

            Console.WriteLine("\nПрограмма завершила работу.");
        }

        // Сортировка

        static void SortMenu(TCar[] cars, int carCount)
        {
            if (carCount == 0)
            {
                Console.WriteLine("[Ошибка] Список пуст. Нечего сортировать.");
            }
            else
            {
                Console.WriteLine("\n--- СОРТИРОВКА СПИСКА (по алфавиту) ---");
                Console.WriteLine("1. По фамилии владельца");
                Console.WriteLine("2. По марке");
                Console.WriteLine("3. По модели");
                Console.WriteLine("4. По номеру");
                Console.WriteLine("5. По статусу тех. осмотра");
                Console.Write("Ваш выбор: ");

                int sortChoice = CarUn.ReadInt(0);

                if (sortChoice == 1) { CarUn.SortBySurname(cars, carCount); Console.WriteLine("[Успех] Отсортировано по фамилии."); }
                else if (sortChoice == 2) { CarUn.SortByBrand(cars, carCount); Console.WriteLine("[Успех] Отсортировано по марке."); }
                else if (sortChoice == 3) { CarUn.SortByModel(cars, carCount); Console.WriteLine("[Успех] Отсортировано по модели."); }
                else if (sortChoice == 4) { CarUn.SortByNumber(cars, carCount); Console.WriteLine("[Успех] Отсортировано по номеру."); }
                else if (sortChoice == 5) { CarUn.SortByInspection(cars, carCount); Console.WriteLine("[Успех] Отсортировано по статусу ТО."); }
                else { Console.WriteLine("[Ошибка] Неверный критерий сортировки."); }
            }
        }

        // Поиск

        static void SearchMenu(TCar[] cars, int carCount)
        {
            if (carCount == 0)
            {
                Console.WriteLine("[Ошибка] Список пуст. Нечего анализировать.");
            }
            else
            {
                int brandIndex = SelectBrand("Выберите марку автомобиля для поиска:");
                if (brandIndex != -1)
                {
                    TCar[] filteredCars = new TCar[MAX_CARS];
                    int foundCount = CarUn.FilterFailedInspectionByBrand(cars, carCount, CarUn.Brands[brandIndex], filteredCars);
                    CarUn.PrintFailedInspectionResults(filteredCars, foundCount, CarUn.Brands[brandIndex]);
                }
            }
        }

        // Добавление

        // Выбор марки из списка. Возвращает индекс или -1, если пользователь вышел в меню
        static int SelectBrand(string prompt)
        {
            int valid = 1;
            int brandIndex = -1;

            while (brandIndex == -1 && valid == 1)
            {
                Console.WriteLine("\n--- МАРКА ---");
                Console.WriteLine(prompt);
                int i = 0;
                while (i < CarUn.BrandsCount)
                {
                    Console.WriteLine((i + 1) + ". " + CarUn.Brands[i]);
                    i++;
                }
                Console.Write("0. Выйти в меню\nВаш выбор: ");

                int select = CarUn.ReadInt(0);

                if (select == 0)
                {
                    valid = 0;
                }
                else if (select >= 1 && select <= CarUn.BrandsCount)
                {
                    brandIndex = select - 1;
                }
                else
                {
                    Console.WriteLine("[Ошибка] Неверный выбор.");
                    if (CarUn.AskRetryOrExit() == 0)
                    {
                        valid = 0;
                    }
                }
            }
            return brandIndex;
        }

        // Выбор модели для марки. Возвращает индекс или -1
        static int SelectModel(int brandIndex)
        {
            int valid = 1;
            int modelIndex = -1;

            while (modelIndex == -1 && valid == 1)
            {
                Console.WriteLine("\n--- МОДЕЛЬ ---");
                Console.WriteLine("Выберите модель для " + CarUn.Brands[brandIndex] + ":");
                int m = 0;
                while (m < CarUn.ModelsPerBrand)
                {
                    Console.WriteLine((m + 1) + ". " + CarUn.Models[brandIndex][m]);
                    m++;
                }
                Console.Write("0. Выйти в меню\nВаш выбор: ");

                int select = CarUn.ReadInt(0);

                if (select == 0)
                {
                    valid = 0;
                }
                else if (select >= 1 && select <= CarUn.ModelsPerBrand)
                {
                    modelIndex = select - 1;
                }
                else
                {
                    Console.WriteLine("[Ошибка] Неверный выбор.");
                    if (CarUn.AskRetryOrExit() == 0)
                    {
                        valid = 0;
                    }
                }
            }
            return modelIndex;
        }

        // Ввод строкового поля с проверкой. Возвращает 1, если значение введено, 0 - если пользователь вышел
        static int ReadField(string title, string prompt, string errorText, Func<string, bool> check, out string value)
        {
            int valid = 1;
            int ok = 0;
            value = "";

            while (ok == 0 && valid == 1)
            {
                Console.WriteLine("\n--- " + title + " ---");
                Console.Write(prompt);
                string s = Console.ReadLine();
                if (s == null)
                {
                    s = "";
                }
                if (check(s))
                {
                    value = s;
                    ok = 1;
                }
                else
                {
                    Console.WriteLine(errorText);
                    if (CarUn.AskRetryOrExit() == 0)
                    {
                        valid = 0;
                    }
                }
            }
            return valid;
        }

        static void AddCarMenu(TCar[] cars, ref int carCount)
        {
            if (carCount >= MAX_CARS)
            {
                Console.WriteLine("[Ошибка] Переполнение: достигнут лимит записей (" + MAX_CARS + ").");
            }
            else
            {
                TCar newCar = TCar.ZeroRec;
                int valid = 1;
                Console.WriteLine("\n=== ДОБАВЛЕНИЕ НОВОГО АВТОМОБИЛЯ ===");

                // 1. Марка
                int brandIndex = SelectBrand("Выберите марку автомобиля:");
                if (brandIndex == -1)
                {
                    valid = 0;
                }
                else
                {
                    newCar.Brand = CarUn.Brands[brandIndex];
                }

                // 2. Модель
                if (valid == 1)
                {
                    int modelIndex = SelectModel(brandIndex);
                    if (modelIndex == -1)
                    {
                        valid = 0;
                    }
                    else
                    {
                        newCar.Model = CarUn.Models[brandIndex][modelIndex];
                    }
                }

                // 3. Гос. номер
                if (valid == 1)
                {
                    valid = ReadField("НОМЕР",
                        "Введите гос. номер (формат А000АА, 6 символов, заглавные буквы из набора АВЕКМНОРСТУХ): ",
                        "[Ошибка] Неверный формат номера!",
                        s => CarUn.IsValidNumber(s) == 1,
                        out newCar.Number);
                }

                // 4. Фамилия
                if (valid == 1)
                {
                    valid = ReadField("ФАМИЛИЯ",
                        "Введите фамилию владельца (до 20 символов): ",
                        "[Ошибка] Некорректная фамилия!",
                        s => CarUn.IsValidFIO(s, 1, 20) == 1,
                        out newCar.Owner.Surname);
                }

                // 5. Имя
                if (valid == 1)
                {
                    valid = ReadField("ИМЯ",
                        "Введите имя владельца (до 20 символов): ",
                        "[Ошибка] Некорректное имя!",
                        s => CarUn.IsValidFIO(s, 2, 20) == 1,
                        out newCar.Owner.Name);
                }

                // 6. Отчество
                if (valid == 1)
                {
                    valid = ReadField("ОТЧЕСТВО",
                        "Введите отчество владельца (до 25 символов, или '-' если нет): ",
                        "[Ошибка] Некорректное отчество!",
                        s => CarUn.IsValidPatronymic(s) == 1,
                        out newCar.Owner.Patronymic);
                }

                // 7. Техосмотр
                int inspectionValid = 0;
                while (inspectionValid == 0 && valid == 1)
                {
                    Console.WriteLine("\n--- ТЕХНИЧЕСКИЙ ОСМОТР ---");
                    Console.Write("Статус техосмотра (1 — пройден, 0 — не пройден): ");
                    int inspection = CarUn.ReadInt(-1);
                    if (inspection == 0 || inspection == 1)
                    {
                        newCar.HasInspection = inspection;
                        inspectionValid = 1;
                    }
                    else
                    {
                        Console.WriteLine("[Ошибка] Статус должен быть 0 или 1!");
                        if (CarUn.AskRetryOrExit() == 0)
                        {
                            valid = 0;
                        }
                    }
                }

                if (valid == 1)
                {
                    if (CarUn.AddCar(cars, ref carCount, MAX_CARS, newCar) == 1)
                    {
                        Console.WriteLine("[Успех] Автомобиль " + newCar.Brand + " " + newCar.Model + " (" + newCar.Number + ") успешно добавлен!");
                    }
                    else
                    {
                        Console.WriteLine("[Ошибка] Не удалось добавить запись (переполнение).");
                    }
                }
            }
        }
    }
}