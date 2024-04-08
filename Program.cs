namespace PracticTask2
{
    class Program
    {
        static void Main()
        {
            var vacationDictionary = new Dictionary<string, List<DateTime>>()
            {
                ["Иванов Иван Иванович"] = new List<DateTime>(),
                ["Петров Петр Петрович"] = new List<DateTime>(),
                ["Юлина Юлия Юлиановна"] = new List<DateTime>(),
                ["Сидоров Сидор Сидорович"] = new List<DateTime>(),
                ["Павлов Павел Павлович"] = new List<DateTime>(),
                ["Георгиев Георг Георгиевич"] = new List<DateTime>()
            };

            Random random = new Random();

            foreach (var vacationList in vacationDictionary)
            {
                int vacationCount = 28;

                while (vacationCount > 0)
                {
                    DateTime startDate = GetRandomWorkingDay(random);
                    int vacationDuration = GetRandomVacationDuration(random);
                    DateTime endDate = startDate.AddDays(vacationDuration);

                    if (!IsVacationOverlapping(vacationList.Value, startDate, endDate) && !IsMonthBoundariesOverlap(vacationList.Value, startDate, endDate))
                    {
                        AddVacationDates(vacationList.Value, startDate, endDate);
                        vacationCount -= vacationDuration;
                    }
                }
            }

            PrintVacationDates(vacationDictionary);
            Console.ReadKey();
        }


        /// <summary>
        /// Проверка является ли день рабочим
        /// </summary>
        /// <param name="date">Проверяемая дата</param>
        /// <returns></returns>
        static bool IsWorkingDay(DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }


        /// <summary>
        /// Генерация случайного рабочего дня
        /// </summary>
        /// <param name="random">Случайное число</param>
        /// <returns></returns>
        static DateTime GetRandomWorkingDay(Random random)
        {
            DateTime start = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime end = new DateTime(DateTime.Today.Year, 12, 31);

            while (true)
            {
                DateTime date = start.AddDays(random.Next((int)(end - start).TotalDays));
                if (IsWorkingDay(date))
                {
                    return date;
                }
            }
        }


        /// <summary>
        /// Генерация случайных дат отпусков
        /// </summary>
        /// <param name="random">Случайное число</param>
        /// <returns></returns>
        static int GetRandomVacationDuration(Random random)
        {
            int[] vacationDurations = { 7, 14 };
            return vacationDurations[random.Next(vacationDurations.Length)];
        }


        /// <summary>
        /// Проверка пересечения отпусков
        /// </summary>
        /// <param name="vacationList">Имеющийся список отпусков</param>
        /// <param name="startDate">Начало проверяемого периода</param>
        /// <param name="endDate">Конец проверяемого периода</param>
        /// <returns></returns>
        static bool IsVacationOverlapping(List<DateTime> vacationList, DateTime startDate, DateTime endDate)
        {
            return vacationList.Any(date => date >= startDate &&
                                            date <= endDate);
        }


        /// <summary>
        /// Проверка пересечения месяцев
        /// </summary>
        /// <param name="vacationList">Имеющийся список отпусков</param>
        /// <param name="startDate">Начало проверяемого периода</param>
        /// <param name="endDate">Конец проверяемого периода</param>
        /// <returns></returns>
        static bool IsMonthBoundariesOverlap(List<DateTime> vacationList, DateTime startDate, DateTime endDate)
        {
            DateTime startMonth = new DateTime(startDate.Year, startDate.Month, 1);
            DateTime endMonth = new DateTime(endDate.Year, endDate.Month, DateTime.DaysInMonth(endDate.Year, endDate.Month));

            return vacationList.Any(date => (date >= startMonth && date <= endMonth) ||
                                            (date.AddDays(3) >= startMonth && date.AddDays(3) <= endMonth));
        }


        /// <summary>
        /// Заполнение списка отпусков релевантными датами
        /// </summary>
        /// <param name="vacationList">Имеющийся список отпусков</param>
        /// <param name="startDate">Начало проверяемого периода</param>
        /// <param name="endDate">Конец проверяемого периода</param>
        static void AddVacationDates(List<DateTime> vacationList, DateTime startDate, DateTime endDate)
        {
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                vacationList.Add(date);
            }
        }


        /// <summary>
        /// Вывод возможных дат отпусков для сотрудников
        /// </summary>
        /// <param name="vacationDictionary">Словарь сотрудников и отпусков</param>
        static void PrintVacationDates(Dictionary<string, List<DateTime>> vacationDictionary)
        {
            foreach (var vacationList in vacationDictionary)
            {
                Console.WriteLine("Дни отпуска " + vacationList.Key + " : ");
                foreach (var date in vacationList.Value)
                {
                    Console.WriteLine(date);
                }
            }
        }
    }
}