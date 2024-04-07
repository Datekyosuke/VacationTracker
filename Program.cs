// Класс с информацией об отпуске сотрудника
public class Vacation 
{ 
    public string EmployeeName { get; set; } 
    public List<DateTime> VacationDates { get; set; } 
}

// Класс для учета отпусков сотрудников
public class VacationTracker
{
    private List<string> employees;
    private List<Vacation> vacations;

    public VacationTracker()
    {
         employees = new List<string>();
         vacations = new List<Vacation>();
    }

// Метод для добавления нового сотрудника
public void AddEmployee(string name)
{
    employees.Add(name);
}

// Метод для генерации даты начала отпуска
private DateTime GenerateVacationStartDate()
{
    Random random = new Random();
    return new DateTime(2023, 1, 1).AddDays(random.Next(365));
}

// Метод для определения длительности отпуска
private int GetVacationDuration()
{
    Random random = new Random();
    return random.Next(2) == 0 ? 7 : 14;
}

// Метод для генерации отпусков сотрудников
public void GenerateVacations()
{
    foreach (string employee in employees)
    {    
        int vacationDaysRemains = 28;
        while (vacationDaysRemains >= 7)
        {
                Vacation vacation = new Vacation();
                vacation.EmployeeName = employee;
                vacation.VacationDates = new List<DateTime>();
                // Генерация дат начала отпуска и проверка на условия
                DateTime startDate = GenerateVacationStartDate();
                int duration = GetVacationDuration();
                while (!IsValidVacation(startDate, duration))
                {
                    startDate = GenerateVacationStartDate();
                    duration = GetVacationDuration();
                }

                for (int i = 0; i < duration; i++)
                {
                    vacation.VacationDates.Add(startDate.AddDays(i));
                }
                vacationDaysRemains -= duration;
                vacations.Add(vacation);
        }
    }
        
}

// Метод для проверки условий отпуска
private bool IsValidVacation(DateTime startDate, int duration)
{
    if (duration != 7 && duration != 14)
    {
        return false;
    }

    foreach (Vacation v in vacations)
    {
         foreach (DateTime date in v.VacationDates)
         {
             if (startDate <= date && startDate.AddDays(duration) >= date)
             {
                    return false;
             }
             if (Math.Abs((date - startDate).TotalDays) < 3)
             {
                 return false;
             }
         }
        
    }

    if (startDate.DayOfWeek == DayOfWeek.Saturday || startDate.DayOfWeek == DayOfWeek.Sunday)
    {
        return false;
    }

    return true;
}

    // Метод для вывода списка дней отпуска для каждого сотрудника
    public void DisplayVacationDates() 
    { 
        string currentEmployee = ""; 
        var sortedVacations = vacations.OrderBy(v => v.EmployeeName).ThenBy(v => v.VacationDates.Min()); 
        foreach (Vacation vacation in sortedVacations) 
        {
            if (vacation.EmployeeName != currentEmployee)
            {
                Console.WriteLine();
                Console.WriteLine($"Сотрудник: {vacation.EmployeeName}");
            }
            Console.WriteLine($"Отпуск с: {vacation.VacationDates[0]} по {vacation.VacationDates[vacation.VacationDates.Count - 1]} "); 

            currentEmployee = vacation.EmployeeName; 
        }
    }

    class Program
    {
        static void Main()
        {
            VacationTracker vacationTracker = new VacationTracker();


            vacationTracker.AddEmployee("Григорий");
            vacationTracker.AddEmployee("Егор");
            vacationTracker.AddEmployee("Антонина");
            vacationTracker.AddEmployee("Ибрагим");
            vacationTracker.AddEmployee("Юрий");
            vacationTracker.AddEmployee("Антон");

            vacationTracker.GenerateVacations();
    

            vacationTracker.DisplayVacationDates();
        }
    }
}