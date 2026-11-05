using Lab1_Insurance;
using System;
using System.Globalization;
using System.IO;
namespace Lab2_Insurance
{
    public class CarUn
    {
        // Массивы марок и моделей
        public static readonly string[] Brands = {
            "Audi", "Chevrolet", "BMW", "Subaru", "Mercedes-Benz", "Lada"
        };
        public const int BrandsCount = 6;
        // Модели по маркам
        public static readonly TModel[][] Models = new TModel[BrandsCount][];
        // Инициализация справочника моделей
        public static void InitModels()
        {
            // Audi
            Models[0] = new TModel[]
            {
                M("A4", 1994, 190, 1.10, 4.68),
                M("A6", 1994, 245, 1.20, 6.69),
                M("Q5", 2008, 252, 1.20, 5.10),
                M("Q7", 2005, 340, 1.35, 8.46)
            };
            // Chevrolet
            Models[1] = new TModel[]
            {
                M("Camaro", 1967, 335, 1.30, 3.50),
                M("Corvette", 1953, 455, 1.50, 15.39),
                M("Tahoe", 1995, 355, 1.25, 4.99),
                M("Cruze", 2008, 153, 1.00, 0.50)
            };
            // BMW
            Models[2] = new TModel[]
            {
                M("3 Series", 1975, 255, 1.25, 5.45),
                M("5 Series", 1972, 248, 1.25, 6.56),
                M("X5", 1999, 335, 1.40, 5.68),
                M("M3", 1986, 473, 1.60, 8.40)
            };
            // Subaru
            Models[3] = new TModel[]
            {
                M("Impreza", 1992, 152, 1.00, 2.44),
                M("Forester", 1997, 182, 1.05, 6.90),
                M("Outback", 1994, 182, 1.05, 8.99),
                M("WRX", 1992, 271, 1.40, 3.72)
            };
            // Mercedes-Benz
            Models[4] = new TModel[]
            {
                M("C-Class", 1993, 255, 1.15, 5.38),
                M("E-Class", 1993, 255, 1.20, 10.03),
                M("S-Class", 1972, 429, 1.45, 32.94),
                M("G-Class", 1979, 416, 1.70, 22.80)
            };
            // Lada
            Models[5] = new TModel[]
            {
                M("Vesta", 2015, 106, 0.90, 1.48),
                M("Granta", 2011, 106, 0.85, 0.78),
                M("Niva Legend", 1977, 83, 0.90, 1.20),
                M("Largus", 2012, 106, 0.90, 1.39)
            };
        }
        // Вспомогательный метод для создания записи модели
        private static TModel M(string name, int year, double power, double coef, double price)
        {
            TModel m = new TModel();
            m.Name = name;
            m.Year = year;
            m.Power = power;
            m.Coef = coef;
            m.Price = price;
            return m;
        }
        // Количество моделей у каждой марки
        public static int GetModelsCount(int brandIndex)
        {
            int k = 0;
            if (brandIndex < 0 || brandIndex >= BrandsCount) k = 0;
            else k = Models[brandIndex].Length;
            return k;
        }
        // Ввод
        // Чтение целого числа; при ошибке ввода возвращает onFail
        public static int ReadInt(int onFail)
        {
            int value;
            string s = Console.ReadLine();
            if (s == null || !int.TryParse(s.Trim(), out value))
            {
                value = onFail;
            }
            return value;
        }
        // Чтение вещественного числа
        public static double ReadDouble(double onFail)
        {
            double value;
            string s = Console.ReadLine();
            if (s == null || !double.TryParse(s.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out value))
            {
                value = onFail;
            }
            return value;
        }
        // Запрос действия при ошибке
        public static int AskRetryOrExit()
        {
            Console.Write("1. Ввести заново\n0. Выйти в меню\nВаш выбор: ");
            return ReadInt(0);
        }
        // Валидация
        // Допустимые буквы гос. номера (кириллица, только заглавные)
        private const string PlateLetters = "АВЕКМНОРСТУХ";
        public static int IsAllowedPlateLetter(char ch)
        {
            int allowed = 0;
            int i = 0;
            while (i < PlateLetters.Length && allowed == 0)
            {
                if (ch == PlateLetters[i])
                {
                    allowed = 1;
                }
                i++;
            }
            return allowed;
        }
        public static int IsValidNumber(string number)
        {
            int res = 0;
            if (number != null && number.Length == 6)
            {
                if (IsAllowedPlateLetter(number[0]) == 1 &&
                    (number[1] >= '0' && number[1] <= '9') &&
                    (number[2] >= '0' && number[2] <= '9') &&
                    (number[3] >= '0' && number[3] <= '9') &&
                    IsAllowedPlateLetter(number[4]) == 1 &&
                    IsAllowedPlateLetter(number[5]) == 1)
                {
                    res = 1;
                }
            }
            return res;
        }
        public static int IsValidBrand(string brand)
        {
            int res = 0;
            if (brand != null)
            {
                int i = 0;
                while (i < BrandsCount && res == 0)
                {
                    if (string.Compare(brand, Brands[i], StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        res = 1;
                    }
                    i++;
                }
            }
            return res;
        }
        // Поиск индекса модели по названию; возвращает -1, если не найдена
        public static int FindModelIndex(int brandIndex, string modelName)
        {
            int Er = -1;
            if (brandIndex >= 0 && brandIndex < BrandsCount && modelName != null)
            {
                int m = 0;
                while (m < Models[brandIndex].Length && Er == -1)
                {
                    if (string.Compare(modelName, Models[brandIndex][m].Name,
                                       StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        Er = m;
                    }
                    m++;
                }
            }
            return Er;
        }
        public static int IsValidModel(string brand, string model)
        {
            int res = 0;
            if (brand != null && model != null)
            {
                int brandIndex = -1;
                int i = 0;
                while (i < BrandsCount && brandIndex == -1)
                {
                    if (string.Compare(brand, Brands[i], StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        brandIndex = i;
                    }
                    i++;
                }
                if (brandIndex != -1)
                {
                    if (FindModelIndex(brandIndex, model) != -1)
                    {
                        res = 1;
                    }
                }
            }
            return res;
        }
        public static int IsValidFIO(string text, int minLen, int maxLen)
        {
            int res = 1;
            if (text == null)
            {
                res = 0;
            }
            else
            {
                int len = text.Length;
                if (len < minLen || len > maxLen)
                {
                    res = 0;
                }
                // Пробелы в начале и конце запрещены
                else if (text[0] == ' ' || text[len - 1] == ' ')
                {
                    res = 0;
                }
                // Дефис не может быть первым или последним символом
                else if (text[0] == '-' || text[len - 1] == '-')
                {
                    res = 0;
                }
                else
                {
                    int i = 0;
                    while (i < len && res == 1)
                    {
                        // Запрет двойных пробелов
                        if (text[i] == ' ' && i > 0 && text[i - 1] == ' ')
                        {
                            res = 0;
                        }
                        // Запрет цифр
                        else if (char.IsDigit(text[i]))
                        {
                            res = 0;
                        }
                        // Запрет всех спецсимволов, кроме дефиса
                        else if ((char.IsPunctuation(text[i]) || char.IsSymbol(text[i])) && text[i] != '-')
                        {
                            res = 0;
                        }
                        i++;
                    }
                }
            }
            return res;
        }
        public static int IsValidPatronymic(string text)
        {
            int res = 0;
            if (text != null)
            {
                if (text == "-")
                {
                    res = 1;
                }
                else
                {
                    res = IsValidFIO(text, 2, 25);
                }
            }
            return res;
        }
        // Проверка возраста (18..122)
        public static int IsValidAge(int age)
        {
            int res = 0;
            if (age >= 18 && age <= 122)
            {
                res = 1;
            }
            return res;
        }
        // Проверка стажа (0..104), причём возраст - стаж >= 18
        public static int IsValidExp(int age, int exp)
        {
            int res = 0;
            if (exp >= 0 && exp <= 104 && age - exp >= 18)
            {
                res = 1;
            }
            return res;
        }
        // Загрузка из файла автомобилей
        public static int AddCarsFromTextFile(TCar[] cars, ref int count, int maxCount, StreamReader txtFile)
        {
            int success = 0;
            if (txtFile != null && count < maxCount)
            {
                int reading = 1;
                while (count < maxCount && reading == 1)
                {
                    string brand = txtFile.ReadLine();
                    if (brand != null)
                    {
                        if (brand.Length > 0)
                        {
                            string model = txtFile.ReadLine();
                            string number = txtFile.ReadLine();
                            string surname = txtFile.ReadLine();
                            string name = txtFile.ReadLine();
                            string patronymic = txtFile.ReadLine();
                            string ageStr = txtFile.ReadLine();
                            string expStr = txtFile.ReadLine();
                            string inspStr = txtFile.ReadLine();
                            if (model != null && number != null && surname != null &&
                                name != null && patronymic != null &&
                                ageStr != null && expStr != null && inspStr != null)
                            {
                                TCar car = TCar.ZeroRec;
                                car.Brand = brand;
                                car.Model = model;
                                car.Number = number;
                                car.Owner.Surname = surname;
                                car.Owner.Name = name;
                                car.Owner.Patronymic = patronymic;
                                int age, exp, inspection;
                                if (!int.TryParse(ageStr.Trim(), out age)) age = -1;
                                if (!int.TryParse(expStr.Trim(), out exp)) exp = -1;
                                if (!int.TryParse(inspStr.Trim(), out inspection)) inspection = -1;
                                car.Owner.Age = age;
                                car.Owner.Exp = exp;
                                car.HasInspection = inspection;
                                if (IsValidBrand(car.Brand) == 1 &&
                                    IsValidModel(car.Brand, car.Model) == 1 &&
                                    IsValidNumber(car.Number) == 1 &&
                                    IsValidFIO(car.Owner.Surname, 1, 20) == 1 &&
                                    IsValidFIO(car.Owner.Name, 2, 20) == 1 &&
                                    IsValidPatronymic(car.Owner.Patronymic) == 1 &&
                                    IsValidAge(car.Owner.Age) == 1 &&
                                    IsValidExp(car.Owner.Age, car.Owner.Exp) == 1 &&
                                    (car.HasInspection == 0 || car.HasInspection == 1))
                                {
                                    cars[count] = car;
                                    count++;
                                }
                                else
                                {
                                    Console.WriteLine("[Предупреждение] Пропущена некорректная запись из файла авто.");
                                }
                            }
                            else
                            {
                                reading = 0;
                            }
                        }
                    }
                    else
                    {
                        reading = 0;
                    }
                }
                success = 1;
            }
            return success;
        }
        // Загрузка из файла полисов
        public static int AddPoliciesFromTextFile(TPolicy[] policies, ref int count, int maxCount,
                                          StreamReader txtFile, TCar[] cars, int carCount)
        {
            int success = 0;
            if (txtFile != null && count < maxCount)
            {
                int reading = 1;
                while (count < maxCount && reading == 1)
                {
                    string typeStr = txtFile.ReadLine();
                    if (typeStr != null)
                    {
                        if (typeStr.Length > 0)
                        {
                            string carNum = txtFile.ReadLine();
                            string dateStr = txtFile.ReadLine();
                            string termStr = txtFile.ReadLine();
                            if (carNum != null && dateStr != null && termStr != null)
                            {
                                int type, term;
                                DateTime date;
                                if (!int.TryParse(typeStr.Trim(), out type)) type = -1;
                                if (!int.TryParse(termStr.Trim(), out term)) term = -1;
                                if (!DateTime.TryParseExact(dateStr.Trim(), "dd.MM.yyyy",
                                    System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
                                {
                                    date = DateTime.MinValue;
                                }
                                // Ищем автомобиль по номеру
                                int carIndex = FindCarByNumber(cars, carCount, carNum);
                                if (carIndex == -1)
                                {
                                    Console.WriteLine("[Предупреждение] Полис пропущен: автомобиль с номером " + carNum + " не найден.");
                                }
                                else if (IsValidPolicyData(type, term, date) == 0)
                                {
                                    Console.WriteLine("[Предупреждение] Полис пропущен: некорректные данные полиса.");
                                }
                                else
                                {
                                    TPolicy pol = TPolicy.ZeroRec;
                                    pol.Type = type;
                                    pol.CarNumber = carNum;
                                    pol.Date = date;
                                    pol.Term = term;
                                    pol.Cost = CalculatePolicyCost(cars[carIndex], pol);
                                    policies[count] = pol;
                                    count++;
                                }
                            }
                            else
                            {
                                reading = 0;
                            }
                        }
                    }
                    else
                    {
                        reading = 0;
                    }
                }
                success = 1;
            }
            return success;
        }
        // Проверка корректности данных полиса
        public static int IsValidPolicyData(int type, int term, DateTime date)
        {
            int res = 0;
            if (type == 1 || type == 2)
            {
                if (term >= 1 && term <= 84)
                {
                    if (type == 1)
                    {
                        if (term <= 12) // ОСАГО не более 12 месяцев
                        {
                            if (date.Year >= 2000 && date.Year <= 2026)
                            {
                                res = 1;
                            }
                        }
                    }
                    else if (type == 2)
                    {
                        if (date.Year >= 2000 && date.Year <= 2026)
                        {
                            res = 1;
                        }
                    }
                }
            }
            return res;
        }
        // Поиск автомобиля по номеру; возвращает индекс или -1
        public static int FindCarByNumber(TCar[] cars, int count, string number)
        {
            int idx = -1;
            int i = 0;
            while (i < count && idx == -1)
            {
                if (string.Compare(cars[i].Number, number, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    idx = i;
                }
                i++;
            }
            return idx;
        }
        // Расчёт стоимости полиса
        public static double CalculatePolicyCost(TCar car, TPolicy pol)
        {
            double cost = 0;
            int brandIndex = -1;
            int i = 0;
            while (i < BrandsCount && brandIndex == -1)
            {
                if (string.Compare(car.Brand, Brands[i], StringComparison.OrdinalIgnoreCase) == 0)
                {
                    brandIndex = i;
                }
                i++;
            }
            if (brandIndex != -1)
            {
                int modelIndex = FindModelIndex(brandIndex, car.Model);
                if (modelIndex != -1)
                {
                    TModel m = Models[brandIndex][modelIndex];
                    double PM = m.Price * 1000.0;
                    double CM = m.Coef;
                    double P = m.Power;
                    // CP - коэф. мощности
                    double CP;
                    if (P <= 50) CP = 0.6;
                    else if (P <= 100) CP = 0.6 + 0.4 * (P - 50) / 50.0;
                    else if (P <= 300) CP = 1.0 + 0.6 * (P - 100) / 200.0;
                    else CP = 1.6 + 0.4 * (P - 300) / 950.0;
                    // KA - коэф. возраста
                    int age = car.Owner.Age;
                    double KA;
                    if (age < 20) KA = 1.5;
                    else if (age < 30) KA = 1.5 - 0.05 * (age - 20);
                    else if (age <= 60) KA = 1.0;
                    else if (age <= 70) KA = 1.0 + 0.05 * (age - 60);
                    else KA = 1.5;
                    // KE - коэф. стажа
                    int exp = car.Owner.Exp;
                    double KE;
                    if (exp < 3) KE = 1.3 - 0.1 * exp;
                    else KE = 1.0;
                    double CA = KA * KE;
                    // K - коэф. типа полиса
                    double K = (pol.Type == 1) ? 0.005 : 0.06;
                    cost = PM * CM * CP * CA * K * pol.Term;
                    cost = Math.Round(cost, 2);
                }
            }
            return cost;
        }
        public static int IsPolicyActive(TPolicy pol)
        {
            int active = 0;
            DateTime endDate = pol.Date.AddMonths(pol.Term);
            if (endDate > DateTime.Now)
            {
                active = 1;
            }
            return active;
        }
        // Проверка: можно ли оформить новый полис данного типа на авто
        public static int CanIssuePolicy(TPolicy[] policies, int count, string carNumber, int type)
        {
            int canIssue = 1;
            int i = 0;
            while (i < count && canIssue == 1)
            {
                if (string.Compare(policies[i].CarNumber, carNumber, StringComparison.OrdinalIgnoreCase) == 0 &&
                    policies[i].Type == type &&
                    IsPolicyActive(policies[i]) == 1)
                {
                    canIssue = 0;
                }
                i++;
            }
            return canIssue;
        }
        // Сортировка (пузырьком)
        public static void SortBySurname(TCar[] cars, int count)
        {
            if (count >= 2)
            {
                int i = 0;
                while (i < count - 1)
                {
                    int j = 0;
                    while (j < count - i - 1)
                    {
                        if (string.Compare(cars[j].Owner.Surname, cars[j + 1].Owner.Surname, StringComparison.OrdinalIgnoreCase) > 0)
                        {
                            TCar temp = cars[j];
                            cars[j] = cars[j + 1];
                            cars[j + 1] = temp;
                        }
                        j++;
                    }
                    i++;
                }
            }
        }
        public static void SortByBrand(TCar[] cars, int count)
        {
            if (count >= 2)
            {
                int i = 0;
                while (i < count - 1)
                {
                    int j = 0;
                    while (j < count - i - 1)
                    {
                        if (string.Compare(cars[j].Brand, cars[j + 1].Brand, StringComparison.OrdinalIgnoreCase) > 0)
                        {
                            TCar temp = cars[j];
                            cars[j] = cars[j + 1];
                            cars[j + 1] = temp;
                        }
                        j++;
                    }
                    i++;
                }
            }
        }
        public static void SortByModel(TCar[] cars, int count)
        {
            if (count >= 2)
            {
                int i = 0;
                while (i < count - 1)
                {
                    int j = 0;
                    while (j < count - i - 1)
                    {
                        if (string.Compare(cars[j].Model, cars[j + 1].Model, StringComparison.OrdinalIgnoreCase) > 0)
                        {
                            TCar temp = cars[j];
                            cars[j] = cars[j + 1];
                            cars[j + 1] = temp;
                        }
                        j++;
                    }
                    i++;
                }
            }
        }
        public static void SortByNumber(TCar[] cars, int count)
        {
            if (count >= 2)
            {
                int i = 0;
                while (i < count - 1)
                {
                    int j = 0;
                    while (j < count - i - 1)
                    {
                        if (string.Compare(cars[j].Number, cars[j + 1].Number, StringComparison.OrdinalIgnoreCase) > 0)
                        {
                            TCar temp = cars[j];
                            cars[j] = cars[j + 1];
                            cars[j + 1] = temp;
                        }
                        j++;
                    }
                    i++;
                }
            }
        }
        public static void SortByInspection(TCar[] cars, int count)
        {
            if (count >= 2)
            {
                int i = 0;
                while (i < count - 1)
                {
                    int j = 0;
                    while (j < count - i - 1)
                    {
                        if (cars[j].HasInspection > cars[j + 1].HasInspection)
                        {
                            TCar temp = cars[j];
                            cars[j] = cars[j + 1];
                            cars[j + 1] = temp;
                        }
                        j++;
                    }
                    i++;
                }
            }
        }
        // Фильтр
        public static int FilterFailedInspectionByBrand(TCar[] cars, int count, string targetBrand, TCar[] resultCars)
        {
            int resultCount = 0;
            int i = 0;
            while (i < count)
            {
                if (string.Compare(cars[i].Brand, targetBrand, StringComparison.OrdinalIgnoreCase) == 0 && cars[i].HasInspection == 0)
                {
                    resultCars[resultCount] = cars[i];
                    resultCount++;
                }
                i++;
            }
            return resultCount;
        }
        // Вывод
        // Обрезка/дополнение строки до заданной ширины
        private static string Fit(string s, int width)
        {
            if (s == null)
            {
                s = "";
            }
            if (s.Length > width)
            {
                s = s.Substring(0, width);
            }
            return s.PadRight(width);
        }
        private static string FullFio(TFIO fio)
        {
            string p = (fio.Patronymic == "-") ? "" : " " + fio.Patronymic;
            return fio.Surname + " " + fio.Name + p;
        }
        // Формат вывода модели: "A4 (2008)"
        public static string FormatModel(string brand, string modelName)
        {
            string result = modelName;
            int brandIndex = -1;
            int i = 0;
            while (i < BrandsCount && brandIndex == -1)
            {
                if (string.Compare(brand, Brands[i], StringComparison.OrdinalIgnoreCase) == 0)
                {
                    brandIndex = i;
                }
                i++;
            }
            if (brandIndex != -1)
            {
                int modelIndex = FindModelIndex(brandIndex, modelName);
                if (modelIndex != -1)
                {
                    result = modelName + " (" + Models[brandIndex][modelIndex].Year + ")";
                }
            }
            return result;
        }
        public static void PrintAllCarsTable(TCar[] cars, int count)
        {
            if (count == 0)
            {
                Console.WriteLine("Список автомобилей пуст.\n");
            }
            else
            {
                string line = new string('=', 130);
                Console.WriteLine(line);
                Console.WriteLine(Fit("Марка", 15) + " | " + Fit("Модель", 18) + " | " + Fit("Номер", 8) + " | " +
                                  Fit("ФИО Владельца", 35) + " | " + Fit("Возр/Стаж", 10) + " | " + Fit("Статус ТО", 12));
                Console.WriteLine(line);
                int i = 0;
                while (i < count)
                {
                    string modelStr = FormatModel(cars[i].Brand, cars[i].Model);
                    string ageExp = cars[i].Owner.Age + "/" + cars[i].Owner.Exp;
                    Console.WriteLine(Fit(cars[i].Brand, 15) + " | " + Fit(modelStr, 18) + " | " +
                                      Fit(cars[i].Number, 8) + " | " + Fit(FullFio(cars[i].Owner), 35) + " | " +
                                      Fit(ageExp, 10) + " | " +
                                      Fit(cars[i].HasInspection == 1 ? "Пройден" : "Не пройден", 12));
                    i++;
                }
                Console.WriteLine(line + "\n");
            }
        }
        public static void PrintFailedInspectionResults(TCar[] resultCars, int resultCount, string targetBrand)
        {
            string line = new string('=', 60);
            Console.WriteLine("\n=== Результаты поиска ===");
            Console.WriteLine("Автомобили марки \"" + targetBrand + "\", не прошедшие ТО:\n");
            Console.WriteLine(line);
            Console.WriteLine(Fit("Номер", 10) + " | " + Fit("ФИО Владельца", 35) + " | " + Fit("Возр/Стаж", 10));
            Console.WriteLine(line);
            int i = 0;
            while (i < resultCount)
            {
                string ageExp = resultCars[i].Owner.Age + "/" + resultCars[i].Owner.Exp;
                Console.WriteLine(Fit(resultCars[i].Number, 10) + " | " + Fit(FullFio(resultCars[i].Owner), 35) + " | " + Fit(ageExp, 10));
                i++;
            }
            Console.WriteLine(line);
            Console.WriteLine("Всего автомобилей марки \"" + targetBrand + "\" без ТО: " + resultCount + "\n");
        }
        public static void PrintAllPoliciesTable(TPolicy[] policies, int count, TCar[] cars, int carCount)
        {
            if (count == 0)
            {
                Console.WriteLine("Список полисов пуст.\n");
            }
            else
            {
                string line = new string('=', 90);
                Console.WriteLine(line);
                Console.WriteLine(Fit("Тип", 8) + " | " + Fit("Номер авто", 10) + " | " + Fit("Дата оформ.", 12) + " | " +
                                  Fit("Срок, мес.", 10) + " | " + Fit("Стоимость, тыс.р.", 20));
                Console.WriteLine(line);
                int i = 0;
                while (i < count)
                {
                    string typeStr = (policies[i].Type == 1) ? "ОСАГО" : "КАСКО";
                    string dateStr = policies[i].Date.ToString("dd.MM.yyyy");
                    Console.WriteLine(Fit(typeStr, 8) + " | " + Fit(policies[i].CarNumber, 10) + " | " +
                                      Fit(dateStr, 12) + " | " + Fit(policies[i].Term.ToString(), 10) + " | " +
                                      Fit(policies[i].Cost.ToString("F2"), 20));
                    i++;
                }
                Console.WriteLine(line + "\n");
            }
        }
        // Добавление
        public static int AddCar(TCar[] cars, ref int count, int maxCount, TCar newCar)
        {
            int status = 1;
            if (count >= maxCount)
            {
                status = 0;
            }
            else
            {
                cars[count] = newCar;
                count++;
            }
            return status;
        }
        public static int AddPolicy(TPolicy[] policies, ref int count, int maxCount, TPolicy newPol)
        {
            int status = 1;
            if (count >= maxCount)
            {
                status = 0;
            }
            else
            {
                policies[count] = newPol;
                count++;
            }
            return status;
        }
    }
}