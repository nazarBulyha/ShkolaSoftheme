using System;
using System.Linq;
using BinaryTree;

namespace QueryBinaryTree
{
    class Program
    {
        static void DoWork()
        {
            Tree<Employee> employeesTree = new Tree<Employee>(new Employee { Id = 1, FirstName = "Kim", LastName = "Abercrobmbie", Department = "IT" });
            employeesTree.Insert(new Employee { Id = 2, FirstName = "Jeff", LastName = "Hay", Department = "Marketing" });
            employeesTree.Insert(new Employee { Id = 4, FirstName = "Charlie", LastName = "Herb", Department = "IT" });
            employeesTree.Insert(new Employee { Id = 6, FirstName = "Chris", LastName = "Preston", Department = "Sales" });
            employeesTree.Insert(new Employee { Id = 3, FirstName = "Dave", LastName = "Barnett", Department = "Sales" });
            employeesTree.Insert(new Employee { Id = 5, FirstName = "Tim", LastName = "Litton", Department = "Marketing" });

            Console.WriteLine("All employees");
            var allEmployees = from e in employeesTree.ToList()
                               select e;

            foreach(var emp in allEmployees)
            {
                Console.WriteLine(emp);
            }

            employeesTree.Insert(new Employee { Id = 7, FirstName = "David", LastName = "Simpson", Department = "IT" });
            Console.WriteLine("\nEmployee added");

            Console.WriteLine("All employees");
            foreach(var emp in allEmployees)
            {
                Console.WriteLine(emp);
            }

            Console.WriteLine("List of departments");
            var depts = allEmployees.Select(d => d.Department).Distinct();
            //var depts = (from d in allEmployees
            //             select d.Department).Distinct();
            
            foreach (var dept in depts)
            {
                Console.WriteLine($"Department: {dept}");
            }
            
            Console.WriteLine("\nEmployees in the IT department");
            //var ITEmployees = empTree.Where(e => String.Equals(e.Department, "IT")).Select(emp => emp);
            var ITEmployees = from e in allEmployees
                              where String.Equals(e.Department, "IT")
                              select e;
            
            foreach(var emp in ITEmployees)
            {
                Console.WriteLine(emp);
            }
            
            Console.WriteLine("\nAll employees grouped by department");
            //var employeesByDept = empTree.GroupBy(e => e.Department);
            var employeesByDept = from e in allEmployees
                                  group e by e.Department;
            
            foreach(var dept in employeesByDept)
            {
                Console.WriteLine($"Department: {dept.Key}");
                foreach(var emp in dept)
                {
                    Console.WriteLine($"\t{emp.FirstName} {emp.LastName}");
                }
            }
        }

        static void Main()
        {
            try
            {
                DoWork();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.Message);
            }
        }
    }
}
