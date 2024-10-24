using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP___Factory
{
    class Factory
    {
        private employee[] employees;
        private int employeesAmount;

        public Factory()
        {
            this.employees = new employee[10];
            this.employeesAmount = 0;
        }

        public void employeesSalary()
        {
            foreach (employee employee in this.employees)
            {
                if (employee == null) break;
                employee.Print();
            }
        }

        public employee[] addEmployee(employee employee)
        {
            if (this.employeesAmount == this.employees.Length)
            {
                employee[] newVehicles = new employee[this.employees.Length * 2];
                for (int i = 0; i < this.employees.Length; i++)
                {
                    newVehicles[i] = this.employees[i];
                }
                this.employees = newVehicles;
            }
            this.employees[this.employeesAmount++] = employee;
            return this.employees;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            employee w1 = new worker(30, 224, "cat", 18, "levi", 101);
            employee w2 = new worker(30, 224, "dog", 18, "levi", 101);
            employee s1 = new salesMan(45, 140, 140, "pig", 21, "eshkol", 102);
            Factory factory = new Factory();

            factory.addEmployee(w1);
            factory.addEmployee(s1);

            factory.employeesSalary();

            Console.WriteLine(w1.Equals(w2));

            Console.WriteLine(w1.GetHashCode());
        }
    }

    abstract class person
    {
        private string name;
        private int age;

        public string Name
        {
            get { return this.name; }
        }

        public int Age
        {
            get { return this.age; }
        }

        public person(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public void Print()
        {
            Console.WriteLine($"Name: {this.name}, Age: {this.age}");
            PrintInfo();
            Console.WriteLine();
        }

        protected abstract void PrintInfo();
    }

    abstract class employee : person
    {
        private string department;
        private int empNum;

        public string Department
        {
            get { return this.department; }
        }

        public int EmpNum
        {
            get { return this.empNum; }
        }

        public employee(string department, int empNum, string name, int age) : base(name, age)
        {
            this.department = department;
            this.empNum = empNum;
        }

        protected double CalcNeto() //private
        {
            double grossSalary = CalcGrossSalary();
            double[] taxBrackets = { 7010, 10060, 16150, 22440, 46690, 60130 };
            double[] taxRates = { 0.10, 0.14, 0.20, 0.31, 0.35, 0.47, 0.50 };

            double taxes = 0;
            double remainingSalary = grossSalary;

            // Handle each bracket
            if (grossSalary > taxBrackets[5])
            {
                taxes += (grossSalary - taxBrackets[5]) * taxRates[6];
                remainingSalary = taxBrackets[5];
            }
            if (grossSalary > taxBrackets[4])
            {
                taxes += (remainingSalary - taxBrackets[4]) * taxRates[5];
                remainingSalary = taxBrackets[4];
            }
            if (grossSalary > taxBrackets[3])
            {
                taxes += (remainingSalary - taxBrackets[3]) * taxRates[4];
                remainingSalary = taxBrackets[3];
            }
            if (grossSalary > taxBrackets[2])
            {
                taxes += (remainingSalary - taxBrackets[2]) * taxRates[3];
                remainingSalary = taxBrackets[2];
            }
            if (grossSalary > taxBrackets[1])
            {
                taxes += (remainingSalary - taxBrackets[1]) * taxRates[2];
                remainingSalary = taxBrackets[1];
            }
            if (grossSalary > taxBrackets[0])
            {
                taxes += (remainingSalary - taxBrackets[0]) * taxRates[1];
                remainingSalary = taxBrackets[0];
            }

            taxes += remainingSalary * taxRates[0]; // First bracket (up to 84,120)

            double netSalary = grossSalary - taxes;
            return netSalary;
        }

        protected abstract double CalcGrossSalary();

        protected override void PrintInfo()
        {
            Console.WriteLine($"Department: {this.department}, EmpNum: {this.empNum}");
        }

        //public override string ToString()
        //{
        //    return $"Name: {this.name}\nJob: {this.GetType().Name}\nSalary: {CalcNeto()}";
        //}
    }

    class worker : employee
    {
        private double salaryPerHour;
        private int hoursWorked;

        public worker(double salaryPerHour, int hoursWorked, string name, int age, string department, int empNum) : base(department, empNum, name, age)
        {
            this.salaryPerHour = salaryPerHour;
            this.hoursWorked = hoursWorked;
        }

        protected override double CalcGrossSalary()
        {
            return this.salaryPerHour * this.hoursWorked;
        }

        protected override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"Job: {this.GetType().Name}\nSalary Per Hour: {this.salaryPerHour}\nSalary: {CalcNeto()}");
        }

        public override bool Equals(object obj)
        {
            if (obj is worker)
            {
                worker worker = (worker)obj;
                if (this.salaryPerHour == worker.salaryPerHour && this.hoursWorked == worker.hoursWorked && this.Name == worker.Name && this.Age == worker.Age && this.Department == worker.Department && this.EmpNum == worker.EmpNum)
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    class salesMan : employee
    {
        private double salaryPerHour;
        private int hoursWorked;
        private int sales;

        public salesMan(double salaryPerHour, int hoursWorked, int sales, string name, int age, string department, int empNum) : base(department, empNum, name, age)
        {
            this.salaryPerHour = salaryPerHour;
            this.hoursWorked = hoursWorked;
            this.sales = sales;
        }

        protected override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"Job: {this.GetType().Name}\nSalary Per Hour: {this.salaryPerHour}\nSales: {this.sales}\nSalary: {CalcNeto()}");
        }

        protected override double CalcGrossSalary()
        {
            return this.salaryPerHour * (this.hoursWorked + this.sales);
        }
    }
}
