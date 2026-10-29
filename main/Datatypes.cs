namespace Lab1_Cars
{
    // Структура 1: ФИО владельца
    public struct TFIO
    {
        public string Surname;     // фамилия, до 20 символов
        public string Name;        // имя, до 20 символов
        public string Patronymic;  // отчество, до 25 символов ("-" если нет)
    }

    // Структура 2: данные об автомобиле
    public struct TCar
    {
        public string Brand;       // марка
        public string Model;       // модель
        public string Number;      // гос. номер, 6 символов
        public TFIO Owner;         // владелец
        public int HasInspection;  // 1 - пройден, 0 - не пройден

        public static readonly TCar ZeroRec = new TCar
        {
            Brand = "",
            Model = "",
            Number = "",
            Owner = new TFIO { Surname = "", Name = "", Patronymic = "" },
            HasInspection = 0
        };
    }
}